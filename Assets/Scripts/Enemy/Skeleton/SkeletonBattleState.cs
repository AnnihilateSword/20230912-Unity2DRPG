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

        // ------ ע�������ǰ�棬ת�����⣬�����ת���²��ܼ�⵽��ң�����С BUG
        // ת��
        if (_player.position.x > _enemy.transform.position.x)
            _moveDir = 1;
        else if (_player.position.x < _enemy.transform.position.x)
            _moveDir = -1;
        // �ƶ�
        _enemy.SetVelocity(_enemy.moveSpeed * _moveDir, rb.velocity.y);
        // ------

        // �������
        if (_enemy.IsPlayerDetected())
        {
            stateTimer = _enemy.battleTime;  // �� stateTimer ����Ϊ�ܹ�������ս��ʱ��
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
            // �˳�������ʱ�� �� ����
            if (stateTimer < 0.0f || Vector2.Distance(_enemy.transform.position, _player.position) > _enemy.battleDistanceLimit)
            {
                // �˳�ս��״̬
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
    /// �Ƿ���Թ���
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
