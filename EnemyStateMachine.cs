using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] public Health health                  { get; private set; }
    [field: SerializeField] public Animator animator              { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent             { get; private set; }
    [field: SerializeField] public WeaponDamage weapon            { get; private set; }
    [field: SerializeField] public ForceReceiver forceReceiver    { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }

    [field: SerializeField] public int AttackDamage               { get; private set; }
    [field: SerializeField] public float MovementSpeed            { get; private set; }
    [field: SerializeField] public float EnemyAttackRange         { get; private set; }
    [field: SerializeField] public float playerDetectionRange     { get; private set; }

    public GameObject player                                      { get; private set; }

    private void Start()
    { 
        Agent.updatePosition = false;
        Agent.updateRotation= false;
        player = GameObject.FindGameObjectWithTag("Player");
        switchState(new EnemyIdleState(this));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRange);
    }

    private void OnEnable()  {health.onTakeDamage += handleTakeDamage;}

    private void OnDisable() { health.onTakeDamage -= handleTakeDamage; }

    private void handleTakeDamage() { switchState(new EnemyImpactState(this)); }
}