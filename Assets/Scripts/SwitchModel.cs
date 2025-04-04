using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwitchModel : MonoBehaviour
{
    public GameObject[] objs = new GameObject[3];
    public Button[] buttons = new Button[3];
    public TextMeshProUGUI[] texts = new TextMeshProUGUI[3];
    [SerializeField] private int offIndex = 0;
    [SerializeField] private bool canSwitch = true;
    [SerializeField] private float delayTime;

    private Vector2 gap = new Vector2(0, 1f);
    GameManager gm;
    WaitForSeconds delay = new WaitForSeconds(2f);

    private void Start()
    {
        gm = GameManager.instance;
        canSwitch = true;
        delayTime = 0;
    }
    public void SwitchCircle()
    {
        if (gm.nowDash || !canSwitch)
            return;
        StartCoroutine(SwitchDelay());

        Debug.Log("Switch Model to Circle");

        buttons[offIndex].transform.position = buttons[0].transform.position;
        buttons[offIndex].gameObject.SetActive(true);

        ResetObj(0);
    }
    public void SwitchTriangle()
    {
        if (gm.nowDash || !canSwitch)
            return;
        StartCoroutine(SwitchDelay());
        Debug.Log("Switch Model to Triangle");

        buttons[offIndex].transform.position = buttons[1].transform.position;
        buttons[offIndex].gameObject.SetActive(true);

        ResetObj(1);
    }
    public void SwitchSquare()
    {
        if (gm.nowDash || !canSwitch)
            return;
        StartCoroutine(SwitchDelay());
        Debug.Log("Switch Model to Square");
        buttons[offIndex].transform.position = buttons[2].transform.position;
        buttons[offIndex].gameObject.SetActive(true);
        
        ResetObj(2);
        objs[2].transform.GetChild(0).transform.position = NowPos() + gap;
    }

    private void ResetObj(int n)
    {
        objs[1].transform.rotation = Quaternion.Euler(0, 0, 0);
        objs[2].transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);

        for(int i = 0; i < 3; i++)
        {
            if(i == n)
            {
                objs[i].gameObject.SetActive(true);
                objs[i].transform.position = NowPos() + gap;
                buttons[i].gameObject.SetActive(false);
                gm.modelIndex = i;
                offIndex = i;
            }
            else
            {
                objs[i].gameObject.SetActive(false);
            }
        }
    }

    private Vector2 NowPos()
    {
        switch(gm.modelIndex)
        {
            case 0:
                return objs[0].transform.position;
            case 1:
                return objs[1].transform.position;
            case 2:
                return objs[2].transform.position;
            default:
                return objs[0].transform.position;
        }
    }

    private IEnumerator SwitchDelay()
    {
        canSwitch = false;
        delayTime = 3f;

        foreach (var delaytext in texts)
        {
            delaytext.gameObject.SetActive(true);
        }

        while (delayTime > 0)
        {
            delayTime -= Time.deltaTime;

            int displayTime = (int)delayTime;
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].text = displayTime.ToString();
            }

            yield return null;
        }

        foreach (var delaytext in texts)
        {
            delaytext.gameObject.SetActive(false);
        }

        canSwitch = true;
        delayTime = 0;
    }
}
