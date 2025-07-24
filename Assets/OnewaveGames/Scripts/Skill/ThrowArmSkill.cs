using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ThrowArmSkill은 플레이어가 팔을 던져 적을 잡거나, 특정 위치로 팔을 발사하는 스킬입니다.
/// 이 스킬은 다음과 같은 순서로 동작합니다:
/// 1. 지정 위치로 팔을 발사합니다.
/// 2. 팔이 특정 Actor에 닿거나, 플레이어의 사거리만큼 발사되면 멈춥니다.
/// 3. 팔을 다시 플레이어에게 당겨옵니다. 만약 팔이 적에게 닿았다면, 적도 같이 당겨옵니다.
/// 4. 팔이 플레이어에게 돌아오면, 팔을 다시 숨깁니다.
/// </summary>
public class ThrowArmSkill : Skill
{
    public override bool ApplySkill(Actor source, Actor target)
    {
        if (target is Enemy enemy)
        {
            // 적에게 스킬 적용
            enemy.ApplySkill(source);
            return true;
        }
        else if (target is Player player)
        {
            // 플레이어에게 스킬 적용
            player.ApplySkill(source);
            return true;
        }

        return false; // 스킬 적용 실패
    }
}
