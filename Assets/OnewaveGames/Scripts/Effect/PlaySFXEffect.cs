using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
