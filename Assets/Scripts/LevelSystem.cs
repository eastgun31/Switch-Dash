using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class LevelSystem : MonoBehaviour
{
    [SerializeField] private int level = 0;
    [SerializeField] private float nowScore = 0;
    [SerializeField] private float scoreValue = 0;
    [SerializeField] private Texture2D[] levelTextures = new Texture2D[3];
    [SerializeField] private GameObject bg;
    [SerializeField] private int temp;

    public UnityEvent levelup;
    GameManager gm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = GameManager.instance;
        level = 0;
        scoreValue = 3;
        temp = 30;

        LevelUp();
    }

    // Update is called once per frame
    void Update()
    {
        gm.gameScore += Time.deltaTime * scoreValue;
        nowScore += Time.deltaTime;

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

        StageUp();

        levelup.Invoke();
    }

    private void StageUp()
    {
        if (level == 3)
        {
            bg.GetComponent<Renderer>().material.mainTexture = levelTextures[1];
        }
        else if (level == 5)
        {
            bg.GetComponent<Renderer>().material.mainTexture = levelTextures[2];
        }
    }
}
