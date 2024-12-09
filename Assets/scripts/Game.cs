using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] GaugeManager gaugeManager;
    [SerializeField] PopulationManager populationManager;
    //Foxes
    public int nbFoxes;
    private List<int> foxesToDie;
    
    //World
    private int foodQuantity;
    private int woodQuantity;
    private int stoneQuantity;
    
    //End
    private string endGame;
    
    //Eating
    private bool hasEaten;
    private int nbFoxesNotEating;
    
    
    void Start()
    {
        
    }
    
    
    

    // Update is called once per frame
    void Update()
    {
        nbFoxes = PopulationManager.instance.foxes.Count;
        CheckEndGame();
        if (!CheckEndGame())//if the game isn't over
        {
            
            Eating();
            KillFox();
        }
        else
        {
            if(endGame == "Win")
            {
                //Display win panel
            }
            if(endGame == "Loose")
            {
                //Display loose panel
            }
        }
    }

    void KillFox()
    {
        //if the fox hasn't eaten today, it dies
        if (!hasEaten)
        {
            for (int i = nbFoxesNotEating - 1; i >= 0; i--)
            {
                Debug.Log("i "+i);
                int random = Random.Range(0, nbFoxes);
                var currentFox = PopulationManager.instance.foxes[random].GetComponent<Fox>();
                PopulationManager.instance.foxes.RemoveAt(random);
                Destroy(currentFox.gameObject); 
            }
        }
        
        //if the fox is too old, it dies
        for (int i = nbFoxes - 1; i >= 0; i--)
        {
            Debug.Log("i "+i);
            var currentFox = PopulationManager.instance.foxes[i].GetComponent<Fox>();
            if (TimeManager.instance.timeArray[0] > currentFox.bornDay + currentFox.daysToLive)
            {
                PopulationManager.instance.foxes.RemoveAt(i);
                Destroy(currentFox.gameObject); 
            }
        }
    }

    void Eating()
    {
        //if there's enough food for all the foxes
        if (nbFoxes <= foodQuantity)
        {
            hasEaten = true;
            foodQuantity -= nbFoxes;
        }
        //if there's not enough food for all the foxes
        if(nbFoxes != foodQuantity )
        {
            hasEaten = false ;
            nbFoxesNotEating = 0; //resetting the value of foxes that haven't eaten
            nbFoxesNotEating = nbFoxes - foodQuantity; //storing a value of foxes that haven't eaten
            foodQuantity = nbFoxes - nbFoxesNotEating; //updating the food quantity
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
}
