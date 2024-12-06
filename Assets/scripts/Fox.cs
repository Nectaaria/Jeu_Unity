using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fox : MonoBehaviour
{
    [SerializeField] float liveTime;
    private float bornTime;
    public enum Task { wandering, working, sleeping };
    public Task state = Task.wandering;
    [SerializeField] float wanderRange = 5f;

    private NavMeshAgent agent;
    [SerializeField] GameObject workplace;

    [SerializeField] float workDuration;
    [SerializeField] float sleepDuration;

    private float sadTimeOrigin;
    public bool isSad = false;

    Coroutine coroutine=null;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        liveTime = Random.Range(10,15);
        //bornTime = horloge.time;
    }

    private void Update()
    {
        /*
        //if he is old enough, he dies
        private float currentTime = horloge.time;
        if(bornTime + currentTime > liveTime)
        {
            GameObject.Destroy();
        }

        //if he didn't sleep for too long, he's sad
        if(horloge.timer > sadTimeOrigin + sadTimeToTrigger)
        {
            isSad = true;
        }
        */

        //if he doesn't have a job
        if (workplace == null)
        {
            state = Task.wandering;
        }

        switch (state)
        {
            case Task.wandering:
                Wander();
                break;

            case Task.working:
                //Work and become tired at the end
                InteractWith(workplace, workDuration, Task.sleeping);
                break;

            case Task.sleeping:
                //chercher maison pour dormir et si arrive pas, Wander();
                //si arrive, isSad = false
                break;
        }
    }

    // make the npc wander to a random point in the navMesh surface
    private void Wander()
    {
        if (agent.remainingDistance < 1)
            agent.SetDestination(RandomWanderPosition());
    }

    // interact with a building (for sleeping and working) and change the state at the end
    private void InteractWith(GameObject target, float duration, Task nextState)
    {
        if (Vector3.Distance(transform.position, target.transform.position) < 1)
        {
            /*if (target.tag == "workplace")
                workplace.GetComponent<script>().Harvest();*/
            if (coroutine != null)
                return;
            coroutine =  StartCoroutine( stateAterTime(duration, nextState));
            coroutine = null;
        }
        else
        {
            agent.SetDestination(target.transform.position);
        }
    }

    // return a random position in a certain radius in the navMesh surface
    private Vector3 RandomWanderPosition()
    {
        Vector3 randomPos = (Random.insideUnitSphere * wanderRange) + transform.position;
        NavMeshHit posHit;
        NavMesh.SamplePosition(randomPos, out posHit, wanderRange, 1);
        return posHit.position;
    }

    //Change the state after a certain amount of time
    IEnumerator stateAterTime(float duration, Task state)
    {
        yield return new WaitForSeconds(duration);
        if (state == Task.sleeping)
            sadTimeOrigin = Time.time;
        this.state = state;
    }
}
