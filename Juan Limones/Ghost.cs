using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour
{
    public GameManager gameManager;

    public Transform[] positions;
    public float rayLength;

    Ray ray;
    RaycastHit hit;

    NavMeshAgent agent;
    Vector3 posToGo;
    int i;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        posToGo = positions[0].position;
    }

    void Update()
    {
        agent.SetDestination(posToGo);
        ChangePosition();
    }

    private void FixedUpdate()
    {
        ray.origin = transform.position;
        ray.direction = transform.forward;
        if(Physics.Raycast(ray, out hit, rayLength))
        {
            if(hit.collider.CompareTag("Player"))
                gameManager.isPlayerCaught = true;

        }
        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);
    }

    void ChangePosition()
    {
        if (Vector3.Distance(transform.position, posToGo) < 0.1f)
        {
            if (i == positions.Length - 1)
                i = 0;
            else
                i++;
            posToGo = positions[i].position;
        }
    }
}
