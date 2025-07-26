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
        }
    }

    private void OnPlayerReached(Actor player)
    {
        var arm = player.GetComponent<Player>().arm;
        var enemy = arm.enemy.gameObject;
        enemy.transform.SetParent(null);

        player.GetComponent<Player>().OnArmReached();
        this.End(player, null, Vector3.zero); // PullArmEffect는 targetPosition이 필요하지 않으므로 Vector3.zero로 전달합니다.
    }
}
