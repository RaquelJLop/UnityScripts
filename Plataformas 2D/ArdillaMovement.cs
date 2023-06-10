using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArdillaMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public float acceleration;

    [Header("Raycast")]
    public Transform groundCheck; //punto de origen del raycast
    public LayerMask groundLayer; //la capa donde va a estar contenido el suelo
    public float RayLength; //longitud del rayo
    public bool isGrounded; //variable que me va a decir si estamos tocando el suelo o no

    [Header("Jump")]
    public float jumpForce;
    bool jumpPressed; //me va a decir si puedo saltar o no

    Animator anim;
    Rigidbody2D rb2D;
    SpriteRenderer spriteRenderer;
    PlayerHealth playerHealth;
    public GameManager gameManager;
    Vector2 targetVelocity; //velocidad a la que quiero mover al personaje
    Vector3 dampVelocity; //variable en la que voy a guardar la velocidad actual del personaje
    float horizontal;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<PlayerHealth>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //si el parámetro Hurt es true (está reproduciendo la animación de daño) con el return me salgo del update
        if (anim.GetBool("Hurt")) return;

        InputPlayer();
        TargetVelocity();
        Animating();
        Flip();
        IsGrounded();
        JumpPressed();
    }

    void FixedUpdate()
    {
        Move();
        if (jumpPressed) Jump();
        ChangeGravity();
    }

    void Move()
    {
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, targetVelocity, ref dampVelocity, acceleration);
    }

    void InputPlayer()
    {
        horizontal = Input.GetAxis("Horizontal");
    }

    void TargetVelocity()
    {
        targetVelocity = new Vector2(horizontal * speed, rb2D.velocity.y);
    }

    void Animating()
    {
        if (horizontal != 0) anim.SetBool("IsRunning", true);
        else anim.SetBool("IsRunning", false);

        anim.SetBool("IsJumping", !isGrounded);
    }

    void Flip()
    {
        if (horizontal > 0) spriteRenderer.flipX = false;
        else if (horizontal < 0) spriteRenderer.flipX = true;
    }

    void ChangeGravity()
    {
        //si el player está cayendo aumentamos el valor de la gravedad para mejorar el game feel
        if (rb2D.velocity.y < 0 && playerHealth.currentHealth > 0) rb2D.gravityScale = 1.5f;
        else if (rb2D.velocity.y < 0 && playerHealth.currentHealth <= 0) rb2D.gravityScale = 3;
        else rb2D.gravityScale = 1;
    }

    public void ResetVelocity()
    {
        targetVelocity = Vector2.zero;
    }

    void JumpPressed()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true) jumpPressed = true;
    }

    void Jump()
    {
        jumpPressed = false;
        rb2D.AddForce(Vector2.up * jumpForce);
    }

    void IsGrounded()
    {
        //Lanzo un raycast selectivo (solo detecta los objetos de la capa groundLayer) que tiene una longitud rayLenght
        //un origen groundCheck y la dirección hacia abajo en el eje Y. El raycast devuelve true si está chocanco con un objeto de la capa groundLayer
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, RayLength, groundLayer);
        Debug.DrawRay(groundCheck.position, Vector2.down * RayLength, Color.red);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Plataform"))
        {
            //pongo el player como hijo de la plataforma
            transform.SetParent(collision.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Plataform"))
        {
            transform.SetParent(null);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Acorn"))
        {
            //acceder al GameManager y llamar a la función addAcorn
            gameManager.GetComponent<GameManager>().AddAcorn();

            Destroy(collision.gameObject);
        }
    }

}
