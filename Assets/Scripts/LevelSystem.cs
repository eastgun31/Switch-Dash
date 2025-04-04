using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using TMPro;

public class LevelSystem : MonoBehaviour
{
    [SerializeField] private int level = 0;
    [SerializeField] private float nowScore = 0;
    [SerializeField] private float scoreValue = 0;
    [SerializeField] private Texture2D[] levelTextures = new Texture2D[3];
    [SerializeField] private GameObject bg;
    [SerializeField] private int temp;
    [SerializeField] private TextMeshProUGUI scoreTextM;
    [SerializeField] private SpriteRenderer fadeInOut;

    public UnityEvent levelup;
    private string scoreText = "Score: ";
    GameManager gm;
    WaitForSeconds delay = new WaitForSeconds(0.1f);

    void Start()
    {
        gm = GameManager.instance;
        level = 0;
        scoreValue = 3;
        temp = 30;

        //LevelUp();
    }

    void Update()
    {
        if (gm.nowLevelUp)
            return;

        gm.gameScore += Time.deltaTime * scoreValue;
        nowScore += Time.deltaTime;

        scoreTextM.text = scoreText + ((int)gm.gameScore).ToString();

        if (nowScore >= temp)
        {
            nowScore = 0;
            LevelUp();
        }
    }

    public void LevelUp()
    {
        level++;

        if(gm.worldSpeed < 3f)
            gm.worldSpeed += 0.5f;

        if(level == 3 || level == 5)
            StartCoroutine(StageUp());

        StageUp();

        levelup.Invoke();
    }

    private IEnumerator StageUp()
    {
        gm.nowLevelUp = true;
        Color c = fadeInOut.color;

        for (float i = 0; i <= 1; i += 0.1f)
        {
            c.a = i;
            fadeInOut.color = c;
            yield return delay;
        }

        if (level == 3)
            bg.GetComponent<Renderer>().material.mainTexture = levelTextures[1];
        else if (level == 5)
            bg.GetComponent<Renderer>().material.mainTexture = levelTextures[2];

        for (float i = 1; i >= 0; i -= 0.1f)
        {
            c.a = i;
            fadeInOut.color = c;
            yield return delay;
        }

        gm.nowLevelUp = false;
    }
}
