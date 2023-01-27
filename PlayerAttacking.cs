using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerAttacking : PlayerBaseState
{
    private Attack attack;
    private float previousFrameTime;


    public PlayerAttacking(PlayerStateMachine stateMachine, int AttackIndex) : base(stateMachine)
    {
        attack = stateMachine.Attacks[AttackIndex];
    }

    public override void Enter()
    {
        stateMachine.weapon.SetAttack(attack.Damage);
        stateMachine.animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }

    public override void Tick(float deltatime)
    {
        float normalizedTime = getNormalizedTime(stateMachine.animator);
        if (normalizedTime >= previousFrameTime && normalizedTime < 1f) 
        {
            if (stateMachine.InputReader.isAttackingStandart) { TryComboAttack(); }
        }

        else
        {
            if(stateMachine.targeter.currentTarget != null) { stateMachine.switchState(new PlayerTargetingState(stateMachine)); }
            else stateMachine.switchState(new PlayerFreeLookState(stateMachine));
        }

        previousFrameTime = normalizedTime;
    }

    public override void Exit()
    {

    }

    private void TryComboAttack()
    {
        float normalizedTime = getNormalizedTime(stateMachine.animator);
        if (attack.ComboStateIndex == -1) { Debug.Log("anana bastým"); }
        stateMachine.switchState(new PlayerAttacking(stateMachine, attack.ComboStateIndex)); 
        
    }
}
