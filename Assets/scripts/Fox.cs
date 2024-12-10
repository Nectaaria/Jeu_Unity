using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fox : MonoBehaviour
{
    public int daysToLive;
    public float bornDay;
    public enum Task { wandering, working, sleeping };
    public Task state = Task.wandering;
    [SerializeField] float wanderRange = 5f;

    private NavMeshAgent agent;

    public GameObject workplace;
    public float schoolProgression = 0;
    public string job;
    public string nextJob;

    [SerializeField] float workDuration = 480;
    [SerializeField] float sleepDuration = 600;

    private float sadTimeOrigin = -1;
    [SerializeField] float sadDelay = 180;
    public bool isSad = false;

    Coroutine coroutine = null;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        daysToLive = Random.Range(1,2);
        bornDay = TimeManager.instance.timeArray[0];
    }

    private void Update()
    {
        //depression if tired fot too long
        if (TimeManager.instance.timeArray[2] > sadTimeOrigin + sadDelay && sadTimeOrigin != -1)
        {
            isSad = true;
        }

        //choose a workplace based on the job
        switch(job)
        {
            case "builder":
                if (workplace == null)
                {
                    //if he is not building something, search for something to build and if there's nothing, wander
                    /*
                    foreach(buildingSite in buildManager.instance.buildingSites)
                    {
                        if (buildingSite.worker == null)
                        {
                            buildingSite.worker == gameObject;
                            workplace = buildingSite;
                            return;
                        }
                    }
                    if(workplace == null)
                    {
                        state = Task.Wandering
                    }
                    */
                }
                break;

            case "farmer":
                workplace = GameObject.Find("Bush");
                break;

            case "lumberjack":
                workplace = GameObject.Find("Forest");
                break;

            case "miner":
                workplace = GameObject.Find("Mine");
                break;

            case "student":
                workplace = GameObject.Find("School");
                break;

            case "wanderer":
                workplace = null;
                state = Task.wandering;
                break;
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
                //si arrive, isSad = false et sadTimeOrigin = -1;
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
            if(target.tag == "Workplace")
            {
                target.GetComponent<IWorkable>().Work(this);
            }

            //j'aurais dû implémenter l'interface IWorkable dans house aussi (mon système avec les tag n'est pas pratique à manipuler et il y a déja une interface pour workplace et school)
            //j'aurais dû appeler l'interface IInteractable
            else if(target.tag == "House")
            {
                StartCoroutine(target.GetComponent<House>().Sleep(sleepDuration));
            }
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
        yield return new WaitForSeconds(duration * TimeManager.instance.gameMinToRealSec);
        if (state == Task.sleeping)
            sadTimeOrigin = TimeManager.instance.timeArray[2];
        this.state = state;
    }

    //change the job throught UI
    public void ChangeJob(string job)
    {
        switch(job)
        {
            case "mason":
                this.job = "student";
                nextJob = job;
                break;

            case "miner":
                this.job = "student";
                nextJob = job;
                break;

            case "lumberjack":
                this.job = "student";
                nextJob = job;
                break;

            case "farmer":
                this.job = "student";
                nextJob = job;
                break;
        }
    }
}
