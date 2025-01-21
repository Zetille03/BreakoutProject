using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreTxt;
    [SerializeField] private TMP_Text powerUpTxt;
    
    [SerializeField] private Image imageLife1;
    
    [SerializeField] private Image imageLife2;
    
    [SerializeField] private Image imageLife3;
    
    [SerializeField] private Image imageLife4;
    
    [SerializeField] private GameObject lostPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject pauseMenu;
    
    public float powerUpDuration;
    private float _powerUpTimer=0f;
    
    public delegate void PowerUpDelegate();
    
    public PowerUpDelegate OnPowerUp;
    
    // Start is called before the first frame update
    void Start()
    {
        lostPanel.SetActive(false);
        winPanel.SetActive(false);
        pauseMenu.SetActive(false);
        OnPowerUp += setPowerUpCounter;
    }

    // Update is called once per frame
    void Update()
    {
        if (_powerUpTimer > 0)
        {
            _powerUpTimer -= Time.deltaTime;
            if (_powerUpTimer <= 0)
            {
                _powerUpTimer = 0;
                powerUpTxt.text = "00.00";
            }
            else
            {
                powerUpTxt.text = _powerUpTimer.ToString("00.00");
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.activeInHierarchy)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }
    
    public void setLifes(int vidas)
    {
        switch (vidas)
        {
            case 4:
                imageLife1.enabled = true;
                imageLife2.enabled = true;
                imageLife3.enabled = true;
                imageLife4.enabled = true;
                break;
            case 3:
                imageLife1.enabled = true;
                imageLife2.enabled = true;
                imageLife3.enabled = true;
                imageLife4.enabled = false;
                break;
            case 2:
                imageLife1.enabled = true;
                imageLife2.enabled = true;
                imageLife3.enabled = false;
                imageLife4.enabled = false;
                break;
            
            case 1:
                imageLife1.enabled = true;
                imageLife2.enabled = false;
                imageLife3.enabled = false;
                imageLife4.enabled = false;
                break;
            case 0:
                imageLife1.enabled = false;
                imageLife2.enabled = false;
                imageLife3.enabled = false;
                imageLife4.enabled = false;
                showLostPanel();
                break;
        }
    }

    public void keepPlaying()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
    
    private void showLostPanel()
    {
        lostPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void showWinPanel()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    
    private void setPowerUpCounter()
    {
        _powerUpTimer = powerUpDuration;
    }

    public void setScore(int score)
    {
        scoreTxt.text = score.ToString();
    }
    
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
    

    public void LoadLevel2()
    {       
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level2");
    }


    public void RetryCurrentLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
    
}
