using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter;
    private float lastTimeAttacked;
    private float comboWindow = 2.0f;
    public PlayerPrimaryAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName)
        :
        base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        xInput = 0.0f;  // 修复攻击方向 bug

        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
            comboCounter = 0;

        player.anim.SetInteger("ComboCounter", comboCounter);

        // 攻击速度
        //player.anim.speed = 1.2f;

        // 选择攻击时的方向
        float attackDir = player.facingDir;
        if (xInput != 0.0f)
            attackDir = xInput;

        Debug.Log($"攻击方向 {attackDir} —— 此时 xinput={xInput}");

        // 攻击时的移动（增加攻击震动感）
        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);

        stateTimer = player.attackInertiaTime;  // 给攻击增加一点惯性
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", player.attackInertiaTime);

        comboCounter++;
        lastTimeAttacked = Time.time;

        // 退出时恢复速度
        //player.anim.speed = 1.0f;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0.0f)
            player.SetZeroVelocity();

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }
    }
}
