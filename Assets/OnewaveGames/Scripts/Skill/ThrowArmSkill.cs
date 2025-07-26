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
        skillType = EnumHelper.SkillType.ThrowArmSkill;
        EffectList.Clear();

        var useMPEffect = new UseMPEffect();
        useMPEffect.skill = this;
        useMPEffect.manaCost = -SkillManager.Instance.armSkillData.requireMana; // MP 소모량 설정
        EffectList.Add(useMPEffect);

        var throwArmEffect = new ThrowArmEffect();
        throwArmEffect.skill = this;
        throwArmEffect.targetPosition = target == null ? targetPosition : target.transform.position;
        EffectList.Add(throwArmEffect);


        var hitEnemyEffect = new HitEnemyEffect();
        hitEnemyEffect.skill = this;
        hitEnemyEffect.damageAmount = SkillManager.Instance.armSkillData.enemyDamage; // 데미지 설정
        EffectList.Add(hitEnemyEffect);

        var pullArmEffect = new PullArmEffect();
        pullArmEffect.skill = this;
        EffectList.Add(pullArmEffect);
    }

    public override bool ApplySkill(Actor source, Actor target, Vector3 targetPosition)
    {
        if (EffectList.Count > 0)
        {
            var effect = EffectList[0];
            EffectList.RemoveAt(0); // 첫 번째 효과를 적용한 후 리스트에서 제거합니다.
            effect.Apply(source, target);
            return true; // 스킬 적용 성공
        }
        else
        {
            EndSkill();
            return false; // 효과가 설정되지 않은 경우 스킬 적용 실패
        }
    }

    public override void EndSkill()
    {
        Debug.Log("ThrowArmSkill has ended.");
        SkillManager.Instance.RemoveSkill(skillType); // 스킬 매니저에서 스킬 제거

    }


}
