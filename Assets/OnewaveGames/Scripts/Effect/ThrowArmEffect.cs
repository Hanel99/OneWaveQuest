using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowArmEffect : Effect
{
    public override void Apply(Actor source, Actor target)
    {
        if (source is Enemy enemy && target is Player player)
        {
            // 적이 플레이어를 던지는 효과 적용
            enemy.Grab();
            Debug.Log("Enemy throws the player");
        }
        else
        {
            Debug.LogWarning("ThrowArmEffect can only be applied by an Enemy to a Player.");
        }
    }
}
