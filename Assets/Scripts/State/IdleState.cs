using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private AIController aiController;
    private float idleDuration = 2f;
    private float idleTimer;

    public StateType Type => StateType.Idle;

    public IdleState(AIController aiController)
    {
        this.aiController = aiController;
    }
    
    public void Enter()
    {
        ideTimer = 0f;
        // aiController.Animator.SetBool("isMoving", false);
    }

    public void Execute()
    {
        ideTimer += Time.deltaTime;
        
        if (idleTimer > ideDuration) 
        {
            aiController.StateMachine.TransitionToState(StateType.Patrol);
        }
    }

    public void Exit()
    {
        // Clean up if necessary
    }

}

