using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleData", menuName = "Scriptable Objects/ObstacleData")]
public class ObstacleData : ScriptableObject
{
    [SerializeField] private List<GameObject> _obsModels = new List<GameObject>();
    public List<GameObject> obsModels { get { return _obsModels; } }

}
