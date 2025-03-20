using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleM : PlayerController
{
    protected override IEnumerator Dash()
    {
        Debug.Log("Triangle Dash");

        yield return null;
    }


    protected override void Die()
    {
        Debug.Log("Triangle Die");
    }
}
