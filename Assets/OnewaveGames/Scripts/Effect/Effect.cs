using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스킬의 각 기능을 담당하는 작은 단위의 모듈입니다. (예: CostEffect - 자원 소모, ProjectileEffect - 투사체 발사, PullEffect - 끌어오기)
/// </summary>
public abstract class Effect
{
    public Skill skill;    // 이 Effect가 속한 Skill
    public EnumHelper.EffectType effectType; // Effect의 종류
    public abstract void Apply(Actor source, Actor target);


    //해당 이펙트가 종료되었음을 알려줌... 인데 필요한가?
    public virtual void End(Actor source, Actor target, Vector3 targetPosition)
    {
        skill.ApplySkill(source, target, targetPosition);
    }
}
