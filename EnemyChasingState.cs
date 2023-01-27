using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.animator.CrossFadeInFixedTime(LocomotionHash, 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        if (!IsInChaseRange()) {stateMachine.switchState(new EnemyIdleState(stateMachine)); return; }
        if (IsInAttackRange()) {stateMachine.switchState(new EnemyAttackingState(stateMachine)); return; }

        moveToPlayer(deltaTime);
        facePlayer();
        stateMachine.animator.SetFloat(SpeedHash, 1f, 0.1f, deltaTime);
    }

    public override void Exit()
    {

        stateMachine.Agent.ResetPath();
        stateMachine.Agent.velocity = Vector3.zero;
        

    }

    private void moveToPlayer(float deltaTime)
    {
        if (stateMachine.Agent.isOnNavMesh)
        {
            stateMachine.Agent.destination = stateMachine.player.transform.position;
            Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.MovementSpeed, deltaTime);
        }

        stateMachine.Agent.velocity = stateMachine.Controller.velocity;
    }

    protected bool IsInAttackRange()
    {
        float playerDistSqr = (stateMachine.player.transform.position - stateMachine.transform.position).sqrMagnitude;
        return playerDistSqr <= stateMachine.EnemyAttackRange * stateMachine.EnemyAttackRange;
    }

}
