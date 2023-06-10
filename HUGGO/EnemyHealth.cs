using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private bool isCatch;

    [SerializeField] private GameManager gameManager;

    EnemyPatrol enemyPatrol;

    private void Start()
    {
        enemyPatrol = GetComponent<EnemyPatrol>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hug") && !isCatch)
        {
            isCatch = true;
            enemyPatrol.enabled = false;
            gameManager.GetComponent<GameManager>().AddGoblin();
            Destroy(gameObject, 1f);
        }

        else if (other.CompareTag("Attack") && !isCatch)
        {
            isCatch = true;
            enemyPatrol.enabled = false;
            Destroy(gameObject, 1f);
        }
    }
}
