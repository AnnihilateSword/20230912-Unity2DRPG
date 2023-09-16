using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player player, PlayerStateMachine stateMachine, string animBoolName)
        :
        base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (player.IsWallDetected())
        {
            stateMachine.ChangeState(player.wallSlideState);
            return;
        }

        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }

        // 空中可以移动
        if (xInput != 0.0f)
            player.SetVelocity(player.moveSpeed * xInput * player.airResistance, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.K) && player.airJumpCountRemain > 0)
        {
            player.airJumpCountRemain--;
            stateMachine.ChangeState(player.jumpState);
            return;
        }
    }
}
