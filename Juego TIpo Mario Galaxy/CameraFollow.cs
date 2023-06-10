using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float distance;
    public float height;
    public float damping;
    public float rotatingDamping;

    void FixedUpdate()
    {
        //Calculamos la posición de la cámara
        Vector3 desiredPosition = player.TransformPoint(0, height, -distance);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);

        //Calculo la rotación
        Vector3 cameraToPlayer = player.position - transform.position;
        Quaternion desiredRotation = Quaternion.LookRotation(cameraToPlayer, player.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotatingDamping);

        Debug.DrawLine(transform.position, player.position, Color.yellow);
    }
}
