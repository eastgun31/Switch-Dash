using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareM : PlayerController
{
    protected override IEnumerator Dash()
    {
        Debug.Log("Square Dash");

        yield return null;
    }

    protected override void Die()
    {
        Debug.Log("Square Die");
    }
}
