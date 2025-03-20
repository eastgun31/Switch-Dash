using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareM : PlayerController
{
    protected override void Dash()
    {
        Debug.Log("Square Dash");
    }

    protected override void Die()
    {
        Debug.Log("Square Die");
    }
}
