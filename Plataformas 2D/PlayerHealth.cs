using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public float maxHealth;
    public float currentHealth;
    public float recoveryHealth;

    [Header("UI")]
    public Image acornUI;

    [Header("Death")]
    public float forceJumpDeath;
    public GameManager gameManager;

    Animator anim;
    ArdillaMovement playerMovement;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<ArdillaMovement>();
        currentHealth = maxHealth;
        acornUI.fillAmount = 1;
    }

    //método público que voy a llamar desde el script del enemigo
   public void TakeDamage(int amount)
    {
        if (anim.GetBool("Hurt") || currentHealth <= 0) return;

        currentHealth -= amount;
        acornUI.fillAmount = currentHealth / maxHealth;

        anim.SetBool("Hurt", true);
        //parar al player, resetear velocidad
        playerMovement.ResetVelocity();

        if(currentHealth <= 0)
        {
            Death();
            return;
        }

        //si no se cumplen todas las condiciones anteriores y la ardilla sigue viva, cuando la dañan es invulnerable por un segundo mientras cambia de animación
        Invoke("HurtToFalse", 1);
    }

    void HurtToFalse()
    {
        anim.SetBool("Hurt", false);
    }

    void Death()
    {
        gameManager.GameOver();
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * forceJumpDeath);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Cherry"))
        {
            currentHealth += recoveryHealth;

            //compruebo que la vida no sea la vida maxima
            if (currentHealth > maxHealth) currentHealth = maxHealth;

            acornUI.fillAmount = currentHealth / maxHealth;

            if (currentHealth < maxHealth) Destroy(collision.gameObject);
        }
    }
}
