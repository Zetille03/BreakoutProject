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
    [SerializeField] private GameObject player;
    [SerializeField] private ProjectileMovement proyectil;
    private Vector2 _playerInitialPosition;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        _playerInitialPosition = player.transform.position;
        uiManager = GameObject.Find("UiManager").GetComponent<UiManager>();
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
        totalScore += score;
        uiManager.setScore(totalScore);
    }

    public void RestLife()
    {
        totalLives--;
        uiManager.setLifes(totalLives);
        if (totalLives <= 0)
        {
            
            return;
        }

        ResetPoint();

    }

    public void ResetPoint()
    {
        player.transform.position = _playerInitialPosition;
        proyectil.LanzamientoInicial();
    }

    public void StartPowerUpCounter()
    {
        uiManager.OnPowerUp?.Invoke();
    }
    
}
