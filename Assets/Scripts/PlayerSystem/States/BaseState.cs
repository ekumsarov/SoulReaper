using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState 
{
    protected PlayerController player;
    protected StateMachine stateMachine;
    protected PlayerData playerData;

    protected bool isAnimationFinished;
    protected bool isExitingState;

    protected float startTime;

    protected string animationName = "idle";
    protected int animationPlayCount = -1;

    protected string name = "BaseState";
    public string Name => name;

    public virtual void Initialize(PlayerController player, StateMachine stateMachine, PlayerData playerData)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
    }

    public virtual void Enter()
    {
        //Debug.LogError(this.Name);
        DoChecks();
        player.PlayAnimation(animationName, animationPlayCount);
        startTime = Time.time;
        isAnimationFinished = false;
        isExitingState = false;
    }

    public virtual void Exit()
    {
        player.StopAnimation();
        isExitingState = true;
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks() 
    {
        if (player.IsAnimationComplete)
            isAnimationFinished = true;
    }
}
