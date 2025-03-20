using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawColor : MonoBehaviour
{
    public bool noDraw = false;
    public SpriteRenderer _color;
    private Vector2 firstScale = new Vector2(1, 2);
    private Vector2 targetScale = new Vector2(6, 2);
    private Vector3 targetPos = new Vector3(-10, -1.75f, 0);
    private float scaleSpeed = 3f;
    private string target = "Target";

    private void OnEnable()
    {
        StartCoroutine(SizeUp());
    }

    private void Start()
    {
        //_color = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(noDraw)
        {
            StopAllCoroutines();
            transform.position = Vector2.MoveTowards
                (transform.position, targetPos, GameManager.instance.worldSpeed * Time.deltaTime);

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
            transform.localScale += new Vector3(scaleSpeed * Time.deltaTime, 0, 0);
            yield return null;
        }
    }

}
