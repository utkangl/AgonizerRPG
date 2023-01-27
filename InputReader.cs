using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions 
{

    public bool isAttackingStandart { get; private set; }
    public bool isAttackingCombo { get; private set; }

    public Vector2 Movementvalue { get; private set; }

    public event Action JumpEvent;
    public event Action DodgeEvent;
    public event Action TargetLock;
    public event Action CancelTargetLock;


    private Controls controls;

    private void Start()
    {
        controls= new Controls();   
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }

    private void onDestroy()
    {
        controls.Player.Disable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(!context.performed) { return; }
        JumpEvent?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        DodgeEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Movementvalue= context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
    }

    public void OnTargetLock(InputAction.CallbackContext context)
    {
        if(!context.performed) { return; }
        TargetLock?.Invoke();
    }

    public void OnCancelTargetLock(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        CancelTargetLock?.Invoke();
    }

    public void OnStandartAttack(InputAction.CallbackContext context)
    {
        if (context.performed) { isAttackingStandart = true; }
        else if (context.canceled) { isAttackingStandart = false; }
    }

    public void OnComboAttack(InputAction.CallbackContext context)
    {
        if (context.performed) { isAttackingCombo = true; }
        else if (context.canceled) { isAttackingCombo = false; }
    }
}
