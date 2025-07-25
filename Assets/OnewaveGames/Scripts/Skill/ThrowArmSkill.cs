using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


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
    public override void SetEffectList(Actor source, Actor target, Vector3 targetPosition)
    {
        EffectList.Clear();

        var throwArmEffect = new ThrowArmEffect();
        throwArmEffect.skill = this;
        throwArmEffect.targetPosition = target == null ? targetPosition : target.transform.position;
        EffectList.Add(throwArmEffect);
    }

    public override bool ApplySkill(Actor source, Actor target, Vector3 targetPosition)
    {
        if (EffectList.Count > 0)
        {
            EffectList[0].Apply(source, target);
            EffectList.RemoveAt(0); // 첫 번째 효과를 적용한 후 리스트에서 제거합니다.
            return true; // 스킬 적용 성공
        }
        else
        {
            return false; // 효과가 설정되지 않은 경우 스킬 적용 실패
        }
    }


}
