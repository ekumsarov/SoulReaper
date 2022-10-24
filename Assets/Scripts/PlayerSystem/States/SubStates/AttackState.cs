using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private int _currentCombo;
    public override void Initialize(PlayerController player, StateMachine stateMachine, PlayerData playerData)
    {
        base.Initialize(player, stateMachine, playerData);
        this.name = "AttackState";
        this.animationName = playerData.WeakAttack;
        this.animationPlayCount = 1;
    }

    public override void Enter()
    {
        base.Enter();
        _currentCombo = 0;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState && isAnimationFinished)
        {
            _currentCombo += 1;

            if(player.AttackCounter > _currentCombo)
            {
                isAnimationFinished = false;
                player.PlayAnimation(animationName, animationPlayCount);
                return;
            }

            player.ResetAttack();

            if (player.xInput != 0)
            {
                stateMachine.ChangeState(player.MoveState);
            }
            else
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        _currentCombo = 0;
    }
}