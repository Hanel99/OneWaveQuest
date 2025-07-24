using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Actor Stat Data", menuName = "ScriptableObjects/Actor Stat Data")]
public class ActorStatData : ScriptableObject
{
    [Header("기본 설정")]
    [Tooltip("Actor의 최대 HP")]
    [SerializeField] private int maxHealth = 100;
    [Tooltip("Actor의 현재 HP")]
    [SerializeField] private int currentHealth = 100;


    [Tooltip("Actor의 최대 MP")]
    [SerializeField] private int maxMana = 50;
    [Tooltip("Actor의 현재 MP")]
    [SerializeField] private int currentMana = 50;


    [Tooltip("Actor의 이동 속도")]
    [SerializeField] private float moveSpeed = 5f;


    [Header("공격력 설정")]
    [Tooltip("Actor의 기본 공격력")]
    [SerializeField] private int attackPower = 10;


    // 프로퍼티 (get만 공개)
    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;
    public int MaxMana => maxMana;
    public int CurrentMana => currentMana;
    public float MoveSpeed => moveSpeed;
    public int AttackPower => attackPower;


    // 값 변경 전용 메소드
    public void SetCurrentHealth(int value) => currentHealth = value;
    public void SetCurrentMana(int value) => currentMana = value;
    public void SetMoveSpeed(float value) => moveSpeed = value;
    public void SetAttackPower(int value) => attackPower = value;
}
