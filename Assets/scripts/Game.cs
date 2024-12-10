using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Game : MonoBehaviour
{
    public static Game gameInstance { get; private set; }
    [SerializeField] GaugeManager gaugeManager;
    [SerializeField] PopulationManager populationManager;
    //UI
    [SerializeField] Canvas endGameCanvas;
    [SerializeField] Canvas uiCanvas;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject loosePanel;

    [SerializeField] private DropdownManager dropdownManager;


    //Foxes
    public int nbFoxes;
    private List<int> foxesToDie;
    
    //World
    public Dictionary<string, int> inventory = new Dictionary<string, int>();
    
    //End
    private string endGame;
    
    //Eating
    private bool hasEaten;
    private int nbFoxesNotEating;

    private void Awake()
    {
        if (gameInstance == null)
        {
            gameInstance = this;
        }
    }


    void Start()
    {
        inventory.Add("wood", 0);
        inventory.Add("stone", 0);
        inventory.Add("food", 0);
        dropdownManager = GetComponent<DropdownManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("wood"+inventory["wood"]+"  "+ "stone" + inventory["stone"] + "  " + "food" + inventory["food"] + "  " );
        nbFoxes = PopulationManager.instance.foxes.Count;
        CheckEndGame();
        if (!CheckEndGame())//if the game isn't over
        {
            if (TimeManager.instance.timeArray[1] == 9)
            {
                for (int i = 0 ; i < nbFoxes; i++)
                {
                    var firsCurrentFox = PopulationManager.instance.foxes[i].GetComponent<Fox>();
                    firsCurrentFox.state = Fox.Task.working;
                }
            }

            if (TimeManager.instance.timeArray[1] == 18)
            {
                Eating();
                KillFox();
            }
            
        }
        else
        {
            EndGame(endGame);
        }
    }

    void KillFox()
    {
        //if the fox hasn't eaten today, it dies
        /*if (!hasEaten)
        {
            for (int i = nbFoxesNotEating - 1; i >= 0; i--)
            {
                Debug.Log("i "+i);
                int random = Random.Range(0, nbFoxes);
                var currentFox = PopulationManager.instance.foxes[random].GetComponent<Fox>();
                PopulationManager.instance.foxes.RemoveAt(random);
                Destroy(currentFox.gameObject);
            }
        }*/
        
        //if the fox is too old, it dies
        for (int i = nbFoxes - 1; i >= 0; i--)
        {
            var currentFox = PopulationManager.instance.foxes[i].GetComponent<Fox>();
            if (TimeManager.instance.timeArray[0] > currentFox.bornDay + currentFox.daysToLive)
            {
                PopulationManager.instance.foxes.RemoveAt(i);
                Destroy(currentFox.gameObject);
            }
        }
        dropdownManager.UpdateDropdown();
    }

    void Eating()
    {
        //if there's enough food for all the foxes
        if (nbFoxes <= inventory["food"])
        {
            hasEaten = true;
            inventory["food"] -= nbFoxes;
        }
        //if there's not enough food for all the foxes
        if(nbFoxes != inventory["food"])
        {
            hasEaten = false ;
            nbFoxesNotEating = 0; //resetting the value of foxes that haven't eaten
            nbFoxesNotEating = nbFoxes - inventory["food"]; //storing a value of foxes that haven't eaten
            inventory["food"] = nbFoxes - nbFoxesNotEating; //updating the food quantity
        }
        
    }

    bool CheckEndGame()
    {
        if (gaugeManager.currentProsperity >= gaugeManager.maxProsperity)
        {
            endGame = "Win";
            //Display win panel
            return true;
        }
        if (nbFoxes == 0)
        {
            endGame = "Loose";
            //Display loose panel
            return true;
        }

        return false;
    }

    void EndGame(string endGame)
    {
        endGameCanvas.enabled = true;
        uiCanvas.enabled = false;

        if (endGame == "Win")
        {
            winPanel.SetActive(true);
        }
        else if (endGame == "Loose")
        {
            loosePanel.SetActive(true);
        }
    }
}
