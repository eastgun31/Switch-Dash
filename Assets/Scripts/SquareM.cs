using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareM : PlayerController
{
    WaitForSeconds dashdelay1 = new WaitForSeconds(0.2f);
    WaitForSeconds dashdelay2 = new WaitForSeconds(1f);

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
                GhostEffectOff();
                yield return dashdelay2;

                isDash = false;
                dashCount = 4;
                StartCoroutine(DashReset());
            }

            yield return null;
        }
    }


    protected override IEnumerator DashReset()
    {
        GhostEffectOff();
        rb.gravityScale = 2;
        dashReset = true;

        while (transform.position.x > dashRoot[0].x)
        {
            transform.position = Vector3.MoveTowards(transform.position, dashRoot[0], dashspeed * Time.deltaTime);

            if (transform.position == dashRoot[0])
            {
                dashReset = false;
                isGround = true;
                gm.nowDash = false;
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
