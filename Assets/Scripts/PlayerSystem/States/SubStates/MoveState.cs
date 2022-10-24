using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : GroundedState
{
    public override void Initialize(PlayerController player, StateMachine stateMachine, PlayerData playerData)
    {
        base.Initialize(player, stateMachine, playerData);
        this.animationName = "walk";
        this.name = playerData.Walk;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.CheckIfShouldFlip();

        player.Movement.SetVelocityX(playerData.MoveSpeed * player.xInput);

        if (!isExitingState)
        {
            if (player.xInput == 0)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else if(player.AttackCounter > 0)
            {
                stateMachine.ChangeState(player.AttackState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
