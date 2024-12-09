using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] GaugeManager gaugeManager;
    //Foxes
    private int nbFoxes = 0;
    
    //World
    private int foodQuantity;
    private int woodQuantity;
    private int stoneQuantity;
    
    //End
    private string endGame;
    
    //Eating
    private string hasEaten;
    private int nbFoxesNotEating;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
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
        if (hasEaten == "Partially")
        {
            nbFoxes -= nbFoxesNotEating;
            //choose randomly which foxes die
        }
        if (hasEaten == "No")
        {
            nbFoxes -= nbFoxes;
        }
        //if the fox is too old, it dies
        
    }

    void Eating()
    {
        //if there's enough food for all the foxes
        if (nbFoxes == foodQuantity)
        {
            hasEaten = "Yes";
            foodQuantity -= nbFoxes;
        }
        //if there's not enough food for all the foxes
        if(nbFoxes != foodQuantity || foodQuantity > 0)
        {
            hasEaten = "Partially";
            nbFoxesNotEating = 0; //resetting the value of foxes that haven't eaten
            nbFoxesNotEating = nbFoxes - foodQuantity; //storing a value of foxes that haven't eaten
            foodQuantity = nbFoxes - nbFoxesNotEating; //updating the food quantity
        }
        //if there's no food
        if (foodQuantity == 0)
        {
            hasEaten = "No";
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
