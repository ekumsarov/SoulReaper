using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : BaseState
{
    private bool JumpInput;
    private bool isGrounded;
    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CollisionSense.Ground;
        JumpInput = player.JumpInput;
    }
    public override void Enter()
    {
        base.Enter();
        player.JumpState.ResetAmountOfJumpsLeft();
        DoChecks();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (JumpInput && player.JumpState.CanJump())
        {
            Debug.LogError("Jump from grounded");
            stateMachine.ChangeState(player.JumpState);
        }else if (!isGrounded)
        {
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }
    }
}
