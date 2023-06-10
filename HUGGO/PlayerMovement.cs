using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public int speed;
    public int turnSpeed;
    public int runSpeed;

    [Header("Raycast")]
    public Transform groundCheck; //punto de origen del raycast
    public LayerMask groundLayer; //la capa donde va a estar contenido el suelo
    public float RayLength; //longitud del rayo
    public bool isGrounded; //variable que me va a decir si estamos tocando el suelo o no

    [Header("Jump")]
    public float jumpForce;
    public bool canJump;

    [Header("UI")]
    public GameManager gameManager;

    float horizontal, vertical;
    Rigidbody rb;
    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        InputPlayer();
        JumpPressed();
        IsGrounded();
        Animating();
    }

    void FixedUpdate()
    {
        Move();
        if (canJump) Jump();
    }

    #region PlayerMovement
    void Move()
    {
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded) speed = runSpeed;
        else if (isGrounded) speed = 10;
        else speed = 5;

        transform.Rotate(0, horizontal * Time.deltaTime * turnSpeed, 0);
        transform.Translate(0, 0, vertical * Time.deltaTime * speed);
    }

    void InputPlayer()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }
    #endregion

    #region Jump
    void JumpPressed()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) canJump = true;
    }

    void Jump()
    {
        canJump = false;
        rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
    }

    void IsGrounded()
    {
        //Lanzo un raycast selectivo (solo detecta los objetos de la capa groundLayer) que tiene una longitud rayLenght
        //un origen groundCheck y la dirección hacia abajo en el eje Y. El raycast devuelve true si está chocanco con un objeto de la capa groundLayer
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, RayLength, groundLayer);
        Debug.DrawRay(groundCheck.position, Vector3.down * RayLength, Color.red);
    }
    #endregion

    #region Animation
    void Animating()
    {
        if (vertical != 0) anim.SetBool("IsWalking", true);
        else anim.SetBool("IsWalking", false);

        if (vertical != 0 && speed >= 20) anim.SetBool("IsRunning", true);
        else anim.SetBool("IsRunning", false);

        if (!isGrounded)
        {
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsJumping", true);
        }
        else anim.SetBool("IsJumping", false);
    }
    #endregion

    #region Collectables
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            //acceder al GameManager y llamar a la función addCoin
            gameManager.GetComponent<GameManager>().AddCoin();

            GameObject coin = other.gameObject;
            Destroy(coin);
        }
    }
    #endregion
}