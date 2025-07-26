using UnityEngine;

[CreateAssetMenu(fileName = "Arm Skill Data", menuName = "ScriptableObjects/Arm Skill Data")]
public class ArmSkillData : ScriptableObject
{
    [Header("프리팹 설정")]
    [Tooltip("팔 투사체 프리팹")]
    public GameObject armProjectilePrefab;

    [Header("기본 설정")]
    [Tooltip("팔 이동 속도")]
    public float armSpeed = 30f;

    [Tooltip("최대 던지기 거리")]
    public float maxDistance = 20f;

    [Tooltip("에너미 감지 반경")]
    public float detectionRadius = 1f;



    [Header("데미지 설정")]
    [Tooltip("잡힌 에너미에게 주는 데미지")]
    public int enemyDamage = 50;



    [Header("고급 설정")]
    [Tooltip("팔 던지기 쿨다운 시간")]
    public float cooldownTime = 3f;

    [Tooltip("에너미를 플레이어 앞에 배치하는 거리")]
    public float enemyPlacementDistance = 3f;

    [Tooltip("복귀 시 속도 배율 (1.0 = 던질 때와 같은 속도)")]
    public float returnSpeedMultiplier = 1.5f;
}