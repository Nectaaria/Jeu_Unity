using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fox : MonoBehaviour
{
    private int age = 0;
    public bool isTired = false;
    public enum Task { wandering, working, sleeping };
    public Task state = Task.wandering;
    [SerializeField] float wanderRange = 5f;

    private NavMeshAgent agent;
    [SerializeField] GameObject workplace;

    [SerializeField] float workDuration;
    [SerializeField] float sleepDuration;
    private float depressionTimer;

    Coroutine coroutine=null;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        switch(state)
        {
            case Task.wandering:
                Wander();
                break;

            case Task.working:
                Work();
                break;
        }
    }

    private void Wander()
    {
        if (coroutine != null)
            return;

        if (isTired)
        {
            //parcours toutes les maisons en quête d'une maison vide pour dodo et si trouve:
            /*coroutine = StartCoroutine(stateAterTime(sleepDuration, Task.wandering));
            coroutine = null;*/
        }
        else
        {
            if (agent.remainingDistance < 1)
                agent.SetDestination(RandomWanderPosition());
        }
    }

    private void Work()
    {
        if (Vector3.Distance(transform.position, workplace.transform.position) < 1)
        {

            //workplace.GetComponent<script>().Harvest();
            if (coroutine != null)
                return;
            coroutine =  StartCoroutine( stateAterTime(workDuration, Task.wandering));
            coroutine = null;
        }
        else
        {
            agent.SetDestination(workplace.transform.position);
        }
    }

    private Vector3 RandomWanderPosition()
    {
        Vector3 randomPos = (Random.insideUnitSphere * wanderRange) + transform.position;
        NavMeshHit posHit;
        NavMesh.SamplePosition(randomPos, out posHit, wanderRange, 1);
        return posHit.position;
    }

    //remplacer par timer de horloge
    IEnumerator stateAterTime(float duration, Task state)
    {
        yield return new WaitForSeconds(duration);

        if(this.state == Task.working)
            isTired = true;

        this.state = state;
    }
}
