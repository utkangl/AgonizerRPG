using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{

    private State currentState;  // object of state class, to inherit from it

    private void Update()
    {
        currentState?.Tick(Time.deltaTime);
    }

    public void switchState(State newstate)
    {
        currentState?.Exit();
        currentState = newstate;
        currentState?.Enter();
    }



}
