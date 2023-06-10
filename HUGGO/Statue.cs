using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hug"))
        {
            gameManager.GetComponent<GameManager>().AddGoblin();
            Destroy(gameObject, 1f);
        }

        else if (other.CompareTag("Attack"))
        {
            Destroy(gameObject, 1f);
        }
    }
}
