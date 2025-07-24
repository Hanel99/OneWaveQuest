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

    public void TakeDamage(int damage)
    {
        // 적이 피해를 받는 로직
        Debug.Log($"Enemy takes {damage} damage");
    }
}
