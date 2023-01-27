using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");


    public override void Enter()
    {
        stateMachine.animator.CrossFadeInFixedTime(LocomotionHash, 0.1f);
    }

    public override void Tick(float deltatime)
    {
        Move(deltatime);
        stateMachine.animator.SetFloat(SpeedHash, 0f, 0.1f, deltatime);

        if (IsInChaseRange())
        {
            stateMachine.switchState(new EnemyChasingState(stateMachine));
            return;
        }
        facePlayer();


    }

    public override void Exit()
    {

    }

}
