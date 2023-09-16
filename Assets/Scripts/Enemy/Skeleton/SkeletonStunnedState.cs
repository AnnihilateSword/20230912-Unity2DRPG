using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStunnedState : EnemyState
{
    private Enemy_Skeleton _enemy;
    public SkeletonStunnedState(Enemy enmeyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skeleton enemy)
        :
        base(enmeyBase, stateMachine, animBoolName)
    {
        _enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        _enemy.fx.InvokeRepeating("RedColorBlink", 0.0f, 0.1f);

        stateTimer = _enemy.stunDuration;

        rb.velocity = new Vector2(-_enemy.facingDir * _enemy.stunDeraction.x, _enemy.stunDeraction.y);
    }

    public override void Exit()
    {
        base.Exit();

        _enemy.fx.Invoke("CancelRedBlink", 0.0f);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0.0f)
            stateMachine.ChangeState(_enemy.idleState);
    }
}
