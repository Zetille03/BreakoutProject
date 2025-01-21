using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;
    private int totalScore = 0;
    private int totalLives = 4;
    private int bricksDestroyed = 0;
    private UiManager uiManager;
    private GameObject _player;
    private PlayerMovement _playerScript;
    private ProjectileMovement _proyectil;
    private Vector2 _playerInitialPosition;
    

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name != "MenuScene" && Instance == null)
        {
            SceneManager.LoadScene("MenuScene");
        }
        if (Instance != null && SceneManager.GetActiveScene().name!="MenuScene")
        {
            Instance.totalScore = 0;
            Instance.totalLives = 4;
            Instance.bricksDestroyed = 0;
            Instance._player = GameObject.FindWithTag("Player");
            Instance._playerScript = Instance._player.GetComponent<PlayerMovement>();
            Instance._proyectil = GameObject.FindWithTag("Ball").GetComponent<ProjectileMovement>();
            Instance._playerInitialPosition = Instance._player.transform.position;
            Instance.uiManager = GameObject.Find("UiManager").GetComponent<UiManager>();
            
            Destroy(this.gameObject);
            return;
        }else if (Instance != null && SceneManager.GetActiveScene().name == "MenuScene")
        {
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
        Instance.bricksDestroyed++;
        Instance.totalScore += score;
        Instance.uiManager.setScore(Instance.totalScore);
        if (Instance.bricksDestroyed >= Instance._playerScript.numberOfBricks)
        {
            Instance.uiManager.showWinPanel();
        }
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
