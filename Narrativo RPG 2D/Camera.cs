using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    Transform playerPosition;
    Vector2 cameraPosition;

    public Vector2 limitSup;
    public Vector2 limitInf;

    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        CameraLimit();
    }

    private void CameraLimit()
    {
        if(playerPosition.position.x < limitSup.x && playerPosition.position.x > limitInf.x)
        {
            cameraPosition.x = playerPosition.position.x;
        }
        if (playerPosition.position.y < limitSup.y && playerPosition.position.y > limitInf.y)
        {
            cameraPosition.y = playerPosition.position.y;
        }

        //Recolocar la cámara(que es 3D) x10 detras del player para que renderice el 2D
        transform.position = (Vector3)cameraPosition + Vector3.back * 10;
    }
}
