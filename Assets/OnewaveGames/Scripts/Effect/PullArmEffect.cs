using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 발사한 투사체를 다시 당겨오는 효과입니다. 만약 target이 있다면 해당 Actor도 당겨옵니다.
/// </summary>
public class PullArmEffect : Effect
{
    public override void Apply(Actor source, Actor target)
    {
        // Implement the logic to pull the arm of the target actor
        // This could involve changing the position or rotation of the target's arm
        // For example:
        Vector3 pullDirection = (target.transform.position - source.transform.position).normalized;
        float pullDistance = 1.0f; // Adjust this value as needed
        target.transform.position += pullDirection * pullDistance;

        // Optionally, you can add visual or sound effects here
    }
}
