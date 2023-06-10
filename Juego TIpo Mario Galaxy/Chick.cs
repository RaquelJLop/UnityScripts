using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chick : GravityBody, IActionable
{
    public Vector3 newScale;

    public void Action()
    {
        attractor = null; //Le quito la atracción del planeta
        transform.localScale = newScale;
    }
}
