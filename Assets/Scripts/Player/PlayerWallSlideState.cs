using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, string animBoolName)
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

        if (Input.GetKeyDown(KeyCode.K))
        {
            stateMachine.ChangeState(player.wallJumpState);
            return;
        }

        if (xInput != 0.0f && player.facingDir != xInput)
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }

        if (yInput < 0.0f)
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        else
            rb.velocity = new Vector2(0.0f, rb.velocity.y * 0.7f);

        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }
    }
}
