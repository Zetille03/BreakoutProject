using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreTxt;
    [SerializeField] private TMP_Text powerUpTxt;
    
    [SerializeField] private Image imageLife1;
    
    [SerializeField] private Image imageLife2;
    
    [SerializeField] private Image imageLife3;
    
    [SerializeField] private Image imageLife4;
    
    public float powerUpDuration;
    private float _powerUpTimer=0f;
    
    public delegate void PowerUpDelegate();
    
    public PowerUpDelegate OnPowerUp;
    
    // Start is called before the first frame update
    void Start()
    {
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
                break;
        }
    }

    private void setPowerUpCounter()
    {
        _powerUpTimer = powerUpDuration;
    }

    public void setScore(int score)
    {
        scoreTxt.text = score.ToString();
    }
    
    
    
}
