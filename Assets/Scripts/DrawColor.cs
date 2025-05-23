using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawColor : MonoBehaviour
{
    public bool noDraw = false;
    public SpriteRenderer _color;
    private Vector2 firstScale = new Vector2(1, 1);
    private Vector2 targetScale = new Vector2(6, 1);
    private Vector3 targetPos = new Vector3(-10, -1.45f, 0);
    private float scaleSpeed = 3f;

    GameManager gm;

    private void OnEnable()
    {
        gm = GameManager.instance;
        StartCoroutine(SizeUp());
    }


    private void FixedUpdate()
    {
        if(noDraw)
        {
            StopAllCoroutines();
            transform.position = Vector2.MoveTowards
                (transform.position, targetPos, gm.worldSpeed * scaleSpeed * Time.fixedDeltaTime);

            if(transform.position == targetPos)
            {
                noDraw = false;
                transform.localScale = firstScale;
                gameObject.SetActive(false);
            }    
        }
    }

    public IEnumerator SizeUp()
    {
        while (transform.localScale.x < targetScale.x)
        {
            transform.localScale += new Vector3(scaleSpeed * gm.worldSpeed * Time.deltaTime, 0, 0);
            
            yield return null;
        }
    }

}
