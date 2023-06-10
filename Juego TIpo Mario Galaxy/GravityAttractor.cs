using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Esta clase se va a encargar de atraer a los objetos con masa
/// </summary>

public class GravityAttractor : MonoBehaviour
{
    public float gravity;

    //Función que se va a encargar de atraer a los objetos con masa
    public void Attract(GravityBody body)
    {
        Transform trans = body.transform; //Variable que hace referencia al componente transform del objeto que estamos atrayendo
        Rigidbody rb = body.GetComponent<Rigidbody>();
        
        Vector3 gravityUp = trans.position - transform.position;
        gravityUp.Normalize();

        //Fuerza de atracción
        rb.AddForce(gravityUp * gravity * rb.mass);

        //Si el objeto tiene bloqueadas las rotaciones nosotros desde código giramos el objeto
        if(rb.freezeRotation == true)
        {
            //Creamos una rotación que va desde el eje Y del personaje hasta GravityUp
            Quaternion quaternion = Quaternion.FromToRotation(trans.up, gravityUp);

            //Esa rotación que hemos generado en la línea anterior la multiplicamos por la rotación actual del personaje
            //Para que el personaje haga dicha rotación partiendo de su rotación actual
            quaternion = quaternion * trans.rotation;

            //Aplico la rotación al personaje
            trans.rotation = Quaternion.Slerp(transform.rotation, quaternion, 1f);
        }
    }
}
