using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public enum BattleState { Start, PlayerTurn, EnemyTurn, Won, Lost };
    public BattleState battleState;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerInitPos;
    public Transform enemyInitPos;

    public BattleHUD battleHUD;

    Unit playerUnit;
    Unit enemyUnit;

    void Start()
    {
        battleState = BattleState.Start;
        StartCoroutine("SetUpBattle");
    }

    IEnumerator SetUpBattle()
    {
        GameObject player = Instantiate(playerPrefab, playerInitPos.position, new Quaternion());
        playerUnit = player.GetComponent<Unit>();

        GameObject enemy = Instantiate(enemyPrefab, enemyInitPos.position, new Quaternion());
        enemyUnit = enemy.GetComponent<Unit>();

        battleHUD.SetHUD(playerUnit); //Rellenamos canvas con info inicial
        battleHUD.SetHP(playerUnit.currentHP, playerUnit.maxHP); //Mostrar por pantalla salud del player

        //¿Quiero poner algo antes de que el player cargue su barra de tiempo?

        //Llama a la corrutina y hasta que no termina no sigue llamando esta corrutina
        yield return StartCoroutine("PlayerTime");
        //Si aquí queréis poner algo se ejecutaría luego del PlayerTime
    }

    //Corrutina que se va a encargar de cargar el tiempo de ataque (slider)
    IEnumerator PlayerTime()
    {
        float timePlayer = 0;
        while (timePlayer < playerUnit.unitTime)
        {
            timePlayer += Time.deltaTime; //Voy sumando valor a mi contador de tiempo
            battleHUD.timeSlider.value = timePlayer;
            yield return null;
        }
        battleState = BattleState.PlayerTurn;
        PlayerTurn();
        Debug.Log("Turno de Zaaaaaack");
    }

    void PlayerTurn()
    {
        battleHUD.panelButtons.SetActive(true);
    }

    //Función que va en el botón Attack
    public void OnAttackButton()
    {
        if (battleState != BattleState.PlayerTurn)
            return;
        StartCoroutine("PlayerAttack");
    }

    //Función que va en el botón Heal
    public void OnHealButton()
    {
        if (battleState != BattleState.PlayerTurn)
            return;
        StartCoroutine("PlayerHeal");
    }

    IEnumerator PlayerAttack()
    {
        ResetAttackPlayer(); //Reseteo el ataque

        //Mover al player hasta el enemigo -> Unit
        yield return StartCoroutine(playerUnit.Attacking(enemyUnit.transform.position));

        //Quitar vida al enemigo -> Unit
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        //////StartCoroutine(battleHUD.ShowTextDamage(playerUnit.TakeDamage));

        //Volver a poner al player en su posición inicial -> Unit
        yield return StartCoroutine(playerUnit.MovingToStartedPosition());

        Debug.Log("El player ha atacado:" + enemyUnit.currentHP);
        if(isDead)
        {
            battleState = BattleState.Won;
            Debug.Log("Batalla ganada");
        }
        else
        {
            battleState = BattleState.EnemyTurn;
            Debug.Log("Turno del enemigo");
            StartCoroutine("EnemyTurn");
        }
    }

    IEnumerator PlayerHeal()
    {
        ResetAttackPlayer();
        playerUnit.Heal(playerUnit.healAmount);

        //Actualizamos la vida en la interfaz
        battleHUD.SetHP(playerUnit.currentHP, playerUnit.maxHP);
        Debug.Log("Me he curado a tope: " + playerUnit.currentHP);

        yield return new WaitForSeconds(2); //Me espero mientras se ejecuta feedback visual de la cura

        battleState = BattleState.EnemyTurn;
        Debug.Log("Turno del enemigo");
        StartCoroutine("EnemyTurn");
    }

    void ResetAttackPlayer()
    {
        battleHUD.panelButtons.SetActive(false);
        battleHUD.timeSlider.value = 0;
    }

    IEnumerator EnemyTurn()
    {
        //Me muevo hacia el player
        yield return StartCoroutine(enemyUnit.Attacking(playerUnit.transform.position));
        //Quito vida al player
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
        //Actualizo la vida en la interfaz
        battleHUD.SetHP(playerUnit.currentHP, playerUnit.maxHP);
        //Enemigo vuelve a la posición inicial
        yield return StartCoroutine(enemyUnit.MovingToStartedPosition());

        Debug.Log("El enemigo ha atacado: " + playerUnit.currentHP);

        if(isDead) //Si el player está muerto
        {
            battleState = BattleState.Lost;
            Debug.Log("Hemos perdido la batalla");
        }
        else //Si el player no está muerto
        {
            battleState = BattleState.PlayerTurn;
            StartCoroutine("PlayerTime"); //Llamo a la corutine que se encarga de gestionar el tiempo de ataque
            Debug.Log("Turno del player");
        }
    }
}
