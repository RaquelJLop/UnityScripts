using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnt : MonoBehaviour
{

    public Transform[] pointsObject; //objetos vacíos en la escena que van a representar los puntos de patrulla
    public int speedWalking; //velocidad de patrulla de la ant
    public GameObject acornPrefab; //bellotas que va a soltar cuando muera

    [Header("Attack Player")]
    public float distanceToPlayer; //la distancia a la que deja de patrullar y seguir al player
    public GameObject player;
    public int speedAttack; //velocidad de la ant cuando esté en modo ataque
    public int speedAnimation; //velocidad de la animación de la hormiga cuando va a atacar
    public int damage;

    Vector2[] points; //las posiciones de la patrulla que sacamos a raiz de pointsObjects
    Vector3 posToGo; //variable donde voy a guardar la posición destino de la hormiga
    int i;
    int speed;

    SpriteRenderer spriteRenderer;
    Animator anim;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        speed = speedWalking;
        //inicializando el array
        points = new Vector2[pointsObject.Length];
        for (int i = 0;i<pointsObject.Length;i++)
        {
            points[i] = pointsObject[i].position;
        }

        posToGo = points[0];
    }

    void Update()
    {
        Debug.DrawLine(transform.position, player.transform.position, Color.red);

        if (Vector2.Distance(transform.position, player.transform.position) <= distanceToPlayer)
            AttackPlayer();
        else
            ChangeTargetPos();

        Flip();
        transform.position = Vector3.MoveTowards(transform.position, posToGo, speed * Time.deltaTime);
    }

    void AttackPlayer()
    {
        speed = speedAttack;
        anim.speed = speedAnimation;
        posToGo = new Vector2(player.transform.position.x, transform.position.y);
    }

    //cambiar la posición de la patrulla
    void ChangeTargetPos()
    {
        speed = speedWalking;
        anim.speed = 1;
        if(transform.position == posToGo)
        {
            if (i == points.Length - 1) i = 0;
            else i++;
            posToGo = points[i];
        }
    }

    void Flip()
    {
        if (posToGo.x > transform.position.x) spriteRenderer.flipX = true;
        else if (posToGo.x < transform.position.x) spriteRenderer.flipX = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            if(!collision.collider.GetComponent<ArdillaMovement>().isGrounded)
            {
                collision.collider.attachedRigidbody.AddForce(Vector2.up * 300);
            }
            else //el player está en el suelo
            {
                //le quito vida al player
                collision.collider.GetComponent<PlayerHealth>().TakeDamage(damage);
            }
            Death();
        }
    }

    void Death()
    {
        //morision del enemigo
        anim.SetTrigger("Death");
        Destroy(gameObject, 0.3f);
        Loot();
    }

    void Loot()
    {
        for (int i = 0; i < Random.Range(1, 2); i++)
        {
            Instantiate(acornPrefab, transform.position, transform.rotation);
        }
    }

}