using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girl : GravityBody, IActionable
{
    public Material newMat;
    public Renderer rend;

    public void Action()
    {
        rend.material = newMat;
    }
}
