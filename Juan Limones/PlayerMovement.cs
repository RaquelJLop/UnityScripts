using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int speed;
    public int turnSpeed;

    AudioSource audioS;
    Rigidbody rb;
    Animator anim;
    Vector3 movement; //para guardar la dirección de movimiento
    float horizontal, vertical; //input

    void Start()
    {
        audioS = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        
    }

    void Update()
    {
        InputPlayer();
        Animating();
        AudioSteps();
    }

    private void FixedUpdate()
    {
        //rb.MovePosition(transform.position + (movement * speed * Time.deltaTime));
        Rotation();
    }

    //No es lo normal, pero si la animación se traslada se debe usar la velocidad de la propia animación y se hace así
    private void OnAnimatorMove()
    {
        rb.MovePosition(transform.position + (movement * anim.deltaPosition.magnitude));
    }

    void Rotation()
    {
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.deltaTime,0);
        Quaternion rotation = Quaternion.LookRotation(desiredForward);
        rb.MoveRotation(rotation);
    }

    void InputPlayer()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        movement = new Vector3(horizontal, 0, vertical);
        movement.Normalize();
    }

    void Animating()
    {
        if (horizontal != 0 || vertical != 0) anim.SetBool("IsWalking", true);
        else anim.SetBool("IsWalking", false);
    }

    void AudioSteps()
    {
        if (horizontal != 0 || vertical != 0)
        {
            if (audioS.isPlaying == false)
                audioS.Play();
        }
        else
            audioS.Stop();
    }
}
