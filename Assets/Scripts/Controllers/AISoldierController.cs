using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISoldierController : AIController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        ChangeState(AIStates.Idle);
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void MakeDecisions()
    {
        if (pawn == null) return; //prevent null reference errors
        SoldierPawn soldier = this.GetComponent<SoldierPawn>();
        //FSM
        //based on current state
        switch (currentState)
        {
            case AIStates.Idle:
                //do state
                DoIdleState();
                //check for change state
                if (IsTimePassed(3))
                {
                    ChangeState(AIStates.Seek);
                    //prevent seek from moving until second from around
                    soldier.moveSpeed = soldier.baseMoveSpeed;
                }
                break;
            case AIStates.Seek:
                target = getEnemyAIFront(transform.lossyScale.z);
                if (target != null && target.team != this.GetComponent<SoldierPawn>().team)
                {
                    ChangeState(AIStates.Destroy);
                } else if (target != null && target.team == this.GetComponent<SoldierPawn>().team)
                {
                    ChangeState(AIStates.Idle);
                }

                DoSeekState();

                //give movement back after first move attempt in seek
                soldier.moveSpeed = soldier.maxMoveSpeed;
                break;
            case AIStates.Destroy:
                DoDestroyState();

                if (getEnemyAIFront(transform.lossyScale.z) == null)
                {
                    ChangeState(AIStates.Seek);
                }
                break;

        }
    }
}
