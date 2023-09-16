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
        xInput = 0.0f;  // �޸��������� bug

        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
            comboCounter = 0;

        player.anim.SetInteger("ComboCounter", comboCounter);

        // �����ٶ�
        //player.anim.speed = 1.2f;

        // ѡ�񹥻�ʱ�ķ���
        float attackDir = player.facingDir;
        if (xInput != 0.0f)
            attackDir = xInput;

        Debug.Log($"�������� {attackDir} ���� ��ʱ xinput={xInput}");

        // ����ʱ���ƶ������ӹ����𶯸У�
        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);

        stateTimer = player.attackInertiaTime;  // ����������һ�����
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", player.attackInertiaTime);

        comboCounter++;
        lastTimeAttacked = Time.time;

        // �˳�ʱ�ָ��ٶ�
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
