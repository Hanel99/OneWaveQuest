using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 적에게 피해를 주는 효과입니다. 플레이어의 공격력 만큼 데미지를 입힙니다.
/// </summary>
public class HitEnemyEffect : Effect
{
    public override void Apply(Actor source, Actor target)
    {
        effectType = EnumHelper.EffectType.HitEnemyEffect;

        if (source is Player player && target is Enemy enemy)
        {
            // 플레이어가 적을 공격하는 효과 적용
            enemy.TakeDamage(player.actorStatData.AttackPower);
            Debug.Log("Player hits the enemy");
        }
        else
        {
            Debug.LogWarning("HitEnemyEffect can only be applied by a Player to an Enemy.");
        }
    }
}
