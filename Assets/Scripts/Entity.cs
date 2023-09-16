using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Knockback info")]
    [SerializeField] protected Vector2 knockbackDirection;
    [SerializeField] protected float knockbackDuration;
    protected bool bIsKnocked;

    [Header("碰撞数据")]
    public Transform attackCheck;
    public float attackCheckRadius;
    [SerializeField] protected Transform _groundCheck;
    [SerializeField] protected float _groundCheckDistance;
    [SerializeField] protected Transform _wallCheck;
    [SerializeField] protected float _wallCheckDistance;
    [SerializeField] protected LayerMask _whatIsGround;

    [Header("转向信息")]
    [SerializeField] protected bool _bFacingRight = true;
    public int facingDir { get; private set; } = 1;

    #region 组件
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityEffects fx { get; private set; }
    #endregion

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        fx = GetComponent<EntityEffects>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {

    }

    public virtual void Damage()
    {
        fx.StartCoroutine("FlashFX");
        StartCoroutine("HitKnockback");
        Debug.Log(gameObject.name + " was damaged!");
    }

    protected virtual IEnumerator HitKnockback()
    {
        bIsKnocked = true;
        rb.velocity = new Vector2(knockbackDirection.x * -facingDir, knockbackDirection.y);
        yield return new WaitForSeconds(knockbackDuration);
        bIsKnocked = false;
    }

    #region 碰撞
    public virtual bool IsGroundDetected() => Physics2D.Raycast(_groundCheck.position, Vector2.down, _groundCheckDistance, _whatIsGround);
    public virtual bool IsWallDetected() => Physics2D.Raycast(_wallCheck.position, Vector2.right * facingDir, _wallCheckDistance, _whatIsGround);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(_groundCheck.position, new Vector3(_groundCheck.position.x, _groundCheck.position.y - _groundCheckDistance));
        Gizmos.DrawLine(_wallCheck.position, new Vector3(_wallCheck.position.x + _wallCheckDistance, _wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    #endregion

    #region 翻转
    public virtual void Flip()
    {
        facingDir *= -1;
        _bFacingRight = !_bFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public virtual void FlipController(float xVelocity)
    {
        if (xVelocity > 0.0f && !_bFacingRight)
            Flip();
        else if (xVelocity < 0.0f && _bFacingRight)
            Flip();
    }
    #endregion

    #region 速度
    public virtual void SetZeroVelocity()
    {
        if (bIsKnocked)
            return;

        rb.velocity = Vector2.zero;
    }

    /// <summary>
    /// 设置速度
    /// </summary>
    public virtual void SetVelocity(float xVelocity, float yVelocity)
    {
        if (bIsKnocked)
            return;

        rb.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);  // 转向控制器
    }
    #endregion

}
