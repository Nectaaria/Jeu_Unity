using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public bool isOccupied = false;

    //make a the house occupied because a fox is sleeping inside
    public IEnumerator Sleep(float duration)
    {
        isOccupied = true;
        yield return new WaitForSeconds(duration);
        isOccupied = false;
    }
}
