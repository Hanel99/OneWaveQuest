using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }
    public List<Skill> skills = new List<Skill>();
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


    public void RegisterSkill(Skill skill)
    {
        if (!skills.Contains(skill))
        {
            skills.Add(skill);
        }
    }

    public void RemoveSkill(EnumHelper.SkillType skillType)
    {
        var skill = skills.Find(s => s.skillType == skillType);
        if (skill != null)
        {
            skills.Remove(skill);
        }
    }

    public Skill GetSkill<T>(EnumHelper.SkillType skillType) where T : Skill
    {
        foreach (var skill in skills)
        {
            if (skill is T && skill.skillType == skillType)
            {
                return skill;
            }
        }
        Debug.LogError($"No skill found of type {typeof(T)}");
        return null;
    }





    public void UseThrowArmSkill(Actor source, Actor target, Vector3 targetPosition)
    {
        var throwArmSkill = new ThrowArmSkill();
        RegisterSkill(throwArmSkill);
        throwArmSkill.SetEffectList(source, target, targetPosition);
        if (throwArmSkill.ApplySkill(source, target, targetPosition))
        {
            Debug.Log("ThrowArmSkill applied successfully.");
        }
        else
        {
            Debug.LogWarning("Failed to apply ThrowArmSkill.");
        }
    }
}
