using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamaManager : MonoBehaviour
{
    public static GamaManager Instance { get; private set; }

    public Player player;
    public Enemy enemy;



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


    public void OnReset()
    {
        Debug.Log("F5 pressed, reloading scene...");
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
