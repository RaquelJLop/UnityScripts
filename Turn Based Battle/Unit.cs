using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    #region Public_Variables
    [Header("Info Unit")]
    public string unitName;
    public float unitTime; //Tiempo que va a tardar el personaje en atacar

    [Header("Attack Variables")]
    public float timeAnimationAttack; //Tiempo que tarda en ejecutarse la animación de ataque
    public float speed; //La velocidad a la que se va a mover el personaje para atacar
    public float offset; //Para calcular la distancia a la que se va a quedar el personaje cuando ataque

    public int damage;
    public int healAmount;

    [Header("HP")]
    public int maxHP; //Salud máxima
    public int currentHP; //Salud actual
    #endregion

    Animator anim;
    Vector3 startedPosition; //Posición inicial de la unidad (posición en batalla)
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        startedPosition = transform.position;
    }

    //Función que se va a encargar de mover a la unidad hasta su enemigo y ejecutar el ataque
    public IEnumerator Attacking(Vector3 point)
    {
        anim.SetBool("Moving", true);
        //Mover al personaje
        while (Vector3.Distance(transform.position, point) >= offset)
        {
            transform.position = Vector3.MoveTowards(transform.position, point, speed * Time.deltaTime);
            yield return null;
        }
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(timeAnimationAttack); //Me espero a que se ejecute la anim de ataque
    }

    public IEnumerator MovingToStartedPosition()
    {
        while (Vector3.Distance(transform.position, startedPosition) >= 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startedPosition, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = startedPosition;
        anim.SetBool("Moving", false);
    }
    
    //Función que me va a devolver un valor booleano, para decirme si el personaje está muerto o no
    //Esta función la vamos a llamar desde el script Battle System
    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg; //Quito vida

        if (currentHP <= 0) return true; //Ha muerto
        else return false; //Sigue vivo
    }

    public void Heal(int amount)
    {
        currentHP += amount;

        //Comprobamos si hemos superado la vida máxima
        if (currentHP >= maxHP)
            currentHP = maxHP;
    }
}
