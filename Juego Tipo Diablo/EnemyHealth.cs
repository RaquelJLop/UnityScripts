using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public Slider slider;

    Enemy enemy;
    NavMeshAgent agent;
    Animator anim;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Fireball")) {
            slider.gameObject.SetActive(true);
            if(!enemy.playerDetected) //si el enemigo no había detectado al player lo detecta al ser atacado
            {
                enemy.Attacking(true);
                enemy.PatrolAndAlert(false);
            }
            currentHealth--;
            slider.value = currentHealth;
            Destroy(collision.gameObject); //destruir el fireball
            if(currentHealth <= 0)
            {
                Death();
            }
        }
    }

    void Death()
    {
        //paro las corutinas
        enemy.Attacking(false);
        enemy.PatrolAndAlert(false);
        agent.enabled = false;
        anim.Play("Death");
    }
}
