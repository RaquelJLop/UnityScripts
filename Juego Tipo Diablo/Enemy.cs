using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Patrol")]
    public Transform[] checkPoints;
    int index;

    [Header("Alert")]
    public float visionRange;
    public float visionAngle;
    public bool playerDetected;

    [Header("Speed")]
    public float patrolSpeed;
    public float attackingSpeed;

    [Header("Attack")]
    public Collider colliderAttack;

    Transform player;
    NavMeshAgent agent;
    Animator anim;
    PlayerHealth playerHealth;

    void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        PatrolAndAlert(true);
    }

    void Update()
    {
        Animating();
    }

    void Animating()
    {
        anim.SetFloat("Velocity", agent.velocity.magnitude);
    }

    public void PatrolAndAlert (bool state)
    {
        if (state)
        {
            StartCoroutine("Patrol");
            StartCoroutine("Alert");
        }
        else
        {
            StopCoroutine("Patrol");
            StopCoroutine("Alert");
        }
    }

    public void Attacking (bool state)
    {
        if (state) StartCoroutine("Attack");
        else StopCoroutine("Attack");
    }

    IEnumerator Patrol()
    {
        agent.speed = patrolSpeed;

        #region Patrol by CheckPoints
        //while (true)
        //{            //me dirijo hacia mi posición de destino
        //    agent.SetDestination(checkPoints[0].position);
        //    //mientras no haya llegado a mi punto de destino me espero al siguiente frame
        //    while (Vector3.Distance(transform.position, checkPoints[index].position) > agent.stoppingDistance)
        //    {
        //        yield return null;
        //    }
        //    //escoger un nuevo punto de ruta
        //    yield return new WaitForSeconds(Random.Range(1, 3)); //random tiempo de espera
        //    index++;
        //    if (index >= checkPoints.Length) //si estoy en la última posición de la array me voy a la primera
        //        index = 0;
        //}
        #endregion

        #region Patrol Random
        while (true)
        {
            Vector3 destination = transform.position;
            while (Vector3.Distance(transform.position, destination) > agent.stoppingDistance)
            {
                yield return null;
                if (agent.velocity.magnitude < 0.1f) //si la velocidad es muy pequeña ya he llegado a mi destino
                    destination = transform.position;
            }
            //me espero
            yield return new WaitForSeconds(Random.Range(1, 3));
            //cojo puntos aleatorios dentro de una esfera fija
            Vector3 randomPoint = checkPoints[0].position + (Random.insideUnitSphere * 20);
            NavMeshHit hit;
            NavMesh.SamplePosition(randomPoint, out hit, 50, 1);
            destination = hit.position;
            agent.SetDestination(destination);
        }

        #endregion
    }

    //Buscando al Player
    IEnumerator Alert()
    {
        while (true)
        {
            Vector3 direction = player.position - transform.position;
            float distance = Vector3.Distance(transform.position, player.position);
            float angle = Vector3.Angle(transform.forward, direction);
            float diffY = Mathf.Abs(transform.position.y - player.position.y);

            if(distance < visionRange && angle < visionAngle && diffY < 0.5f)
            {
                Debug.Log("Al ataqueeeeeeeeerrrrll");
                Attacking(true);
                PatrolAndAlert(false); //Paro la coroutina de alert y patrulla
            }
            yield return null;
        }
    }

    IEnumerator Attack()
    {
        playerDetected = true;
        agent.ResetPath();
        anim.Play("Alert");
        agent.speed = attackingSpeed;

        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length); //vamos a esperar a que se ejecute la animación para atacar

        //ataco:
        while (true)
        {
            while(Vector3.Distance(transform.position, player.position) > agent.stoppingDistance)
            {
                //si el player se escapa de mi rango
                if(Vector3.Distance(transform.position, player.position) > visionRange * 2)
                {
                    PatrolAndAlert(true);
                    Attacking(false);
                    playerDetected = false;
                }
                else //si sigue dentro de mi rango
                {
                    agent.SetDestination(player.position);
                }
                yield return null;
            }
            if (playerHealth.currentHealth > 0)
            {
                transform.LookAt(player.position);
                anim.Play("Attack");
            }
            yield return new WaitForSeconds(2);
        }
    }

    //Función que va como evento
    public void ActivateCollider()
    {
        colliderAttack.enabled = true;
    }

    public void DesactivateCollider()
    {
        colliderAttack.enabled = false;
    }

    //Para dibujar en la escena y poder debugear lo que estamos haciendo
    private void OnDrawGizmos()
    {
        //Dibujo una esfera con el rango de visión del enemigo
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Gizmos.color = Color.green;
        Vector3 righDir = Quaternion.Euler(0, visionAngle, 0) * transform.forward;
        Vector3 leftDir = Quaternion.Euler(0, -visionAngle, 0) * transform.forward;

        Gizmos.DrawRay(transform.position + new Vector3(0, 1, 0), righDir * visionRange);
        Gizmos.DrawRay(transform.position + new Vector3(0, 1, 0), leftDir * visionRange);
    }
}
