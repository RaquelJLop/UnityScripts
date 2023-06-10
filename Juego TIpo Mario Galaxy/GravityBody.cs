using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase padre de la que van a heredar los objetos que se vean atraídos por el planeta
/// </summary>
public class GravityBody : MonoBehaviour
{
    public GravityAttractor attractor = null;
    public int grounded;
    public Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; //NO vamos a usar la gravedad de Unity
        rb.freezeRotation = true; //Bloqueamos la rotación por físicas porque vamos a rotar nosotros el objeto a mano
    }

    #region Detect Ground
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Planet"))
            grounded++;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Planet"))
            grounded--;
    }
    #endregion

    void FixedUpdate()
    {
        if(attractor != null)
        {
            //Configurar el attractor -> Decirle al attractor que atraíga este objeto
            attractor.Attract(this);
        }
    }
}
