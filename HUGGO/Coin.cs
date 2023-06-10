using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int turnSpeed;

    void Update()
    {
        transform.Rotate(Vector3.forward * turnSpeed * Time.deltaTime);
    }

}