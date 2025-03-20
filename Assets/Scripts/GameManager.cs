using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int modelIndex = 0;
    public float worldSpeed = 3f;
    public SpriteRenderer[] shapes = new SpriteRenderer[3];
    public Color[] colors = new Color[3];


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    void Start()
    {
        modelIndex = 0;

        for(int i = 0; i < shapes.Length; i++)
            colors[i] = shapes[i].color;

       
    }
}
