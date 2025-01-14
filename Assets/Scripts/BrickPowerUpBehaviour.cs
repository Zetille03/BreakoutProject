using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPowerUpBehaviour : MonoBehaviour
{
    [SerializeField] public int points;
    [SerializeField] public Sprite powerUpSprite;
    private Rigidbody2D _rb;
    private BoxCollider2D _col;
    
    private SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<BoxCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;

        if (other.CompareTag("Ball"))
        {
            _rb.constraints = RigidbodyConstraints2D.None;
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            _rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            _col.isTrigger = true;
            _rb.gravityScale = 1;
            _spriteRenderer.sprite = powerUpSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
