using UnityEngine;
using UnityEngine.UI;

public class SwitchModel : MonoBehaviour
{
    public GameObject[] objs = new GameObject[3];
    public Button[] buttons = new Button[3];
    [SerializeField] private int offIndex = 0;
    private Vector2 gap = new Vector2(0, 1f);
    GameManager gm;

    private void Start()
    {
        gm = GameManager.instance;
    }
    public void SwitchCircle()
    {
        Debug.Log("Switch Model to Circle");

        buttons[offIndex].transform.position = buttons[0].transform.position;
        buttons[offIndex].gameObject.SetActive(true);

        ResetObj(0);
    }
    public void SwitchTriangle()
    {
        Debug.Log("Switch Model to Triangle");

        buttons[offIndex].transform.position = buttons[1].transform.position;
        buttons[offIndex].gameObject.SetActive(true);

        ResetObj(1);
    }
    public void SwitchSquare()
    {
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
}
