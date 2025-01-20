using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform leftWall; 
    [SerializeField] private Transform rightWall;
    
    private float _xMin;
    private float _xMax;
    
    [SerializeField] private float xMinPowerUpOffset = 1f; 
    [SerializeField] private float xMaxPowerUpOffset = 1f;
    
    
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 previousPosition = Vector3.zero; 
    public Vector3 Velocity { get; private set; }

    [SerializeField] public float powerUpDuration = 3f;
    private float _timer = 0f;
    public bool isPoweredUp = false;
    [SerializeField] private float initialSize = 2f;
    [SerializeField] private float powerUpSize = 5f;

    public int numberOfBricks;
    

    private void Start()
    {
        numberOfBricks = GameObject.FindGameObjectsWithTag("Brick").Length;
        numberOfBricks += GameObject.FindGameObjectsWithTag("PowerUp").Length;
        _xMin = leftWall.position.x + leftWall.GetComponent<Collider2D>().bounds.extents.x+GetComponent<Collider2D>().bounds.extents.x/2;
        _xMax = rightWall.position.x - rightWall.GetComponent<Collider2D>().bounds.extents.x-GetComponent<Collider2D>().bounds.extents.x/2;
        
        
        transform.localScale = new Vector3(initialSize, transform.localScale.y, transform.localScale.z);
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {   
        float horizontalInput = Input.GetAxis("Horizontal"); 

        moveDirection = new Vector3(horizontalInput, 0f, 0f);

        float adjustedXMin = isPoweredUp ? _xMin + xMinPowerUpOffset : _xMin;
        float adjustedXMax = isPoweredUp ? _xMax - xMaxPowerUpOffset : _xMax;
        
        transform.position += moveDirection * (speed * Time.deltaTime);
        
        float clampedX = Mathf.Clamp(transform.position.x, adjustedXMin,adjustedXMax);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        Velocity = (transform.position - previousPosition) / Time.deltaTime;

        previousPosition = transform.position;

        if (_timer != 0 && isPoweredUp)
        {
            if (_timer <= 0)
            {
                transform.localScale = new Vector3(initialSize,transform.localScale.y,transform.localScale.z);
                _timer = 0f;
                isPoweredUp = false;
                return;
            }
            _timer -= Time.deltaTime;
            
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            GameManager.Instance.StartPowerUpCounter(powerUpDuration);
            _timer = powerUpDuration;
            isPoweredUp = true;
            transform.localScale = new Vector3(powerUpSize,transform.localScale.y,transform.localScale.z);
            Destroy(other.gameObject);
        }
    }
}
