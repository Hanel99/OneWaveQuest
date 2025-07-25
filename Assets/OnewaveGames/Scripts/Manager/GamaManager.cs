using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamaManager : MonoBehaviour
{
    public static GamaManager Instance { get; private set; }

    public Player player;
    public Enemy enemy;

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
    }
}
