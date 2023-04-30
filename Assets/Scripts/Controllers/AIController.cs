using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIController : Controller
{
    public enum AIStates {Idle, Seek, Destroy};
    [SerializeField] protected AIStates currentState;
    protected float timeEnteredCurrentState;

    // Start is called before the first frame update
    protected override void Start()
    {
        //add itself to list of ais
        GameManager.instance.ais.Add(this);
    }
    protected override void OnDestroy()
    {
        //remove itself from list of players
        GameManager.instance.ais.Remove(this);
    }

    protected virtual void Update()
    {
        MakeDecisions();
    }

    public virtual void ChangeState(AIStates newState)
    {
        //remember change state time
        timeEnteredCurrentState = Time.time;
        //change state
        currentState = newState;
    }

    public abstract void MakeDecisions();

    public virtual bool IsTimePassed(float amountOfTime)
    {
        if (Time.time - timeEnteredCurrentState >= amountOfTime)
        {
            return true;
        }
        return false;
    }

    public SoldierPawn getEnemyAIFront(float distance)
    {
        Vector3 targetPos = transform.forward * distance;
        //check if in LoS
        Ray tempRay = new Ray(transform.position, targetPos);
        RaycastHit hitInfo;
        if (Physics.Raycast(tempRay, out hitInfo, distance))
        {
            //check if hit
            if (hitInfo.collider.gameObject != this.gameObject)
            {
                //check if hit is an ai
                foreach (AIController ai in GameManager.instance.ais)
                {
                    if (hitInfo.collider.gameObject == ai.gameObject)
                    {
                        return hitInfo.collider.gameObject.GetComponent<SoldierPawn>();
                    }
                }
            }
        }
        return null;
    }

    protected void DoIdleState()
    {
        //do nothing
    }

    protected void DoSeekState()
    {
        pawn.MoveForward();
    }

    public SoldierPawn target;
    protected void DoDestroyState()
    {
        target = getEnemyAIFront(transform.lossyScale.z);
        if(target != null)
        {
            if (target.team != this.GetComponent<SoldierPawn>().team)
            {
                pawn.Attack(target);
            }
        }
        
    }


}
