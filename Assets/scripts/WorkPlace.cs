using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkPlace : MonoBehaviour, IWorkable
{
    [SerializeField] int amountPerSec = 1;
    public int amountBonus = 0;
    [SerializeField] string type;
    Coroutine coroutine;

    //Add a certain amount of wood/stone/food to the inventory when a fox is working
    void IWorkable.Work(Fox fox)
    {
        if (coroutine != null)
            return;
        coroutine = StartCoroutine(CollectCooldown());
        coroutine = null;
    }

    IEnumerator CollectCooldown()
    {
        yield return new WaitForSeconds(1);
        Game.gameInstance.inventory[type] += amountPerSec + amountBonus;
    }
}
