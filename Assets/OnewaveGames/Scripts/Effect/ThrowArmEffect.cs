using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 지정한 위치로 투사체를 발사합니다. 발사 도중 특정 Actor에 닿거나, 플레이어의 사거리만큼 발사되었다면 이후 행동을 취합니다.
/// </summary>
public class ThrowArmEffect : Effect
{
    public Vector3 targetPosition;

    public override void Apply(Actor source, Actor target)
    {
        if (source is Player player)
        {
            var playerPosition = player.transform.position;
            var direction = (targetPosition - playerPosition).normalized;

            var currentArm = ObjectPool.Instance.GetProjectile<Arm>();
            currentArm.transform.position = playerPosition;
            currentArm.transform.rotation = Quaternion.LookRotation(direction);
            currentArm.gameObject.SetActive(true);

            var projectile = currentArm.GetComponent<Arm>();

            projectile.Initialize();
            projectile.OnEnemyDetected += OnEnemyDetected;
            projectile.LaunchToTarget(targetPosition);
        }
        else
        {
            Debug.LogWarning("ThrowArmEffect can only be applied by a Player.");
        }
    }

    // 이벤트 핸들러 예시
    private void OnEnemyDetected(Actor enemy)
    {
        enemy.GetComponent<Enemy>().OnGrabbed();
    }
}
