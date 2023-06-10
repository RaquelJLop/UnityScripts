using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableChest : MonoBehaviour
{
    bool playerInside;
    public bool interactable;

    public Sprite spriteOpenChest;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerInside && interactable)
        {
            Action();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = false;
        }
    }

    private void Action()
    {
        Debug.Log("Bollicao recogido");
        spriteRenderer.sprite = spriteOpenChest;
        LevelTownManager.instance.OpenChest();
        interactable = false;
    }
}
