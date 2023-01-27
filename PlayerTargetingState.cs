using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    //Targeter targeter = new Targeter();
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { } // constructor
    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    private readonly int TargetingForwardHash = Animator.StringToHash("TargetingForwardSpeed");
    private readonly int TargetingRightHash = Animator.StringToHash("TargetingRightSpeed");

    public override void Enter()
    {
        stateMachine.InputReader.CancelTargetLock += onCancelTargetLock;  
        stateMachine.animator.Play(TargetingBlendTreeHash);
    }

    public override void Tick(float deltatime)
    {
        if (stateMachine.InputReader.isAttackingStandart) { stateMachine.switchState(new PlayerAttacking(stateMachine, 0)); return; }

        if(stateMachine.targeter.currentTarget == null) 
        { 
            stateMachine.switchState(new PlayerTargetingState(stateMachine));
            return;
        }

        Vector3 movement = CalculateMovement();

        stateMachine.Controller.Move(deltatime * stateMachine.TargettingMovementSpeed * movement);
        UpdateAnimator(deltatime);
        faceTarget();
    }

    public override void Exit()
    {
        stateMachine.InputReader.CancelTargetLock -= onCancelTargetLock;
    }

    public void onCancelTargetLock()
    {
        stateMachine.targeter.cancelSelectedTarget();
        stateMachine.switchState(new PlayerFreeLookState(stateMachine));
    }

    private Vector3 CalculateMovement()
    {
        Vector3 movement = new();

        movement += stateMachine.transform.right * stateMachine.InputReader.Movementvalue.x;
        movement += stateMachine.transform.forward * stateMachine.InputReader.Movementvalue.y;

        return movement;
    }


    private void UpdateAnimator(float deltaTime)
    {
        if (stateMachine.InputReader.Movementvalue.x == 0) { stateMachine.animator.SetFloat(TargetingForwardHash, 0, 0.1f, deltaTime*3); }
        else if (stateMachine.InputReader.Movementvalue.x <= 0) { stateMachine.animator.SetFloat(TargetingForwardHash, -1, 0.1f, deltaTime*3); }
        else if (stateMachine.InputReader.Movementvalue.x >= 0) { stateMachine.animator.SetFloat(TargetingForwardHash, 1, 0.1f, deltaTime*3); }
            if (stateMachine.InputReader.Movementvalue.y == 0) { stateMachine.animator.SetFloat(TargetingRightHash, 0, 0.1f, deltaTime*3); }
            else if (stateMachine.InputReader.Movementvalue.y <= 0) { stateMachine.animator.SetFloat(TargetingRightHash, -1, 0.1f, deltaTime*3); }
            else if (stateMachine.InputReader.Movementvalue.y >= 0) { stateMachine.animator.SetFloat(TargetingRightHash, 1, 0.1f, deltaTime*3); }        }
    
}
