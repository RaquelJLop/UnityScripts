using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : GravityBody
{
    public float maxSpeed;
    public float force;
    public float jumpSpeed;

    bool jump;

    float horizontal;
    float vertical;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
        anim.SetFloat("Velocity", rb.velocity.magnitude);
    }

    void Jump()
    {
        jump = true;
        if (grounded == 0) jump = false;
    }

    void FixedUpdate()
    {
        if(attractor != null)
        {
            attractor.Attract(this);
        }

        //Movimiento
        if(rb.velocity.magnitude <= maxSpeed)
        {
            //Movimiento hacia delante y hacia atrás
            //transform.rotation * Vector3.forward = transform.forward
            rb.AddForce(transform.rotation * Vector3.forward * vertical * force);
            //Movimiento lateral
            rb.AddForce(transform.rotation * Vector3.right * horizontal * force);
        }

        if(jump)
        {
            jump = false;
            rb.velocity = rb.velocity + (rb.transform.up * jumpSpeed);
        }
    }

    void RotationVectors()
    {
        Quaternion rot = Quaternion.Euler(0, 45, 0);
        Vector3 d = rot * transform.forward;
        Debug.DrawLine(transform.position, transform.position + (d * 10), Color.red);
        Debug.DrawLine(transform.position, transform.position + (transform.forward * 10), Color.blue);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Chick chick = collision.gameObject.GetComponent<Chick>();
        //Girl girl = collision.gameObject.GetComponent<Girl>();
        //Hat hat = collision.gameObject.GetComponent<Hat>();
        //if (chick != null)
        //    chick.Action();
        //if (girl != null)
        //    girl.Action();
        //if (hat != null)
        //    hat.Action();
        if(collision.gameObject.GetComponent<IActionable>() != null)
            collision.gameObject.GetComponent<IActionable>().Action();
    }
}
