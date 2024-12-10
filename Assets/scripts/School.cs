using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class School : MonoBehaviour, IWorkable
{
    [SerializeField] float expAmount = 1f;

    //work at school by progressing a gauge. When gauge is filled, change job
    void IWorkable.Work(Fox fox)
    {
        fox.schoolProgression += expAmount;
        if(fox.schoolProgression >= 100)
        {
            fox.schoolProgression = 0;
            fox.job = fox.nextJob;
            fox.nextJob = null;
        }
    }
}
