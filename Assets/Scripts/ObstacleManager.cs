using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject sdItemPrefab;
    [SerializeField] private GameObject hItemPrefab;
    [SerializeField] private List<GameObject> obstacles = new List<GameObject>();
    [SerializeField] private List<Obstacle> _obstacles = new List<Obstacle>();
    [SerializeField] private ObstacleData obsData;
    [SerializeField] private float spawnTime = 0f;
    [SerializeField] private float spawnCool = 0f;
    [SerializeField] private int itemCoolCount = 0;
    [SerializeField] private Vector3[] itemSpawnPos = new Vector3[3];
    [SerializeField] private int obsTypes;

    GameManager gm;

    void Start()
    {
        gm = GameManager.instance;
        spawnCool = 7f;
        obsTypes = 1;

        for (int i = 0; i < 10; i++)
        {
            GameObject obj = Instantiate(obstaclePrefab, transform);
            obstacles.Add(obj);
            _obstacles.Add(obj.GetComponent<Obstacle>());
            obj.SetActive(false);
        }

        //StartCoroutine(_SpawnObj());
    }

    private void Update()
    {
        if(gm.nowLevelUp)
            return;

        spawnTime += Time.deltaTime;
        SpawnObj();
    }

    public void SetObstacle()
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            if (!obstacles[i].activeSelf)
            {
                obstacles[i].SetActive(true);
                _obstacles[i].ObsModelOn(obsTypes);
                return;
            }
        }
        GameObject newObj = Instantiate(obstaclePrefab, transform);
        newObj.SetActive(true);
        obstacles.Add(newObj);
        _obstacles.Add(newObj.GetComponent<Obstacle>());
        _obstacles[_obstacles.Count - 1].ObsModelOn(obsTypes);
    }

    private void SpawnObj()
    {
        if(spawnTime > spawnCool)
        {
            spawnTime = 0f;
            itemCoolCount++;

            if(itemCoolCount >= 4)
            {
                itemCoolCount = 0;
                int rand = Random.Range(0, 2);

                if (rand == 0)
                {
                    SpawnItem();
                }
                else
                    SetObstacle();
            }
            else
                SetObstacle(); 
        }
    }

    private void SpawnItem()
    {
        int rand = Random.Range(0, 3);

        if (rand == 0)
        {
            GameObject item = Instantiate(hItemPrefab, transform);
            int randPos = Random.Range(0, itemSpawnPos.Length);
            item.transform.position = itemSpawnPos[randPos];
        }
        else
        {
            GameObject item = Instantiate(sdItemPrefab, transform);
            int randPos = Random.Range(0, itemSpawnPos.Length);
            item.transform.position = itemSpawnPos[randPos];
        }
    }

    public void LevelUp()
    {
        if(spawnCool > 1f)
            spawnCool --;

        if (obsTypes < 3)
            obsTypes++;
    }

}
