using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class prosperityGauje : MonoBehaviour
{

    [SerializeField] private Slider prosperityGauge;
    [SerializeField] private GameObject timeControl;
    public float prosperity;
    [SerializeField] private TimeManager timeManager;
    void Start()
    {
        timeManager = timeControl.GetComponent<TimeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        prosperity = timeManager.timeArray[2];
        prosperityGauge.value = prosperity / 60;
        //Debug.Log(prosperity);
    }
}
