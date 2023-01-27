using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpactState : PlayerBaseState
{
    public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    private readonly int ImpactHash = Animator.StringToHash("Impact");
    private float duration = 1f;

    public override void Enter()
    {
        stateMachine.animator.CrossFadeInFixedTime(ImpactHash, 0.1f);
    }

    public override void Tick(float deltatime)
    {
        Move(deltatime);

        duration -= deltatime;
        if (duration < 0f)
        {
            if (stateMachine.targeter.currentTarget != null)
            {
                stateMachine.switchState(new PlayerTargetingState(stateMachine));

            }else stateMachine.switchState(new PlayerFreeLookState(stateMachine));
        }

    }

    public override void Exit()
    {

    }
}
