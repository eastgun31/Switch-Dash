using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int obsIndex = 0;
    [SerializeField] private float objSpeed = 3.5f;
    [SerializeField] private float _worldSpeed;
    [SerializeField] private Vector3 targetPos = new Vector3(-12, 3f, 0);
    [SerializeField] private Vector3 targetPos2 = new Vector3(-28, 3f, 0);
    [SerializeField] private Vector3 HolePos = new Vector3(-8, 0, 0);
    [SerializeField] private Vector3 platformtPos = new Vector3(23, -1.75f, 0);
    [SerializeField] private ObstacleData obs;
    [SerializeField] private GameObject[] obsModel;
    [SerializeField] private int arrayCount = 0;

    [SerializeField] private ObstacleManager obsM;
    [SerializeField] private GameObject platform;

    GameManager gm;

    void Awake()
    {
        arrayCount = obs.obsModels.Count;
        gm = GameManager.instance;
        obsModel = new GameObject[arrayCount];
    }

    void FixedUpdate()
    {
        if (gm.nowLevelUp && obsIndex != 4)
        {
            transform.position = targetPos;
        }

        switch (obsIndex)
        {
            case 0:
            case 1:
            case 3:
                BasicObs();
                break;
            case 2:
                objSpeed = 10f;
                FasterObs();
                break;
            case 4:
                HoleObs();
                break;
        }
    }

    public void ObsModelOn(int level, GameObject land)
    {
        obsIndex = Random.Range(0, level);

        if(obsIndex == 4 && gm.holeTrapActive)
            obsIndex = Random.Range(0, level - 1);

        if (obsModel[obsIndex] == null)
        {
            GameObject _obs = Instantiate(obs.obsModels[obsIndex], transform.position, Quaternion.identity, transform);
            obsModel[obsIndex] = _obs;
        }
        else
        {
            obsModel[obsIndex].SetActive(true);

            for (int i = 0; i < obsModel[obsIndex].transform.childCount; i++)
                obsModel[obsIndex].transform.GetChild(i).gameObject.SetActive(true);
        }

        if(obsIndex == 4)
        {
            platform = land;
            platform.transform.SetParent(transform);
            gm.holeTrapActive = true;
        }
    }

    private void BasicObs()
    {
        transform.position = Vector2.MoveTowards
                (transform.position, targetPos, gm.worldSpeed * objSpeed * Time.fixedDeltaTime);

        if (transform.position.x <= targetPos.x)
        {
            objSpeed = 3f;
            obsModel[obsIndex].SetActive(false);
            transform.position = transform.parent.position;
            gameObject.SetActive(false);
        }    
    }

    private void FasterObs()
    {
        transform.position = Vector2.MoveTowards
                (transform.position, targetPos, gm.worldSpeed * objSpeed * Time.fixedDeltaTime);

        if (transform.position.x <= targetPos.x)
        {
            objSpeed = 3f;
            obsModel[obsIndex].SetActive(false);
            transform.position = transform.parent.position;
            gameObject.SetActive(false);
        }
    }


    private void HoleObs()
    {
        transform.position = Vector2.MoveTowards
                (transform.position, targetPos2, gm.worldSpeed * objSpeed * Time.fixedDeltaTime);

        if (platform != null && transform.position.x <= HolePos.x)
        {
            platform.transform.position = platformtPos;
            platform.transform.SetParent(null);
            platform = null;
        }

        if (transform.position.x <= targetPos2.x)
        {
            objSpeed = 3f;
            obsModel[obsIndex].SetActive(false);
            transform.position = transform.parent.position;
            gameObject.SetActive(false);
        }
    }

}
