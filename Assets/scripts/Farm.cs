using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{
    [SerializeField] int foodBuff = 1;

    //permanent food buff when farm is built
    private void Awake()
    {
        GameObject.Find("Bush").GetComponent<WorkPlace>().amountBonus += foodBuff;
    }
}
