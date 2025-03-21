using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleM : PlayerController
{
    [SerializeField] private Vector3[] dashRoot = new Vector3[4];

    protected override IEnumerator Dash()
    {

        while (isDash)
        {
            Debug.Log("Circle Dash now");

            if (transform.position.x < dashRoot[1].x)
                transform.position = Vector3.MoveTowards(transform.position, dashRoot[1], dashspeed * Time.deltaTime);
            else if (transform.position.x < dashRoot[2].x)
                transform.position = Vector3.MoveTowards(transform.position, dashRoot[2], dashspeed * Time.deltaTime);
            else if (transform.position.x < dashRoot[3].x)
                transform.position = Vector3.MoveTowards(transform.position, dashRoot[3], dashspeed * Time.deltaTime);

            if (transform.position == dashRoot[3])
            {
                isDash = false;
                StartCoroutine(DashReset());
            }

            yield return null;
        }
    }

    private IEnumerator DashReset()
    {
        rb.gravityScale = 5;
        while (transform.position.x > dashRoot[0].x)
        {
            transform.position = Vector3.MoveTowards(transform.position, dashRoot[0], 3 * Time.deltaTime);
            yield return null;
        }
    }


    protected override void Die()
    {
        Debug.Log("Circle Die");
    }
}
