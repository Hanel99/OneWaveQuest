using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
