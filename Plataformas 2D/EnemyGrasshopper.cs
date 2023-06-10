using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrasshopper : MonoBehaviour
{
    [Header("Jump")]
    public float forceUp;
    public float forceRight;
    public float timeToJump;

    [Header("Raycast")]
    public Transform groundCheck;
    public LayerMask layerGround;
    public float rayLenght;
    public bool isGrounded;

    int direction; //variable que me va a indicar si se mueve a izquierda o derecha
    Animator anim;
    Rigidbody2D rb2D;
    SpriteRenderer spriteRenderer;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        direction = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToJump) Jump();

        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, rayLenght, layerGround);
        Animating();
    }

    void Animating()
    {
        anim.SetFloat("YVelocity", rb2D.velocity.y);
        anim.SetBool("IsGrounded", isGrounded);
    }

    void Jump()
    {
        timer = 0;
        direction *= -1;
        Flip();
        //direction = direction * -1;
        rb2D.AddForce(Vector2.up * forceUp, ForceMode2D.Impulse);
        rb2D.AddForce(Vector2.right * forceRight * direction, ForceMode2D.Impulse);
    }

    void Flip()
    {
        if (direction > 0) spriteRenderer.flipX = true;
        else spriteRenderer.flipX = false;
    }
}
