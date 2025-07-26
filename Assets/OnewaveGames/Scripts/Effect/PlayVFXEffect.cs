using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어가 특정 위치에서 VFX(Visual Effects)를 재생하는 효과입니다.
/// </summary>
public class PlayVFXEffect : Effect
{
    public GameObject VFXPrefab;

    public override void Apply(Actor source, Actor target)
    {
        effectType = EnumHelper.EffectType.PlayVFXEffect;

        if (VFXPrefab != null)
        {
            GameObject vfxInstance = Object.Instantiate(VFXPrefab, target.transform.position, Quaternion.identity);
            Object.Destroy(vfxInstance, 5f); // Destroy after 5 seconds to clean up
        }

        this.End(source, target, Vector3.zero);
    }
}
