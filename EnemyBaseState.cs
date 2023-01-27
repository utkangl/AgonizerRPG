using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{

    protected EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
        
    public override void Enter()
    {
    }

    public override void Tick(float deltatime)
    {
    }

    public override void Exit()
    {
    }


    protected bool IsInChaseRange()
    {
        float playerDistSqr = (stateMachine.player.transform.position - stateMachine.transform.position).sqrMagnitude;
        return playerDistSqr <= stateMachine.playerDetectionRange * stateMachine.playerDetectionRange; 
    }


    protected void Move (float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void Move (Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move((motion + stateMachine.forceReceiver.Movement) * deltaTime);
    }

    protected void facePlayer()
    {
        if (stateMachine.player == null) { return; }
        Vector3 lookpos = stateMachine.player.transform.position - stateMachine.transform.position;
        lookpos.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(lookpos);
    }


}
