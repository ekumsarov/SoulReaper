using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandState : BaseState
{
    public override void Initialize(PlayerController player, StateMachine stateMachine, PlayerData playerData)
    {
        base.Initialize(player, stateMachine, playerData);
        this.name = "LandState";
        this.animationName = playerData.Landing;
        this.animationPlayCount = 1;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState && isAnimationFinished)
        {
            if(player.AttackCounter > 0)
            {
                stateMachine.ChangeState(player.AttackState);
            }
            else if (player.xInput != 0)
            {
                stateMachine.ChangeState(player.MoveState);
            }
            else
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }
}