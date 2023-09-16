using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletomAttackState : EnemyState
{
    private Enemy_Skeleton _enemy;
    public SkeletomAttackState(Enemy enmeyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skeleton enemy)
        :
        base(enmeyBase, stateMachine, animBoolName)
    {
        _enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        _enemy.lastTimeAttacked = Time.time;  // 刷新上次攻击时间
    }

    public override void Update()
    {
        base.Update();

        _enemy.SetZeroVelocity();

        if (triggerCalled)
        {
            stateMachine.ChangeState(_enemy.battleState);
            return;
        }
    }
}
