using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    public override void ApplySkill(Actor target)
    {
        if (target is Player player)
        {
            // 몬스터가 플레이어에게 가할 스킬
        }
    }

    public void OnGrabbed()
    {
        Debug.Log("붙잡힘!");
    }
}
