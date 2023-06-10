using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Esta clase se va a encargar de atraer a los objetos con masa
/// </summary>

public class GravityAttractor : MonoBehaviour
{
    public float gravity;

    //Funci�n que se va a encargar de atraer a los objetos con masa
    public void Attract(GravityBody body)
    {
        Transform trans = body.transform; //Variable que hace referencia al componente transform del objeto que estamos atrayendo
        Rigidbody rb = body.GetComponent<Rigidbody>();
        
        Vector3 gravityUp = trans.position - transform.position;
        gravityUp.Normalize();

        //Fuerza de atracci�n
        rb.AddForce(gravityUp * gravity * rb.mass);

        //Si el objeto tiene bloqueadas las rotaciones nosotros desde c�digo giramos el objeto
        if(rb.freezeRotation == true)
        {
            //Creamos una rotaci�n que va desde el eje Y del personaje hasta GravityUp
            Quaternion quaternion = Quaternion.FromToRotation(trans.up, gravityUp);

            //Esa rotaci�n que hemos generado en la l�nea anterior la multiplicamos por la rotaci�n actual del personaje
            //Para que el personaje haga dicha rotaci�n partiendo de su rotaci�n actual
            quaternion = quaternion * trans.rotation;

            //Aplico la rotaci�n al personaje
            trans.rotation = Quaternion.Slerp(transform.rotation, quaternion, 1f);
        }
    }
}
