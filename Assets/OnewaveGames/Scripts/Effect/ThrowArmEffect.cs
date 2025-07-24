using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 지정한 위치로 투사체를 발사합니다. 발사 도중 특정 Actor에 닿거나, 플레이어의 사거리만큼 발사되었다면 이후 행동을 취합니다.
/// </summary>
public class ThrowArmEffect : Effect
{
    public Vector3 targetPosition; // 투사체가 도달할 목표 위치

    public override void Apply(Actor source, Actor target)
    {
        if (source is Player player && target is Enemy targetEnemy)
        {
            Debug.Log("팔을 던짐");

            // TODO 가다가 투사체가 적에게 닿으면 해당 적을 끌고 옴
            {
                targetEnemy.OnGrabbed();
            }
        }
        else
        {
            Debug.LogWarning("ThrowArmEffect can only be applied by a Player to an Enemy.");
        }
    }
}
