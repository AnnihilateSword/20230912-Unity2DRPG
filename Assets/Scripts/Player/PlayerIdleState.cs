using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string animBoolName)
        :
        base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (!player.isBusy)
        {
            if (player.IsWallDetected())
            {
                // 如果碰到墙壁只能往反方向移动（防止碰壁跑）
                if (xInput > 0.0f && player.facingDir == -1 ||
                xInput < 0.0f && player.facingDir == 1)
                {
                    stateMachine.ChangeState(player.moveState);
                    return;
                }
            }
            else if (xInput != 0.0f)
            {
                stateMachine.ChangeState(player.moveState);
                return;
            }
        }
    }
}
