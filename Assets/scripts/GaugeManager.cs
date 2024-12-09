using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeManager : MonoBehaviour
{
    //Prosperity 
    public Slider prosperityGauge;//Slider for the prosperity gauge

    public float minProsperity = 0f;
    public float maxProsperity = 100f;

    public float currentProsperity;
    public bool hasMuseum = false;
    public bool hasLibrary = false;

    // Start is called before the first frame update
    public void Start()
    {
        currentProsperity = minProsperity;
        prosperityGauge.minValue=minProsperity;
        prosperityGauge.maxValue=maxProsperity;
        prosperityGauge.value=currentProsperity;
        
    }

    // Update is called once per frame
    void Update()
    {
        IncreaseProsperity();
    }
    
    //if there's a museum or a library, the prosperity increases
    public void IncreaseProsperity()
    {
        if (hasLibrary)
        {
            currentProsperity += 10;
            prosperityGauge.value = currentProsperity;
            hasLibrary = false; 
        }
        if (hasMuseum)
        {
            currentProsperity += 20;
            prosperityGauge.value = currentProsperity;
            hasMuseum = false;
        }
        
    }
    
}
