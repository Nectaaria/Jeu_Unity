using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropdownManager : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown;

    private List<GameObject> population;

    [SerializeField] private GameObject mine;
    [SerializeField] private GameObject forest;
    [SerializeField] private GameObject bush;
    [SerializeField] private GameObject plain;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private GameObject builderButtons;
    [SerializeField] private GameObject wandererButtons; // ces fields apparaissent pas 

    [SerializeField] private TMP_Text foxNameText;
    //[SerializeField] private TMP_Text foxAgeText;//ajouter un attribut pour l'age ? ou calculer ici ?
    [SerializeField] private TMP_Text foxJobText;

    private List<GameObject> farmers = new List<GameObject>();
    private List<GameObject> builders = new List<GameObject>();
    private List<GameObject> miners = new List<GameObject>();
    private List<GameObject> lumberjacks = new List<GameObject>();
    private List<GameObject> wanderers = new List<GameObject>();

    [SerializeField] List<Fox> listFox = new List<Fox>();



    void Start()
    {
        SortByJob();
        UpdateDropdown(farmers);
        UpdateDropdown(builders);
        UpdateDropdown(miners);
        UpdateDropdown(lumberjacks);
        UpdateDropdown(wanderers);//APPELER CES METHODES A CHAQUE MAJ DE LA POPULATION

        dropdown.onValueChanged.AddListener(OnFoxSelected);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SortByJob()
    {
        /* foreach (GameObject pop in population)//trie les renards par métiers selon leur workplace mais mieux d'ajouter des string job pour l'avoir direct ? 
         {
             Fox fox = pop.GetComponent<Fox>();
             if (fox.workplace == mine)
             {
                 miners.Add(pop);
             }
             if (fox.workplace == forest)
             {
                 lumberjacks.Add(pop);
             }
             if (fox.workplace == bush)
             {
                 farmers.Add(pop);
             }
             if (fox.workplace == plain)//Ajouter plain en workplace pour les builders
             {
                 builders.Add(pop);
             }
             if (fox.workplace == null)
             {
                 wanderers.Add(pop);
             }
    }*/
}

    void UpdateDropdown(List<GameObject> listFox)
    {
        List<string> foxNames = new List<string>();//AJOUTER UN ATTRIBUT NAME AUX RENARDS
        foreach (GameObject fox in listFox)
        {
            foxNames.Add(fox.name);
        }
        dropdown.AddOptions(foxNames);
    }

    private void OnFoxSelected(int index)
    {
        if (index >= 0 && index < listFox.Count)
        {
            Fox selectedFox = listFox[index];

            foxNameText.text = selectedFox.name;
            //foxAgeText.text = selectedFox.age;
            //foxJobText.text = selectedFox.workplace != null ? selectedFox.workplace.name : "Wanderer";

            // Afficher le panel
            infoPanel.SetActive(true);
            /*if (selectedFox.workplace == plain)
            {
                builderButtons.SetActive(true);
                wandererButtons.SetActive(false);
            }
            else if (selectedFox.workplace == null)
            {
                builderButtons.SetActive(false);
                wandererButtons.SetActive(true);
            }
            else
            {
                builderButtons.SetActive(false); 
                wandererButtons.SetActive(false);
            }*/
        }
        else
        {
            infoPanel.SetActive(false);
        }
    }
}
