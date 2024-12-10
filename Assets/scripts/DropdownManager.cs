using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropdownManager : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown;

    [SerializeField] private GameObject infoPanel;
    [SerializeField] private GameObject ChangeJobButton;

    [SerializeField] private Button closeInfo;

    [SerializeField] private TMP_Text foxJobText;
    [SerializeField] private GameObject changeJobButtonsLayout;

    [SerializeField] private Button learnMason;
    [SerializeField] private Button learnFarmer;
    [SerializeField] private Button learnLumberjack;
    [SerializeField] private Button learnMiner;
    private GameObject selectedFox;

    private Fox fox;

    [SerializeField] List<GameObject> listFox = new List<GameObject>();

    private PopulationManager pop;


    void Start()
    {
        closeInfo.onClick.AddListener(CloseInfoPanel);
        dropdown.onValueChanged.AddListener(OnFoxSelected);

        learnLumberjack.onClick.AddListener(() => ChangeJob("lumberjack"));
        learnMason.onClick.AddListener(() => ChangeJob("mason"));
        learnFarmer.onClick.AddListener(() => ChangeJob("farmer"));
        learnMiner.onClick.AddListener(() => ChangeJob("miner"));

        UpdateDropdown();
    }

    void Update()
    {
        
    }

    public void UpdateDropdown()
    {
        dropdown.options.Clear();
        listFox.Clear();
        listFox = PopulationManager.instance.foxes;
        foreach (GameObject fox in listFox)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData(fox.GetComponent<Fox>().job));
        }
    }

    private void OnFoxSelected(int index)
    {
        if (index >= 0 && index < listFox.Count)
        {
            selectedFox = listFox[index];
            foxJobText.text = selectedFox.GetComponent<Fox>().job;

            infoPanel.SetActive(true);
        }
        else
        {
            CloseInfoPanel();
        }
    }
    void CloseInfoPanel()
    {
        infoPanel.SetActive(false);
        changeJobButtonsLayout.SetActive(false);
    }
    void ChangeJob(string newJob)
    {
        selectedFox.GetComponent<Fox>().job = newJob;
        foxJobText.text = selectedFox.GetComponent<Fox>().job;
    }
}
