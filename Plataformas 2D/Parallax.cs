using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public float distanceX; //representa a que "distancia" se encuentra el fondo
    public float smoothing; //velocidad

    Transform cam;
    Vector3 previousCamPos; //variable para guardarme la posición de la cámara en el frame anterior

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        previousCamPos = cam.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(distanceX != 0)
        {
            float parallaxX = (previousCamPos.x - cam.position.x) * distanceX;
            Vector3 backgroundTargetPosX = new Vector3(transform.position.x + parallaxX, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, backgroundTargetPosX, smoothing * Time.deltaTime);

            previousCamPos = cam.position;
        }
    }
}
