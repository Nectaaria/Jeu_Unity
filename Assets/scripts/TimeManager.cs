using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance { get; private set; }

    [SerializeField, Tooltip("Durée de la journée en minutes")]
    private float dayDuration = 4f;

    [SerializeField] private Toggle toggleSpeed_Pause;
    [SerializeField] private Toggle toggleSpeed_x1;
    [SerializeField] private Toggle toggleSpeed_x2;
    [SerializeField] private Toggle toggleSpeed_x3;

    [SerializeField] private TextMeshProUGUI timerMinutesDisplay;
    [SerializeField] private TextMeshProUGUI timerHoursDisplay;
    [SerializeField] private TextMeshProUGUI dayCounterDisplay;

    public int[] timeArray = new int[3]; //array avec Jour/heure/minute du temps ingame
    private float timeElapsed;

    private float minute = 0;
    private int hour = 0; // uniquement utilisées pour le display

    public float gameMinToRealSec;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeTimer()
    {
        Debug.Log("TimeManager initialisé");
        TimeManager Instance = instance;
    }

    private void Awake()
    {
        Debug.Log("Awake appelé");
        gameMinToRealSec = dayDuration * 60 / 1440;
    }

    void Start()
    {
        timeArray[0] = 1;
        timeArray[1] = hour;
        timeArray[2] = (int)minute;

        dayCounterDisplay.text = "Day : " + timeArray[0].ToString();
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
        timeElapsed += Time.deltaTime * 1440 / (dayDuration * 60);
        timeArray[2] = (int)timeElapsed;
        minute += Time.deltaTime * 1440 / (dayDuration * 60);
        //Debug.Log("minutes = " + timeArray[2]);
        //Debug.Log("heures = " + timeArray[1]);

        if (minute < 9.5f)
        {
            timerMinutesDisplay.text = "0" + Mathf.Round(minute).ToString();
        }
        else
        {
            timerMinutesDisplay.text = Mathf.Round(minute).ToString();
        }

        if (minute >= 59.5f)
        {
            minute = 0;
            hour += 1;
            timeArray[1] += 1;
            if (hour == 24)
            {
                hour = 0;
                timeArray[0] += 1;
                dayCounterDisplay.text = "Day : " + timeArray[0].ToString();
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