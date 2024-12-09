using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PopulationManager : MonoBehaviour
{
    public static PopulationManager instance;

    private float lastBornDay = 0;
    [SerializeField] float bornDelay = 3;
    [SerializeField] GameObject populationPrefab;
    [SerializeField] List<Transform> spawnPoints;

    public List<GameObject> foxes;

    PopulationManager()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (TimeManager.instance.timeArray[0] > lastBornDay + bornDelay)
        {
            GameObject fox = Instantiate(populationPrefab, spawnPoints[Random.Range(0, spawnPoints.Count - 1)].position, Quaternion.identity, transform);
            foxes.Add(fox);
            lastBornDay = TimeManager.instance.timeArray[0];
        }
    }
}
