using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public static Enemy instance;

    [Header("ѣ������")]
    public float stunDuration;
    public Vector2 stunDeraction;
    protected bool bCanBeStunned;
    [SerializeField] protected GameObject counterImage;

    [Header("��Ҽ��")]
    [SerializeField] protected LayerMask whatIsPlayer;

    [Header("�ƶ�����")]
    public float moveSpeed = 1.5f;
    public float idleTime = 2.0f;

    [Header("ս������")]
    public float battleTime;  // ����ս��ʱ��

    [Header("��������")]
    public float attackDistance;
    public float attackCooldown;  // ������ȴ
    [HideInInspector] public float lastTimeAttacked;  // �ϴι���ʱ��

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
    /// ������������
    /// </summary>
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    /// <summary>
    /// ������
    /// </summary>
    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(transform.position, Vector2.right * facingDir, 50.0f, whatIsPlayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        // �������
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
    }

}
