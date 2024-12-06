using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Toggle toggleSpeed_Pause;
    public Toggle toggleSpeed_x1;
    public Toggle toggleSpeed_x2;
    public Toggle toggleSpeed_x3;

    public TextMeshProUGUI timerMinutesDisplay;
    public TextMeshProUGUI timerHoursDisplay;
    public TextMeshProUGUI DayCounterDisplay;
    public float timer = 0f;
    private int day = 1;
    public int hour = 0;
    void Start()
    {
        DayCounterDisplay.text = day.ToString();
        toggleSpeed_Pause.onValueChanged.AddListener(delegate {
            if (toggleSpeed_Pause.isOn)
            {
                Time.timeScale = 0;
            }
        });
        toggleSpeed_x1.onValueChanged.AddListener(delegate {
            if (toggleSpeed_x1.isOn)
            {
                Time.timeScale = 1;
            }
        });
        toggleSpeed_x2.onValueChanged.AddListener(delegate {
            if (toggleSpeed_x2.isOn)
            {
                Time.timeScale = 2;
            }
        });
        toggleSpeed_x3.onValueChanged.AddListener(delegate {
            if (toggleSpeed_x3.isOn)
            {
                Time.timeScale = 10;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime*10;
        Debug.Log(timer);
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
                DayCounterDisplay.text = day.ToString();
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
