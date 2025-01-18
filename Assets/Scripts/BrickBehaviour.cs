using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBehaviour : MonoBehaviour
{
    [SerializeField] private int lives;
    [SerializeField] private Sprite breakedBrick;
    [SerializeField] public int points;
    
    public delegate void BrickHited();
    
    public BrickHited OnBrickHited;
    
    private SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        OnBrickHited += BrickHit;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BrickHit()
    {
            lives--;
            switch (lives)
            {
                case 1:
                    _spriteRenderer.sprite = breakedBrick;
                    break;
                case 0:
                    GameManager.Instance.AddScore(points);
                    Destroy(this.gameObject);
                    break;
                case <=-1:
                    break;
            }
    }
}
