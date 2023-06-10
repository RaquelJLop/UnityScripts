using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{

    public bool canMove;

    NavMeshAgent agent;
    Animator anim;
    Ray ray;
    RaycastHit hit;
    bool move;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        InputPlayer();
        Move();
        Animating();
    }

    void InputPlayer()
    {
        if (Input.GetMouseButtonDown(0) && canMove) move = true;
    }

    void Move()
    {
        if(move)
        {
            move = false;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }

        if(agent.velocity != Vector3.zero) //Si el player se está moviendo
        {
            //transform.rotation -> Quaternion
            //transform.eulerAngles -> los ángulos euler del objeto (los que aparecen en el editor)
            transform.eulerAngles = new Vector3(0, Quaternion.LookRotation(agent.velocity).eulerAngles.y, 0);
        }
    }

    void Animating()
    {
        anim.SetFloat("Velocity", agent.velocity.magnitude);
    }

    //Evento que vamos a llamar al final de la animación de ataque
    public void ActiveMovement()
    {
        Debug.Log("muevete fruto");
        canMove = true;
    }
}
