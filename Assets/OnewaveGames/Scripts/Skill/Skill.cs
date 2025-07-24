using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 여러 Effect의 조합으로 동작하는 스킬의 기본 클래스입니다. 각 스킬은 Effect를 조합하여 다양한 기능을 수행할 수 있습니다.
/// </summary>
public abstract class Skill
{
    public List<Effect> EffectList { get; } = new();

    public abstract bool ApplySkill(Actor source, Actor target);
}
