using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 지정한 위치로 투사체를 발사합니다. 발사 도중 특정 Actor에 닿거나, 플레이어의 사거리만큼 발사되었다면 이후 행동을 취합니다.
/// </summary>
public class ThrowArmEffect : Effect
{
    public override void Apply(Actor source, Actor target)
    {
        if (source is Enemy enemy && target is Player player)
        {
            // 적이 플레이어를 던지는 효과 적용
            enemy.Grab();
            Debug.Log("Enemy throws the player");
        }
        else
        {
            Debug.LogWarning("ThrowArmEffect can only be applied by an Enemy to a Player.");
        }
    }
}
