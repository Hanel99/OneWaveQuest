using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnumHelper
{
    public enum SkillType
    {
        None,
        ThrowArmSkill,
        Count,
    }

    public enum EffectType
    {
        None,
        PullArmEffect,
        ThrowArmEffect,
        HitEnemyEffect,
        PlaySFXEffect,
        PlayVFXEffect,
        UseHPEffect,
        UseMPEffect,
        Count,
    }
}
