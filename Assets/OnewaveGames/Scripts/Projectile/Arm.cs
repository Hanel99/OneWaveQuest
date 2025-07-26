using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : Projectile
{
    public Transform attachPoint; // 적 담아놓을 오브젝트
    public Enemy enemy; // 팔이 붙잡을 적

    [Header("스킬 데이터")]
    public ArmSkillData skillData;

    [Header("디버그")]
    [SerializeField] private bool showGizmos = true;

    // 이벤트
    public System.Action<Actor> OnEnemyDetected;
    public System.Action<Actor> OnPlayerReached;

    // 상태
    public bool IsActive => isMoving;
    private Actor detectedEnemy;


    // 이동 관련 변수
    private Vector3 direction;
    private float distanceTraveled;
    private float speed;

    private bool isMoving = false;
    private bool isPushing = true;

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

    public void Initialize(bool push = true)
    {
        isPushing = push;
        Debug.Log($"팔 초기화: {this.name}, 푸시 상태: {isPushing}");

        if (isPushing)
        {
            capsuleCollider.radius = skillData.detectionRadius;
            speed = skillData.armSpeed;
            OnEnemyDetected = null;
            detectedEnemy = null;
        }
        else
        {
            capsuleCollider.radius = skillData.detectionRadius * skillData.enemyPlacementDistance;
            speed = skillData.armSpeed * skillData.returnSpeedMultiplier;
            OnPlayerReached = null;
        }
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

        Debug.Log($"투사체 발사: 방향 {direction}, 속도 {speed}, 최대거리 {skillData.maxDistance}");
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
        float moveDistance = speed * Time.deltaTime;

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
            this.gameObject.SetActive(false); // 투사체 비활성화
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
        // 적 감지
        if (isPushing && other.CompareTag("Enemy") && detectedEnemy == null)
        {
            detectedEnemy = other.GetComponent<Actor>();
            Debug.Log($"적 감지: {detectedEnemy.name}");

            // 이벤트 호출
            isMoving = false;
            OnEnemyDetected?.Invoke(detectedEnemy);
        }
        else if (isPushing == false && other.CompareTag("Player"))
        {
            // 플레이어와 충돌 시
            Debug.Log("플레이어에게 도착");
            isMoving = false;
            OnPlayerReached?.Invoke(other.GetComponent<Player>());
            this.gameObject.SetActive(false); // 투사체 비활성화
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
