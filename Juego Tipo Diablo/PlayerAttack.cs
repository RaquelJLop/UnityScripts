using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAttack : MonoBehaviour
{
    //public GameObject fireBallPrefab;
    public Rigidbody fireBallPrefabRB;
    public float shootForce;
    public Transform shootPoint;

    Animator anim;
    PlayerMovement playerMovement;
    NavMeshAgent agent;
    Ray ray;
    RaycastHit hit;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) Attack();
    }

    void Attack()
    {
        //Ejecuta la animaci�n contenida dentro del estado Attack
        anim.Play("Attack");
        playerMovement.canMove = false;
        agent.velocity = Vector3.zero; //Paro al personaje
        agent.ResetPath();
        Turn();
    }

    void Turn()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
    }

    //Lo vamos a llamar como evento en la animaci�n de ataque
    public void Shoot()
    {
        //GameObject cloneFireBall = Instantiate(fireBallPrefab, shootPoint.position, shootPoint.rotation);
        //cloneFireBall.GetComponent<Rigidbody>().AddForce(shootPoint.forward * shootForce);

        //Esto ser�a igual que abajo pero m�s optimizado
        Rigidbody cloneFireBallRB = Instantiate(fireBallPrefabRB, shootPoint.position, shootPoint.rotation) as Rigidbody;
        cloneFireBallRB.AddForce(shootPoint.forward * shootForce);

        Destroy(cloneFireBallRB, 4);
    }
}
