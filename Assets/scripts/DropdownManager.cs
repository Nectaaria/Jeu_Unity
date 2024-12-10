using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropdownManager : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown;

    //private List<GameObject> population;

    [SerializeField] private GameObject mine;
    [SerializeField] private GameObject forest;
    [SerializeField] private GameObject bush;
    [SerializeField] private GameObject plain;

    [SerializeField] private GameObject infoPanel;
    [SerializeField] private GameObject ChangeJobButton;
    [SerializeField] private GameObject popManager;

    [SerializeField] private Button closeInfo;

    [SerializeField] private TMP_Text foxNameText;
    [SerializeField] private TMP_Text foxJobText;

    [SerializeField] private Fox fox;

    [SerializeField] List<GameObject> listFox = new List<GameObject>();
    private PopulationManager pop;


    void Start()
    {
        closeInfo.onClick.AddListener(CloseInfoPanel);
        dropdown.onValueChanged.AddListener(OnFoxSelected);

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
            GameObject selectedFox = listFox[index];

            foxNameText.text = selectedFox.name;

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
    }
}
