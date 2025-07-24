using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어가 특정 위치에서 사운드 효과(SFX)를 재생하는 효과입니다.
/// </summary>
public class PlaySFXEffect : Effect
{
    public AudioClip SFX;

    public override void Apply(Actor source, Actor target)
    {
        if (SFX != null)
        {
            AudioSource.PlayClipAtPoint(SFX, target.transform.position);
        }
    }
}
