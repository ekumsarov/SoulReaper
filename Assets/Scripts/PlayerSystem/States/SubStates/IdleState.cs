using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : AbilityState
{
    public override void Initialize(PlayerController player, StateMachine stateMachine, PlayerData playerData)
    {
        base.Initialize(player, stateMachine, playerData);
        this.name = playerData.Idle;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.Movement.SetVelocityX(0f);
        player.JumpState.ResetAmountOfJumpsLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            if (player.xInput != 0)
            {
                stateMachine.ChangeState(player.MoveState);
            }
            else if(player.AttackCounter > 0)
            {
                stateMachine.ChangeState(player.AttackState);
            }
            else if(player.JumpInput && player.CollisionSense.Ground)
            {
                stateMachine.ChangeState(player.JumpState);
            }
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}