using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleM : PlayerController
{
    protected override IEnumerator Dash()
    {

        while (isDash)
        {
            if (transform.position.x < dashRoot[1].x)
                transform.position = Vector3.MoveTowards(transform.position, dashRoot[1], dashspeed * Time.deltaTime);
            else if (transform.position.x < dashRoot[2].x)
                transform.position = Vector3.MoveTowards(transform.position, dashRoot[2], dashspeed * Time.deltaTime);
            else if (transform.position.x < dashRoot[3].x)
                transform.position = Vector3.MoveTowards(transform.position, dashRoot[3], dashspeed * Time.deltaTime);

            if (transform.position == dashRoot[3])
            {
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
        rb.gravityScale = 5;
        dashReset = true;
        anim.SetBool(run, true);

        while (transform.position.x > dashRoot[0].x)
        {
            transform.position = Vector3.MoveTowards(transform.position, dashRoot[0], dashspeed * Time.deltaTime);

            if (transform.position == dashRoot[0])
            {
                dashReset = false;
                isGround = true;
                drawPooling.SetDraw();
            }

            yield return null;
        }
    }




    protected override void Die()
    {
        Debug.Log("Circle Die");
    }
}
