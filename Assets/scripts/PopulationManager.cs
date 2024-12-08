using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PopulationManager : MonoBehaviour
{
    private float lastBornDay = 0;
    [SerializeField] float bornDelay = 3;
    [SerializeField] GameObject populationPrefab;
    [SerializeField] List<Transform> spawnPoints;

    private void Update()
    {
        if (TimeManager.instance.timeArray[0] > lastBornDay + bornDelay)
        {
            Instantiate(populationPrefab, spawnPoints[Random.Range(0, spawnPoints.Count - 1)].position, Quaternion.identity, transform);
            lastBornDay = TimeManager.instance.timeArray[0];
        }
    }
}
