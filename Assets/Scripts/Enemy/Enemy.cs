using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public static Enemy instance;

    [Header("眩晕数据")]
    public float stunDuration;
    public Vector2 stunDeraction;
    protected bool bCanBeStunned;
    [SerializeField] protected GameObject counterImage;

    [Header("玩家检测")]
    [SerializeField] protected LayerMask whatIsPlayer;

    [Header("移动数据")]
    public float moveSpeed = 1.5f;
    public float idleTime = 2.0f;

    [Header("战斗数据")]
    public float battleTime;  // 持续战斗时间

    [Header("攻击数据")]
    public float attackDistance;
    public float attackCooldown;  // 攻击冷却
    [HideInInspector] public float lastTimeAttacked;  // 上次攻击时间

    public EnemyStateMachine stateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        if (instance == null)
            instance = this;

        stateMachine = new EnemyStateMachine();
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();
    }

    public virtual void OpenCounterAttackWindow()
    {
        bCanBeStunned = true;
        counterImage.SetActive(true);
    }

    public virtual void CloseCounterAttackWindow()
    {
        bCanBeStunned = false;
        counterImage.SetActive(false);
    }

    public virtual bool CanBeStunned()
    {
        if (bCanBeStunned)
        {
            CloseCounterAttackWindow();
            return true;
        }

        return false;
    }

    /// <summary>
    /// 触发动画开关
    /// </summary>
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    /// <summary>
    /// 检测玩家
    /// </summary>
    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(transform.position, Vector2.right * facingDir, 50.0f, whatIsPlayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        // 攻击检测
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
    }

}
