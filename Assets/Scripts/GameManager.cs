using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int modelIndex = 0;
    public int hp = 0;
    public float worldSpeed = 0;
    public float gameScore = 0;
    public SpriteRenderer[] shapes = new SpriteRenderer[3];
    public Color[] colors = new Color[3];
    public bool nowDash = false;
    public bool holeTrapActive = false;
    public bool nowLevelUp = false;


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    void Start()
    {
        gameScore = 0;
        modelIndex = 0;
        hp = 3;
        //worldSpeed = 0.5f;

        for (int i = 0; i < shapes.Length; i++)
            colors[i] = shapes[i].color;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }
}
