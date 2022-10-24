using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirState : BaseState
{
    //Input
    private int xInput;
    private bool jumpInput;
    private bool jumpInputStop;

    //Checks
    private bool isGrounded;

    private bool coyoteTime;
    private bool wallJumpCoyoteTime;
    private bool isJumping;

    private float startWallJumpCoyoteTime;

    private bool changedAnimation = false;

    public override void Initialize(PlayerController player, StateMachine stateMachine, PlayerData playerData)
    {
        base.Initialize(player, stateMachine, playerData);
        this.name = "InAirState";
        this.animationName = playerData.FlyingUp;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CollisionSense.Ground;
    }

    public override void Enter()
    {
        base.Enter();
        changedAnimation = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();

        xInput = player.xInput;
        jumpInputStop = player.JumpInputStop;

        if (changedAnimation == false && player.Movement.CurrentVelocity.y < -0.01f)
        {
            changedAnimation = true;
            player.PlayAnimation(playerData.FlyingDown);
        }

        CheckJumpMultiplier();

        if (isGrounded && player.Movement.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if (player.JumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else
        {
            player.CheckIfShouldFlip();
            player.Movement.SetVelocityX(playerData.MoveSpeed * xInput);
        }

    }

    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop && playerData.ActivateVariableJump)
            {
                player.Movement.SetVelocityY(player.Movement.CurrentVelocity.y * playerData.VariableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (player.Movement.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
            }

        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time > startTime + playerData.CoyoteTime)
        {
            coyoteTime = false;
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    public void StartCoyoteTime() => coyoteTime = true;


    public void SetIsJumping() => isJumping = true;
}