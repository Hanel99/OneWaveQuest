using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Actor가 HP를 소모할 때 사용하는 효과입니다.
/// </summary>
public class UseHPEffect : Effect
{
    public int healthCost = -20; // HP 소모량

    public override void Apply(Actor source, Actor target)
    {
        source.RestoreHealth(healthCost);
        Debug.Log("Player uses HP effect on self");
    }
}