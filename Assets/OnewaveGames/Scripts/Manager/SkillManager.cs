using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }

    [Header("투사체 프리팹(어드레서블로 분리 필요)")]
    public Arm armProjectilePrefab;
    public List<Skill> skills = new List<Skill>();

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




    // 프로젝타일 프리팹 가져오는 코드... 어드레서블로 분리 필요
    public Projectile GetProjectilePrefab<T>() where T : Projectile
    {
        if (typeof(T) == typeof(Arm)) return armProjectilePrefab;

        Debug.LogError($"No projectile prefab found for type {typeof(T)}");
        return null;
    }




    public void RegisterSkill(Skill skill)
    {
        if (!skills.Contains(skill))
        {
            skills.Add(skill);
        }
    }

    public void UnregisterSkill(Skill skill)
    {
        if (skills.Contains(skill))
        {
            skills.Remove(skill);
        }
    }
}
