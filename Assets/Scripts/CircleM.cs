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
                dashCount = 0;
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
        isInvincible = true;
        anim.SetBool(run, true);

        while (transform.position.x > dashRoot[0].x)
        {
            transform.position = Vector3.MoveTowards(transform.position, dashRoot[0], dashspeed * Time.deltaTime);

            if (transform.position == dashRoot[0])
            {
                dashReset = false;
                isGround = true;
                gm.nowDash = false;
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
