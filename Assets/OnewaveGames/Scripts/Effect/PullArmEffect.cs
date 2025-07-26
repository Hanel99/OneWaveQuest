using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 발사한 투사체를 다시 당겨오는 효과입니다. 만약 target이 있다면 해당 Actor도 당겨옵니다.
/// </summary>
public class PullArmEffect : Effect
{
    public override void Apply(Actor source, Actor target)
    {
        effectType = EnumHelper.EffectType.PullArmEffect;

        if (source is Player player && target is Enemy enemy)
        {
            Debug.Log($"적을 붙입니다: {enemy.name}");

            var direction = (enemy.transform.position - player.transform.position).normalized;

            var arm = player.arm;
            arm.transform.position = enemy.transform.position;
            arm.transform.rotation = Quaternion.LookRotation(direction);

            arm.Initialize(false);
            arm.gameObject.SetActive(true);
            arm.OnPlayerReached += OnPlayerReached;
            arm.LaunchToTarget(player.transform.position);


            // 적의 물리 비활성화
            DisableEnemyPhysics(enemy.gameObject);
        }
    }

    private void OnPlayerReached(Actor player)
    {
        var arm = player.GetComponent<Player>().arm;
        var enemy = arm.enemy.gameObject;
        enemy.transform.SetParent(null);
        EnableEnemyPhysics(enemy);

        player.GetComponent<Player>().OnArmReached();
    }


    private void DisableEnemyPhysics(GameObject enemy)
    {
        // 콜리더 비활성화 (다른 오브젝트와 충돌 방지)
        // var colliders = enemy.GetComponents<Collider>();
        // foreach (var collider in colliders)
        // {
        //     collider.enabled = false;
        // }
    }

    private void EnableEnemyPhysics(GameObject enemy)
    {
        // 콜리더 다시 활성화
        // var colliders = enemy.GetComponents<Collider>();
        // foreach (var collider in colliders)
        // {
        //     collider.enabled = true;
        // }
    }
}
