using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPooling : MonoBehaviour
{
    [SerializeField] private GameObject drawPrefab;
    [SerializeField] private GameObject ghostPool;
    [SerializeField] private List<GameObject> drawPrefabs = new List<GameObject>();
    [SerializeField] private List<DrawColor> drawcolor = new List<DrawColor>();

    private Vector3 startpos = new Vector3(-3, -1.45f, 0);
    GameManager gm;

    void Start()
    {
        gm = GameManager.instance;

        for (int i = 0; i < 10; i++)
        {
            GameObject obj = Instantiate(drawPrefab, transform);
            drawPrefabs.Add(obj);
            drawcolor.Add(obj.GetComponent<DrawColor>());
            drawcolor[i]._color = obj.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();

            obj.SetActive(false);
        }
    }

    public void SetDraw()
    {
        for (int i = 0; i < drawPrefabs.Count; i++)
        {
            if (!drawPrefabs[i].activeSelf)
            {
                drawPrefabs[i].SetActive(true);
                drawPrefabs[i].transform.position = startpos;
                drawcolor[i]._color.color = gm.colors[gm.modelIndex];
                
                return;
            }
        }

        GameObject newObj = Instantiate(drawPrefab, transform);
        newObj.SetActive(true);
        newObj.transform.position = startpos;
        drawPrefabs.Add(newObj);
        drawcolor.Add(newObj.GetComponent<DrawColor>());

        newObj.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color 
            = gm.colors[gm.modelIndex];
    }

    public void MoveDraw()
    {
        for(int i = 0; i < drawPrefabs.Count; i++)
        {
            if (drawPrefabs[i] != null && drawPrefabs[i].activeSelf)
                drawcolor[i].noDraw = true;
            else if (drawPrefabs[i] == null)
                Debug.LogWarning("GameObject has been destroyed: " + i);
        }
    }
}
