using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 발사한 투사체를 다시 당겨오는 효과입니다. 만약 target이 있다면 해당 Actor도 당겨옵니다.
/// </summary>
public class PullArmEffect : Effect
{
    public Arm arm;

    public override void Apply(Actor source, Actor target)
    {
        if (source is Enemy enemy && target is Player player)
        {
            Debug.Log($"적을 붙입니다: {enemy.name}");

            enemy.transform.SetParent(arm.attachPoint);
            enemy.transform.localPosition = Vector3.zero;
            enemy.transform.localRotation = Quaternion.identity;

            // 적의 물리 비활성화
            DisableEnemyPhysics(enemy.gameObject);
        }
    }


    private void DisableEnemyPhysics(GameObject enemy)
    {
        // Rigidbody 처리
        var rigidbody = enemy.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.isKinematic = true;
            rigidbody.detectCollisions = false;
        }

        // 콜리더 비활성화 (다른 오브젝트와 충돌 방지)
        var colliders = enemy.GetComponents<Collider>();
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
    }

    private void EnableEnemyPhysics(GameObject enemy)
    {
        // Rigidbody 처리
        var rigidbody = enemy.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.isKinematic = false;
            rigidbody.detectCollisions = true;
        }

        // 콜리더 다시 활성화
        var colliders = enemy.GetComponents<Collider>();
        foreach (var collider in colliders)
        {
            collider.enabled = true;
        }
    }
}
