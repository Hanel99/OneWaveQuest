using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : Projectile
{
    public Transform attachPoint; // 적 담아놓을 오브젝트

    [Header("스킬 데이터")]
    public ArmSkillData skillData;

    [Header("디버그")]
    [SerializeField] private bool showGizmos = true;

    // 이벤트
    public System.Action<Actor> OnEnemyDetected;

    // 상태
    private bool isActive = true;
    private Actor detectedEnemy;


    // 이동 관련 변수
    private Vector3 direction;
    private float distanceTraveled;
    private bool isMoving = false;

    void Awake()
    {
        SetupComponents();
    }

    void Update()
    {
        if (isMoving)
        {
            MoveProjectile();
        }
    }



    void SetupComponents()
    {
        if (tr == null) tr = GetComponent<Transform>();
        if (capsuleCollider == null) capsuleCollider = GetComponent<CapsuleCollider>();
        if (capsuleCollider != null) capsuleCollider.isTrigger = true;
    }

    public void Initialize()
    {
        capsuleCollider.radius = skillData.detectionRadius;
        OnEnemyDetected = null;

        isActive = true;
        detectedEnemy = null;
    }






    public void LaunchProjectile(Vector3 targetDirection)
    {
        direction = targetDirection.normalized;
        distanceTraveled = 0f;
        isMoving = true;

        // 투사체가 이동 방향을 바라보도록 회전
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        Debug.Log($"투사체 발사: 방향 {direction}, 속도 {skillData.armSpeed}, 최대거리 {skillData.maxDistance}");
    }

    public void LaunchToTarget(Vector3 targetPosition)
    {
        Vector3 directionToTarget = (targetPosition - transform.position).normalized;
        LaunchProjectile(directionToTarget);
    }

    public void StopMovement()
    {
        isMoving = false;
        Debug.Log("투사체 이동 정지");
    }

    private void MoveProjectile()
    {
        // 이동 거리 계산
        float moveDistance = skillData.armSpeed * Time.deltaTime;

        // 최대 거리 체크
        if (distanceTraveled + moveDistance >= skillData.maxDistance)
        {
            // 정확히 최대 거리까지만 이동
            float remainingDistance = skillData.maxDistance - distanceTraveled;
            transform.position += direction * remainingDistance;
            distanceTraveled = skillData.maxDistance;

            // 이동 정지
            isMoving = false;

            Debug.Log("투사체가 최대 거리에 도달했습니다");
            this.ReturnToPool();
            return;
        }

        // 일반 이동
        transform.position += direction * moveDistance;
        distanceTraveled += moveDistance;
    }








    public Actor GetDetectedEnemy()
    {
        return detectedEnemy;
    }

    public bool HasDetectedEnemy()
    {
        return detectedEnemy != null;
    }



    // 트리거 이벤트
    void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;

        // 적 감지
        if (other.CompareTag("Enemy") && detectedEnemy == null)
        {
            detectedEnemy = other.GetComponent<Actor>();
            Debug.Log($"적 감지: {detectedEnemy.name}");

            // 이벤트 호출
            OnEnemyDetected?.Invoke(detectedEnemy);
            this.ReturnToPool(); // 투사체 풀로 반환
        }
    }




    // 기즈모 그리기 (Scene 뷰에서 감지 범위 시각화)
    void OnDrawGizmos()
    {
        if (!showGizmos) return;

        // 감지 범위 표시
        if (capsuleCollider != null)
        {
            Gizmos.color = detectedEnemy != null ? Color.red : Color.green;
            Gizmos.color = new Color(Gizmos.color.r, Gizmos.color.g, Gizmos.color.b, 0.3f);
            Gizmos.DrawSphere(transform.position, capsuleCollider.radius);

            // 와이어프레임으로 경계선 표시
            Gizmos.color = detectedEnemy != null ? Color.red : Color.green;
            Gizmos.DrawWireSphere(transform.position, capsuleCollider.radius);
        }

        // 투사체 중심점 표시
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 0.1f);
    }

    void OnDrawGizmosSelected()
    {
        // 선택되었을 때 더 명확하게 표시
        if (capsuleCollider != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, capsuleCollider.radius);
        }
    }
}
