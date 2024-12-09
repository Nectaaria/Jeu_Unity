using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropdownManager : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown;

    private List<GameObject> population;

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

    private List<GameObject> farmers = new List<GameObject>();
    private List<GameObject> builders = new List<GameObject>();
    private List<GameObject> miners = new List<GameObject>();
    private List<GameObject> lumberjacks = new List<GameObject>();
    private List<GameObject> wanderers = new List<GameObject>();

    [SerializeField] List<GameObject> listFox = new List<GameObject>();


    void Start()
    {
        closeInfo.onClick.AddListener(CloseInfoPanel);
        dropdown.onValueChanged.AddListener(OnFoxSelected);
        PopulationManager pop = popManager.GetComponent<PopulationManager>();
        listFox = pop.foxes;

        UpdateDropdown(listFox);//APPELER CES METHODES A CHAQUE MAJ DE LA POPULATION


    }

    void Update()
    {

    }

    void UpdateDropdown(List<GameObject> listFox)
    {
        dropdown.options.Clear();
        List<string> foxNames = new List<string>();//AJOUTER UN ATTRIBUT NAME AUX RENARDS
        foreach (GameObject fox in listFox)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData(fox.name));
        }
    }

    private void OnFoxSelected(int index)
    {
        if (index >= 0 && index < listFox.Count)
        {
            GameObject selectedFox = listFox[index];

            foxNameText.text = selectedFox.name;

            // Afficher le panel

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
