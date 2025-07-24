using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVFXEffect : Effect
{
    public GameObject VFXPrefab;

    public override void Apply(Actor source, Actor target)
    {
        if (VFXPrefab != null)
        {
            GameObject vfxInstance = Object.Instantiate(VFXPrefab, target.transform.position, Quaternion.identity);
            Object.Destroy(vfxInstance, 5f); // Destroy after 5 seconds to clean up
        }
    }
}
