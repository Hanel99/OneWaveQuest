using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set; }

    private Dictionary<System.Type, Queue<Projectile>> poolDictionary = new();

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
        // 초기화 작업
        // RegisterProjectileType(SkillManager.Instance.armProjectilePrefab, 10);
    }

    // 풀에 Projectile 추가
    public void RegisterProjectileType<T>(T prefab, int initialCount) where T : Projectile
    {
        var type = typeof(T);
        if (!poolDictionary.ContainsKey(type))
            poolDictionary[type] = new Queue<Projectile>();

        for (int i = 0; i < initialCount; i++)
        {
            T obj = Instantiate(prefab, transform);
            obj.gameObject.SetActive(false);
            poolDictionary[type].Enqueue(obj);
        }
    }

    // 풀에서 Projectile 꺼내기
    public T GetProjectile<T>() where T : Projectile
    {
        var type = typeof(T);
        if (poolDictionary.ContainsKey(type) && poolDictionary[type].Count > 0)
        {
            var obj = poolDictionary[type].Dequeue() as T;
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            T prefab = SkillManager.Instance.GetProjectilePrefab<T>() as T;
            if (prefab != null)
            {
                T obj = Instantiate(prefab, transform);
                obj.gameObject.SetActive(true);
                return obj;
            }
            else
            {
                Debug.LogError($"No prefab found for type {type}. Check SkillManager.");
                return null;
            }
        }
    }

    // Projectile 반환
    public void ReturnProjectile<T>(T obj) where T : Projectile
    {
        obj.gameObject.SetActive(false);
        var type = typeof(T);
        if (!poolDictionary.ContainsKey(type))
            poolDictionary[type] = new Queue<Projectile>();
        poolDictionary[type].Enqueue(obj);
    }
}


public static class ProjectilePoolExtensions
{
    public static void ReturnToPool<T>(this T projectile) where T : Projectile
    {
        ObjectPool.Instance.ReturnProjectile(projectile);
    }
}