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
        
    }
    
}
