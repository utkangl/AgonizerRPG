using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    private readonly int AttackHash = Animator.StringToHash("Attack");

    public override void Enter()
    {
        stateMachine.animator.CrossFadeInFixedTime(AttackHash, 0.1f);
        stateMachine.weapon.SetAttack(stateMachine.AttackDamage);
    }

    public override void Tick(float deltatime)
    {
        if(getNormalizedTime(stateMachine.animator) >= 1) 
        {
            stateMachine.switchState(new EnemyChasingState(stateMachine));
        }
    }

    public override void Exit()
    {
    }



}

