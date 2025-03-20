using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleM : PlayerController
{
    [SerializeField] private Vector3[] dashRoot = new Vector3[4];

    protected override IEnumerator Dash()
    {
        transform.position = dashRoot[0];
        isDash = true;
        anim.SetBool(run, false);

        yield return null;
    }


    protected override void Die()
    {
        Debug.Log("Circle Die");
    }
}
