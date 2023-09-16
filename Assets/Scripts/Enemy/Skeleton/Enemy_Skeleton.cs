using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Enemy
{
    #region ∂Øª≠◊¥Ã¨
    public SkeletonIdleState idleState { get; private set; }
    public SkeletonMoveState moveState { get; private set; }
    public SkeletonBattleState battleState { get; private set; }
    public SkeletomAttackState attackState { get; private set; }
    public SkeletonStunnedState stunnedState { get; private set; }
    #endregion

    public float battleDistanceLimit = 10.0f;
    public float battleBackCheckDistance = 2.0f;

    protected override void Awake()
    {
        base.Awake();

        idleState = new SkeletonIdleState(this, stateMachine, "Idle", this);
        moveState = new SkeletonMoveState(this, stateMachine, "Move", this);
        battleState = new SkeletonBattleState(this, stateMachine, "Move", this);
        attackState = new SkeletomAttackState(this, stateMachine, "Attack", this);
        stunnedState = new SkeletonStunnedState(this, stateMachine, "Stunned", this);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(stunnedState);
            return true;
        }
        return false;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        // ’Ω∂∑±≥∫ÛºÏ≤‚æ‡¿Î
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, battleBackCheckDistance);
        // ’Ω∂∑æ‡¿Îœﬁ÷∆
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, battleDistanceLimit);
    }
}
