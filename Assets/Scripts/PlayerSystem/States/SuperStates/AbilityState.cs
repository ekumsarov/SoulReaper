using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityState : BaseState
{
    protected bool isAbilityDone;

    private bool isGrounded;

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CollisionSense.Ground;
    }

    public override void Enter()
    {
        base.Enter();

        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAbilityDone)
        {
            if (isGrounded && player.Movement.CurrentVelocity.y < 0.01f)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else
            {
                stateMachine.ChangeState(player.InAirState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
