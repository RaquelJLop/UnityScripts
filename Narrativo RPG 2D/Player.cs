using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    public float distRaycast;

    float h;
    float v;

    Vector2 destination;
    Animator anim;

    public LayerMask layerNotWalkable;

    bool walking;

    void Start()
    {
        //Inicializo destination a la posición actual del player
        destination = transform.position;

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        PlayerMovement();
        PlayerAnimation();
        SpeedMovement();
    }

    public void PlayerMovement()
    {
        if (!walking)
        {
            walking = true;

            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");

            if (h != 0 && !Physics2D.Raycast(transform.position, h * Vector2.right, distRaycast, layerNotWalkable))
            {
                destination = (Vector2)transform.position + (h * Vector2.right);
            }
            else if(v != 0 && !Physics2D.Raycast(transform.position, v * Vector2.up, distRaycast, layerNotWalkable))
            {
                destination = (Vector2)transform.position + (v * Vector2.up);
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

            //Estamos haciendo que el personaje se mueva de cuadrícula en cuadrícula
            if(Vector2.Distance(transform.position, destination) < 0.01f)
            {
                //Si no le indicamos la nueva posición del personaje se generan imprecisiones
                transform.position = destination;
                walking = false;
            }
        }
    }

    void PlayerAnimation()
    {
        anim.SetFloat("VelocityX", h);
        anim.SetFloat("VelocityY", v);
    }

    void SpeedMovement()
    {
        //Cuando pulsemos SHIFT la velocidad se incrementará x2
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Time.timeScale = 2;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Time.timeScale = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Scene"))
        {
            SceneManager.LoadScene("Level2");
        }
    }
}
