using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth;
    public float currentHealth;

    [Header("Recovery Health")]
    public float recoveryHealth;

    [Header("UI Health")]
    public Material matYellow;
    public Material matOrange;
    public Material matRed;
    MeshRenderer peaceHealth;
    public TextMeshProUGUI debugHealth;

    [Header("Death")]
    public GameManager gameManager;

    Animator anim;
    PlayerMovement playerMovement;
    PlayerAttack playerAttack;

    void Start()
    {
        peaceHealth = GameObject.FindGameObjectWithTag("Peace").GetComponent<MeshRenderer>();
        anim = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponentInChildren<PlayerAttack>();
        currentHealth = maxHealth;
        debugHealth.text = "HEALTH: " + currentHealth;
    }

    void Update()
    {

    }

    #region UI
    public void ChangeColor()
    {
        if (currentHealth == 3)
        {
            peaceHealth.material = matYellow;
            Debug.Log("3 VIDAS");
        }
        else if (currentHealth == 2)
        {
            peaceHealth.material = matOrange;
            Debug.Log("2 VIDAS");
        }
        else if (currentHealth == 1)
        {
            peaceHealth.material = matRed;
            Debug.Log("1 VIDA");
        }
    }
    #endregion

    #region Hurt
    public void TakeDamage(int amount) //Método público que voy a llamar desde el script Enemy
    {
        currentHealth -= amount;
        debugHealth.text = "HEALTH: " + currentHealth;

        anim.SetBool("IsHurt", true);

        if (currentHealth <= 0)
        {
            Death();
        }

        Invoke("HurtToFalse", 1.15f);
    }

    void HurtToFalse()
    {
        anim.SetBool("IsHurt", false);
    }
    #endregion

    #region Death
    void Death()
    {
        gameManager.GameOver();
        anim.SetBool("IsDeath", true);
        playerMovement.enabled = false;
        playerAttack.enabled = false;
    }
    #endregion

    #region Recovery Health
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Health"))
        {
            if (currentHealth < maxHealth)
            {
                currentHealth += recoveryHealth;
                Destroy(collision.gameObject);
                ChangeColor();
            }

            //compruebo que la vida no sea la vida maxima
            if (currentHealth > maxHealth) currentHealth = maxHealth;

            debugHealth.text = "HEALTH: " + currentHealth;
        }
    }
    #endregion

}