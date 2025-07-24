using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{


    public override void ApplySkill(Actor target)
    {
        if (target is Player player)
        {
            // 스킬 효과 적용
            Debug.Log("Enemy applies skill to Player");
        }
    }

    public void Grab()
    {
        Debug.Log("Enemy grabs the player");
    }
}
