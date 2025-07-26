using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Actor가 MP를 소모할 때 사용하는 효과입니다.
/// </summary>
public class UseMPEffect : Effect
{
    public int manaCost = -20; // MP 소모량

    public override void Apply(Actor source, Actor target)
    {
        effectType = EnumHelper.EffectType.UseMPEffect;

        source.RestoreMana(manaCost);
        Debug.Log($"{manaCost} 마나 증감. -> 현재 {source.actorStatData.CurrentMana}");

        this.End(source, target, Vector3.zero);
    }
}