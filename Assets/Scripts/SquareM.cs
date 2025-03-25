using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareM : PlayerController
{
    [SerializeField] private Vector3[] dashRoot = new Vector3[2];
    WaitForSeconds dashdelay1 = new WaitForSeconds(0.2f);
    WaitForSeconds dashdelay2 = new WaitForSeconds(0.5f);

    protected override IEnumerator Dash()
    {
        rb.gravityScale = 2;
        yield return dashdelay1;

        while (isDash)
        {
            if (transform.position.x < dashRoot[1].x)
                transform.position = Vector3.MoveTowards(transform.position, dashRoot[1], dashspeed * Time.deltaTime);

            if (transform.position == dashRoot[1])
            {
                yield return dashdelay2;

                isDash = false;
                dashCount = 1;
                StartCoroutine(DashReset());
            }

            yield return null;
        }
    }

    private IEnumerator DashReset()
    {
        rb.gravityScale = 2;
        dashReset = true;

        while (transform.position.x > dashRoot[0].x)
        {
            transform.position = Vector3.MoveTowards(transform.position, dashRoot[0], 10 * Time.deltaTime);

            if (transform.position == dashRoot[0])
            {
                dashReset = false;
                isGround = true;
                anim.SetBool(run, true);
                drawPooling.SetDraw();
            }

            yield return null;
        }
    }

    protected override void Die()
    {
        Debug.Log("Square Die");
    }
}
