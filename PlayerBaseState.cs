using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State 
    
{

    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void faceTarget()
    {
        if(stateMachine.targeter.currentTarget == null) { return; }
        Vector3 lookpos = stateMachine.targeter.currentTarget.transform.position - stateMachine.transform.position;
        lookpos.y = 0f;

        stateMachine.transform.rotation= Quaternion.LookRotation(lookpos);
    }
    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move((motion + stateMachine.forceReceiver.Movement) * deltaTime);
    }

}
