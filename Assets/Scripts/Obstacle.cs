using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int obsIndex = 0;
    [SerializeField] private float objSpeed = 3.0f;
    [SerializeField] private Vector3 targetPos = new Vector3(-12, 3f, 0);
    [SerializeField] private ObstacleData obs;
    [SerializeField] private GameObject[] obsModel;
    [SerializeField] private int arrayCount = 0;

    [SerializeField] private ObstacleManager obsM;

    GameManager gm;

    void Awake()
    {
        arrayCount = obs.obsModels.Count;
        gm = GameManager.instance;
        obsModel = new GameObject[arrayCount];
    }

    void Update()
    {
        switch (obsIndex)
        {
            case 0:
            case 1:
                Obs_0();
                break;
        }
    }

    public void ObsModelOn()
    {
        obsIndex = Random.Range(0, arrayCount);

        if(obsModel[obsIndex] == null)
        {
            GameObject _obs = Instantiate(obs.obsModels[obsIndex], transform.position, Quaternion.identity, transform);
            obsModel[obsIndex] = _obs;
        }
        else
        {
            obsModel[obsIndex].SetActive(true);
        }

    }

    private void Obs_0()
    {
        transform.position = Vector2.MoveTowards
                (transform.position, targetPos, gm.worldSpeed * objSpeed * Time.deltaTime);

        if (transform.position.x <= targetPos.x)
        {
            obsModel[obsIndex].SetActive(false);
            transform.position = transform.parent.position;
            gameObject.SetActive(false);
        }    
    }

    public void DestroyObs()
    {

    }
}
