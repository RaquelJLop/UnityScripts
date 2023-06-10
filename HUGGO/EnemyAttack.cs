using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    [Header("Attack")]
    public int damage;
    public float attackImpulse;

    Rigidbody rb;
    PlayerMovement playerMovement;
    PlayerHealth playerHealth;

    private void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }


    #region Quitar vida y Empujar al Player
    private void OnTriggerEnter(Collider collision)
    {
        float fewSeconds = 1;

        if (collision.CompareTag("Player"))
        {
            playerHealth.TakeDamage(damage);
            playerHealth.ChangeColor();
            rb.AddForce(new Vector3(0, 0, -attackImpulse), ForceMode.Impulse);
            Disable(fewSeconds);
        }
    }

    void Disable(float time)
    {
        playerMovement.enabled = false;
        // If we were called multiple times, reset timer.
        CancelInvoke("Enable");
        // Note: Even if we have disabled the script, Invoke will still run.
        Invoke("Enable", time);
    }

    void Enable()
    {
        if (playerHealth.currentHealth >= 1)
            playerMovement.enabled = true;
        else playerMovement.enabled = false;
    }
    #endregion

}