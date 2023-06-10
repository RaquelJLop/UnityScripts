using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject colliderAttack; //Hace referencia al gameobject hijo que tiene el player
    public GameObject colliderHug;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Attack();
        Hug();
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("IsAttack");
            Debug.Log("Estoy atacando");
        }

    }

    void Hug()
    {
        if (Input.GetMouseButtonDown(1))
        {
            anim.SetTrigger("IsHug");
            Debug.Log("Estoy abrazando");
        }

    }

    //función pública que voy a llamar desde el evento de animación
    public void EnableColliderAttack()
    {
        colliderAttack.SetActive(true);
        Invoke("DisableColliderAttack", 1f);
    }

    void DisableColliderAttack()
    {
        colliderAttack.SetActive(false);
    }

    public void EnableColliderHug()
    {
        colliderHug.SetActive(true);
        Invoke("DisableColliderHug", 1f);
    }

    void DisableColliderHug()
    {
        colliderHug.SetActive(false);
    }
}
