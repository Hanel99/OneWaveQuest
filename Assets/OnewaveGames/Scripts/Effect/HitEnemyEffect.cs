using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemyEffect : Effect
{

    public override void Apply(Actor source, Actor target)
    {
        if (source is Player player && target is Enemy enemy)
        {
            // 플레이어가 적을 공격하는 효과 적용
            enemy.TakeDamage(player.AttackPower);
            Debug.Log("Player hits the enemy");
        }
        else
        {
            Debug.LogWarning("HitEnemyEffect can only be applied by a Player to an Enemy.");
        }
    }
}
