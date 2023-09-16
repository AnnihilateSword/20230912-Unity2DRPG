using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public static Player instance;

    [Header("移动数据")]
    public float moveSpeed = 8.0f;
    public float jumpForce = 12.0f;
    public float airResistance = 0.7f;
    public int airJumpCount = 1;
    public int airJumpCountRemain { get; set; }

    [Header("冲刺数据")]
    [SerializeField] private float _dashCoolDown = 1.0f;
    private float _dashCoolDownTimer;
    public float dashSpeed = 25.0f;
    public float dashDuration = 0.25f;
    public float dashDir { get; private set; }

    [Header("滑墙数据")]
    public float wallJumpTime = 0.15f;
    public float wallJumpForce = 5.0f;

    [Header("攻击数据")]
    public float attackInertiaTime = 0.1f;
    public Vector2[] attackMovement;
    public float counterAttackDuration = 0.2f;

    // 状态
    public bool isBusy { get; private set; } = false;

    #region 动画状态
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerPrimaryAttackState primaryAttackState { get; private set; }
    public PlayerCounterAttackState counterAttackState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        if (instance == null)
            instance = this;

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "WallJump");
        primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        counterAttackState = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currState.Update();

        CheckForDashInput();
    }

    /// <summary>
    /// 开启忙碌状态
    /// </summary>
    public IEnumerator BusyFor(float seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(seconds);
        isBusy = false;
    }

    /// <summary>
    /// 触发动画开关
    /// </summary>
    public void AnimationTrigger() => stateMachine.currState.AnimationFinishTrigger();

    /// <summary>
    /// 检测冲刺输入
    /// </summary>
    private void CheckForDashInput()
    {
        if (IsWallDetected())
            return;

        _dashCoolDownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.L) && _dashCoolDownTimer < 0.0f)
        {
            _dashCoolDownTimer = _dashCoolDown;
            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0.0f)
                dashDir = facingDir;

            stateMachine.ChangeState(dashState);
        }
    }
}
