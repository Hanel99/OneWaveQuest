using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 여러 Effect의 조합으로 동작하는 스킬의 기본 클래스입니다. 각 스킬은 Effect를 조합하여 다양한 기능을 수행할 수 있습니다.
/// </summary>
public abstract class Skill
{
    public EnumHelper.SkillType skillType;
    public List<Effect> EffectList { get; } = new();


    public abstract void SetEffectList(Actor source, Actor target, Vector3 targetPosition);
    public abstract bool ApplySkill(Actor source, Actor target, Vector3 targetPosition);
    public abstract void EndSkill();

    public struct SkillOptionData
    {
        // 스킬에 필요한 데이터들을 넣어 같이 보낼 구조체
        public Vector3 targetPosition;
    }
}
