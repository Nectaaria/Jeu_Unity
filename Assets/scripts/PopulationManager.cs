using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PopulationManager : MonoBehaviour
{
    public static PopulationManager instance;

    private float lastBornDay = 0;
    [SerializeField] float bornDelay = 3;
    [SerializeField] GameObject populationPrefab;
    [SerializeField] List<Transform> spawnPoints;

    [SerializeField] private DropdownManager dropdownManager;

    public List<GameObject> foxes;

    private void Start()
    {
        dropdownManager = GetComponent<DropdownManager>();
    }

    PopulationManager()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        //Instantiate a fox each X delay and add the fox in a list
        if (TimeManager.instance.timeArray[0] > lastBornDay + bornDelay)
        {
            GameObject fox = Instantiate(populationPrefab, spawnPoints[Random.Range(0, spawnPoints.Count - 1)].position, Quaternion.identity, transform);
            foxes.Add(fox);
            lastBornDay = TimeManager.instance.timeArray[0];
        }
    }
}
