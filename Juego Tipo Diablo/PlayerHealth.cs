using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public Image healthImage;

    Animator anim;
    PlayerAttack playerAttack;
    PlayerMovement playerMovement;

    private void Awake()
    {
        playerAttack = GetComponent<PlayerAttack>();
        playerMovement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("DamageEnemy"))
        {
            currentHealth--; //Se podría cambiar esto por un damage del enemigo
            healthImage.fillAmount = currentHealth / maxHealth;
            if (currentHealth <= 0)
            {
                Death();
            }
        }
    }

    void Death()
    {
        playerAttack.enabled = false;
        playerMovement.enabled = false;
        anim.Play("Death");
    }
}
