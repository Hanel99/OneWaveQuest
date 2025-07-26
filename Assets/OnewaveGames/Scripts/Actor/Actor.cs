using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public CharacterController characterController;
    public ActorStatData actorStatData;

    public virtual void ApplySkill(Actor target)
    {

    }

    public virtual void ApplySkill(Vector3 targetPosition)
    {

    }

    public virtual void ApplySkill(Transform transform)
    {

    }


    public void Start()
    {
        // 컴포넌트 가져오기
        characterController = GetComponent<CharacterController>();
        InitData();
    }
    public virtual void InitData()
    {
        SetStatData();
    }

    public void SetStatData()
    {
        actorStatData.SetCurrentHealth(actorStatData.MaxHealth);
        actorStatData.SetCurrentMana(actorStatData.MaxMana);
    }


    public void RestoreMana(int amount)
    {
        int newMana = actorStatData.CurrentMana + amount;
        newMana = Mathf.Min(newMana, actorStatData.MaxMana);
        actorStatData.SetCurrentMana(newMana);
    }

    public void RestoreHealth(int amount)
    {
        int newHealth = actorStatData.CurrentHealth + amount;
        newHealth = Mathf.Min(newHealth, actorStatData.MaxHealth);
        actorStatData.SetCurrentHealth(newHealth);
    }

    public void TakeDamage(int damage)
    {
        int newHealth = actorStatData.CurrentHealth - damage;
        actorStatData.SetCurrentHealth(newHealth);
        if (actorStatData.CurrentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log($"{gameObject.name} has died.");

        this.gameObject.SetActive(false);
        // 이후 추가적인 사망 로직 구현
    }

    public void SetMoveSpeed(float speed)
    {
        actorStatData.SetMoveSpeed(speed);
    }

    public void SetAttackPower(int power)
    {
        actorStatData.SetAttackPower(power);
    }
}
