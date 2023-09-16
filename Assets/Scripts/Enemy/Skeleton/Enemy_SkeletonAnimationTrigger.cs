using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SkeletonAnimationTrigger : MonoBehaviour
{
    private void AnimationTrigger()
    {
        Enemy.instance.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(Enemy.instance.attackCheck.position, Enemy.instance.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
                hit.GetComponent<Player>().Damage();
        }
    }

    protected void OpenCounterAttackWindow() => Enemy.instance.OpenCounterAttackWindow();
    protected void CloseCounterAttackWindow() => Enemy.instance.CloseCounterAttackWindow();
}
