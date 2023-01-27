using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State 
{
    public abstract void Enter();
    public abstract void Tick(float deltatime);
    public abstract void Exit();
    protected float getNormalizedTime(Animator animator)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag("Attack")) { return nextInfo.normalizedTime; }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag("Attack")) { return currentInfo.normalizedTime; }
        else return 0f;
    }
}