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
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CheckEndGame();
        if (!CheckEndGame())//if the game isn't over
        {
            
        }
    }

    void KillFox()
    {
        //if the fox hasn't eaten today, it dies
        //if the fox is too old, it dies
        
    }

    void GiveBirth()
    {
        
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
