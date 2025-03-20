using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleM : PlayerController
{

    protected override void Dash()
    {
        Debug.Log("Circle Dash");
    }


    protected override void Die()
    {
        Debug.Log("Circle Die");
    }
}
