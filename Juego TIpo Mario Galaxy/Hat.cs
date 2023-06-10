using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat : GravityBody, IActionable
{
    public Transform point; //Point es un hijo del player

    public void Action()
    {
        attractor = null;
        transform.SetParent(point);
        rb.isKinematic = true;
        GetComponent<Collider>().enabled = false;
        transform.localPosition = Vector3.zero;
    }
}
