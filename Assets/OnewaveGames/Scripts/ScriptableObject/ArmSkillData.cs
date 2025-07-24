using UnityEngine;

[CreateAssetMenu(fileName = "New Arm Skill Data", menuName = "Skills/Arm Skill Data")]
public class ArmSkillData : ScriptableObject
{
    [Header("기본 설정")]
    [Tooltip("팔 이동 속도")]
    public float armSpeed = 10f;

    [Tooltip("최대 던지기 거리")]
    public float maxDistance = 15f;

    [Tooltip("에너미 감지 반경")]
    public float detectionRadius = 1f;



    [Header("데미지 설정")]
    [Tooltip("잡힌 에너미에게 주는 데미지")]
    public int enemyDamage = 50;


    [Header("레이어 설정")]
    [Tooltip("에너미 레이어")]
    public LayerMask enemyLayer = 1 << 6;



    [Header("고급 설정")]
    [Tooltip("팔 던지기 쿨다운 시간")]
    public float cooldownTime = 2f;

    [Tooltip("에너미를 플레이어 앞에 배치하는 거리")]
    public float enemyPlacementDistance = 2f;

    [Tooltip("복귀 시 속도 배율 (1.0 = 던질 때와 같은 속도)")]
    public float returnSpeedMultiplier = 1.2f;
}