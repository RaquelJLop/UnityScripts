using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjects : MonoBehaviour
{
    public GameObject button; //hace referencia al texto o botón que va a salir cuando el player se acerca para avisar de que se
    //puede interactuar

    public Collider trigger;

    Animator anim;
    Animator buttomAnim;
    PlayerActions playerActions;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        buttomAnim = button.transform.parent.GetComponent<Animator>();
        playerActions = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActions>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            button.SetActive(true);
            buttomAnim.SetBool("ShowUp", true);
            playerActions.canInteract = true;
            playerActions.interactableObject = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            button.SetActive(false);
            buttomAnim.SetBool("ShowUp", false);
            playerActions.canInteract = false;
            playerActions.interactableObject = null;
        }
    }

    //Función que vamos a llamar desde PlayerActions cuando queramos interactuar con el objeto
    public void Interact()
    {
        anim.SetTrigger("Interact"); //animación de apertura de cofre,puerta....
        trigger.enabled = false; //deshabilito el collider
        button.SetActive(false);
        this.enabled = false;

    }
}
