using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [SerializeField, Tooltip("Durée de la journée en minutes")]
    private float dayDuration = 4f;

    [SerializeField] private Toggle toggleSpeed_Pause;
    [SerializeField] private Toggle toggleSpeed_x1;
    [SerializeField] private Toggle toggleSpeed_x2;
    [SerializeField] private Toggle toggleSpeed_x3;

    [SerializeField] private TextMeshProUGUI timerMinutesDisplay;
    [SerializeField] private TextMeshProUGUI timerHoursDisplay;
    [SerializeField] private TextMeshProUGUI dayCounterDisplay;
    
    public float timer = 0f;
    public int day = 1;
    public int hour = 0;
    
    void Start()
    {
        dayCounterDisplay.text = "Day : " + day.ToString();
        toggleSpeed_Pause.onValueChanged.AddListener(delegate
        {
            if (toggleSpeed_Pause.isOn)
            {
                Time.timeScale = 0;
            }
        });
        toggleSpeed_x1.onValueChanged.AddListener(delegate
        {
            if (toggleSpeed_x1.isOn)
            {
                Time.timeScale = 1;
            }
        });
        toggleSpeed_x2.onValueChanged.AddListener(delegate
        {
            if (toggleSpeed_x2.isOn)
            {
                Time.timeScale = 2;
            }
        });
        toggleSpeed_x3.onValueChanged.AddListener(delegate
        {
            if (toggleSpeed_x3.isOn)
            {
                Time.timeScale = 10;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * 1440 / (dayDuration * 60);
        //Debug.Log(timer);

        if (Mathf.Round(timer) < 10)
        {
            timerMinutesDisplay.text = "0" + Mathf.Round(timer).ToString();
        }
        else
        {
            timerMinutesDisplay.text = Mathf.Round(timer).ToString();
        }

        if (Mathf.Round(timer) >= 60f)
        {
            timer = 0f;
            hour += 1;
            if (hour == 24)
            {
                hour = 0;
                day += 1;
                dayCounterDisplay.text = "Day : " + day.ToString();
            }
            if (hour < 10)
            {
                timerHoursDisplay.text = "0" + hour.ToString();
            }
            else
            {
                timerHoursDisplay.text = hour.ToString();

            }
        }
    }
}