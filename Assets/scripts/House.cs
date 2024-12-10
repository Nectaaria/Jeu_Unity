using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public bool isOccupied = false;

    public IEnumerator Sleep(float duration)
    {
        isOccupied = true;
        yield return new WaitForSeconds(duration);
        isOccupied = false;
    }
}
