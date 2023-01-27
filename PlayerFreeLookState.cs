using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState 
{
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) {}

        private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");

    public override void Enter()
    {
        stateMachine.InputReader.TargetLock += onTargetLock;
        stateMachine.animator.Play(FreeLookBlendTreeHash);

    }

    public override void Tick(float deltatime)
    {
        Vector3 movement = CalculateMovement();

        if (stateMachine.InputReader.isAttackingStandart) { stateMachine.switchState(new PlayerAttacking(stateMachine, 0)); return; }
        stateMachine.Controller.Move(movement * deltatime * stateMachine.FreeLookMovementSpeed);

        if (stateMachine.InputReader.Movementvalue == Vector2.zero)
        {

            stateMachine.animator.SetFloat("FreeLookSpeed", 0, 0.1f, deltatime);
            return;
        }

        if (stateMachine.InputReader.isAttackingStandart) { stateMachine.switchState(new PlayerAttacking(stateMachine, 0)); return; }

        else
        {
            stateMachine.animator.SetFloat("FreeLookSpeed", 1, 0.1f, deltatime);
            faceMovementDirective(movement,deltatime);
        }

    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetLock -= onTargetLock;
    }

    private Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.InputReader.Movementvalue.y + right * stateMachine.InputReader.Movementvalue.x; 

    }

    private void faceMovementDirective(Vector3 movement , float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation,
            Quaternion.LookRotation(movement),
            deltaTime * stateMachine.RotationDamping)   ;
    }

    private void onTargetLock()
    {
        if (!stateMachine.targeter.selectTarget()) { return; }
        stateMachine.switchState(new PlayerTargetingState(stateMachine));
    }
}
