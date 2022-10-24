using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class JumpState : AbilityState
{
    private int amountOfJumpsLeft;

    public override void Initialize(PlayerController player, StateMachine stateMachine, PlayerData playerData)
    {
        base.Initialize(player, stateMachine, playerData);
        this.amountOfJumpsLeft = playerData.AmountOfJumps;
        this.name = "JumpState";
        this.animationName = playerData.Jump;
        this.animationPlayCount = 1;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        if(player.CollisionSense.Ground)
            player.PlayParticle("JumpDust");
        else
            player.PlayParticle("SecondJump");
    }

    public override void Exit()
    {
        base.Exit();
        player.StopParticle("JumpDust");
        player.StopParticle("SecondJump");
        player.UseJumpInput();
        player.Movement.SetVelocityY(playerData.JumpVelocity);
        amountOfJumpsLeft--;
        player.InAirState.SetIsJumping();
    }

    public override void LogicUpdate()
    {
        if(isAbilityDone)
        {
            stateMachine.ChangeState(player.InAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (isAnimationFinished)
        {
            isAbilityDone = true;
        }
    }

    public bool CanJump()
    {
        if (amountOfJumpsLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetAmountOfJumpsLeft() => amountOfJumpsLeft = playerData.AmountOfJumps;

    public void DecreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;
}