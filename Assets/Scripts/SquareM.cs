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
                dashCount = 0;
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
        isInvincible = true;

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

                yield return indivildelay;

                isInvincible = false;
            }

            yield return null;
        }
    }


    //protected override IEnumerator Hit()
    //{
    //    if (isInvincible)
    //    {
    //        yield break;
    //    }

    //    Debug.Log("Hit");
    //    isInvincible = true;

    //    int blinkCount = 3;
    //    for (int i = 0; i < blinkCount; i++)
    //    {
    //        _sprite.color = _sprite.color + new Color(0, 0, 0, -1f);
    //        yield return indivildelay;
    //        _sprite.color = _sprite.color + new Color(0, 0, 0, 1f);
    //        yield return indivildelay;
    //    }

    //    isInvincible = false;
    //}
}
