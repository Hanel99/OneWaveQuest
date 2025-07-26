using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamaManager : MonoBehaviour
{
    public static GamaManager Instance { get; private set; }

    public Player player;
    public Enemy enemy;


    // 게임에 필요한 데이터들. 임시로 게임매니저에서 통합 관리.
    public ActorStatData playerStatData;
    public ActorStatData enemyStatData;
    public ArmSkillData armSkillData;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeGameData();
    }

    public void InitializeGameData()
    {
        // 게임 시작 시 필요한 데이터 초기화
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }

        if (enemy == null)
        {
            enemy = FindObjectOfType<Enemy>();
        }
    }
}
