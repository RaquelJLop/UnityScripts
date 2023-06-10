using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    Animator anim;

    public bool isOpen;

    public GameManager gameManager;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attack") && !isOpen)
        {
            anim.SetTrigger("IsOpen");
            gameManager.GetComponent<GameManager>().AddCoin3();
            isOpen = true;
        }
    }
}
