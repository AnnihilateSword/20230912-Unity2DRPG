using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Enemy_Skeleton _enemy;
    private Transform _player;
    private int _moveDir;
    public SkeletonBattleState(Enemy enmeyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skeleton enemy)
        :
        base(enmeyBase, stateMachine, animBoolName)
    {
        this._enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        _player = Player.instance.transform;
    }

    public override void Update()
    {
        base.Update();

        // ------ 注意这个放前面，转向问题，如果不转向导致不能检测到玩家，会有小 BUG
        // 转向
        if (_player.position.x > _enemy.transform.position.x)
            _moveDir = 1;
        else if (_player.position.x < _enemy.transform.position.x)
            _moveDir = -1;
        // 移动
        _enemy.SetVelocity(_enemy.moveSpeed * _moveDir, rb.velocity.y);
        // ------

        // 攻击检测
        if (_enemy.IsPlayerDetected())
        {
            stateTimer = _enemy.battleTime;  // 将 stateTimer 重置为能够持续的战斗时间
            if (_enemy.IsPlayerDetected().distance < _enemy.attackDistance)
            {
                if (CanAttack())
                {
                    stateMachine.ChangeState(_enemy.attackState);
                    return;
                }
            }
        }
        else
        {
            // 退出条件，时间 或 距离
            if (stateTimer < 0.0f || Vector2.Distance(_enemy.transform.position, _player.position) > _enemy.battleDistanceLimit)
            {
                // 退出战斗状态
                stateMachine.ChangeState(_enemy.idleState);
                return;
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    /// <summary>
    /// 是否可以攻击
    /// </summary>
    private bool CanAttack()
    {
        if (Time.time >= _enemy.attackCooldown + _enemy.lastTimeAttacked)
        {
            _enemy.lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }
}
