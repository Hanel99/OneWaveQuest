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
        effectType = EnumHelper.EffectType.ThrowArmEffect;

        if (source is Player player)
        {
            var playerPosition = player.transform.position;
            var direction = (targetPosition - playerPosition).normalized;

            // var currentArm = ObjectPool.Instance.GetProjectile<Arm>();
            var arm = player.arm;
            arm.transform.position = playerPosition;
            arm.transform.rotation = Quaternion.LookRotation(direction);

            arm.Initialize();
            arm.OnEnemyDetected += OnEnemyDetected;
            arm.gameObject.SetActive(true);
            arm.LaunchToTarget(targetPosition);
        }
        else
        {
            Debug.LogWarning("ThrowArmEffect can only be applied by a Player.");
        }
    }

    // 이벤트 핸들러 예시
    private void OnEnemyDetected(Actor enemy)
    {
        var player = GamaManager.Instance.player;

        enemy.transform.SetParent(player.arm.attachPoint);
        enemy.transform.localPosition = Vector3.zero;
        // y값만 유지, x/z는 0
        var currentY = enemy.transform.eulerAngles.y;
        enemy.transform.rotation = Quaternion.Euler(0, currentY, 0);
        player.arm.enemy = enemy.GetComponent<Enemy>(); // 팔이 붙잡은 적

        enemy.GetComponent<Enemy>().OnGrabbed();
        this.End(player, enemy, targetPosition);
    }


}
