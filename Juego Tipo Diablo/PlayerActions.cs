using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public bool canInteract;
    public InteractableObjects interactableObject; //va a hacer referencia al componente (la clase) del objeto
    //con el que el player está interactuando

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            //Llamada a la función para interactuar con el objeto
            interactableObject.Interact();
        }
    }
}
