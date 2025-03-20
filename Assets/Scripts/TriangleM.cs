using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleM : PlayerController
{
    protected override void Dash()
    {
        Debug.Log("Triangle Dash");
    }


    protected override void Die()
    {
        Debug.Log("Triangle Die");
    }
}
