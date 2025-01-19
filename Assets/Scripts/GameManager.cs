using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;
    private int totalScore = 0;
    private int totalLives = 4;
    private UiManager uiManager;
    private GameObject _player;
    private ProjectileMovement _proyectil;
    private Vector2 _playerInitialPosition;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("entra");
            Instance._player = GameObject.FindWithTag("Player");
            Instance._proyectil = GameObject.FindWithTag("Ball").GetComponent<ProjectileMovement>();
            Instance._playerInitialPosition = Instance._player.transform.position;
            Instance.uiManager = GameObject.Find("UiManager").GetComponent<UiManager>();
            
            Destroy(this.gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(this);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ResetPoint();
        }
    }

    public void AddScore(int score)
    {
        Instance.totalScore += score;
        Instance.uiManager.setScore(Instance.totalScore);
    }

    public void RestLife()
    {
        Instance.totalLives--;
        Instance.uiManager.setLifes(Instance.totalLives);
        if (Instance.totalLives <= 0)
        {
            
            return;
        }

        ResetPoint();

    }

    public void ResetPoint()
    {
        Instance._player.transform.position = Instance._playerInitialPosition;
        Instance._proyectil.ReseteoPunto();
    }

    public void StartPowerUpCounter(float powerUpTime)
    {
        Instance.uiManager.powerUpDuration = powerUpTime;
        Instance.uiManager.OnPowerUp?.Invoke();
    }
    
}
