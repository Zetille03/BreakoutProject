using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPowerUpBehaviour : MonoBehaviour
{
    [SerializeField] public int points;
    [SerializeField] public Sprite powerUpSprite;
    private Rigidbody2D _rb;
    private BoxCollider2D _col;
    
    public delegate void BrickHited();
    
    public BrickHited OnBrickHited;
    
    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<BoxCollider2D>();
        OnBrickHited += BrickHit;
        
    }

    public void BrickHit()
    {
        _audioSource.Play();
        GameManager.Instance.AddScore(points);
        _rb.constraints = RigidbodyConstraints2D.None;
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        _rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        _col.isTrigger = true;
        _rb.gravityScale = 1;
        _spriteRenderer.sprite = powerUpSprite;
        
        
    }
}
