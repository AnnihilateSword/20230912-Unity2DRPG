using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName)
        :
        base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(rb.velocity.x, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        // 跳跃期间可以移动
        if (xInput != 0.0f)
            player.SetVelocity(player.moveSpeed * xInput * player.airResistance, rb.velocity.y);

        if (rb.velocity.y < 0.0f)
        {
            stateMachine.ChangeState(player.airState);
            return;
        }
    }
}
