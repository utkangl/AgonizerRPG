using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public Attack[] Attacks               { get; private set; }
    [field: SerializeField] public Health health                  { get; private set; }
    [field: SerializeField] public Animator animator              { get; private set; }
    [field: SerializeField] public Targeter targeter              { get; private set; }
    [field: SerializeField] public WeaponDamage weapon            { get; private set; }
    [field: SerializeField] public InputReader InputReader        { get; private set; }
    [field: SerializeField] public ForceReceiver forceReceiver    { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }

    [field: SerializeField] public float FreeLookMovementSpeed    { get; private set; }
    [field: SerializeField] public float TargettingMovementSpeed  { get; private set; }
    [field: SerializeField] public float RotationDamping          { get; private set; }

    public Transform MainCameraTransform                          { get; private set; }

    private void Start()
    {
        switchState(new PlayerFreeLookState(this));
        MainCameraTransform = Camera.main.transform;
    }

    private void OnEnable()  {health.onTakeDamage += handleTakeDamage;}

    private void OnDisable() {health.onTakeDamage -= handleTakeDamage;}

    private void handleTakeDamage() { switchState(new PlayerImpactState(this)); }
}