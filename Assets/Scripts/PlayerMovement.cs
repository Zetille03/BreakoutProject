using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float xMin = -6.4f;
    [SerializeField] private float xMax = 4.5f;
    [SerializeField] private float xMinPowerUp = -5f;
    [SerializeField] private float xMaxPowerUp = 3f;
    
    
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 previousPosition = Vector3.zero; 
    public Vector3 Velocity { get; private set; }

    [SerializeField] public float powerUpDuration = 3f;
    private float _timer = 0f;
    public bool isPoweredUp = false;
    [SerializeField] private float initialSize = 2f;
    [SerializeField] private float powerUpSize = 5f;
    
    

    private void Start()
    {
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {   
        float horizontalInput = Input.GetAxis("Horizontal"); 

        moveDirection = new Vector3(horizontalInput, 0f, 0f);

        transform.position += moveDirection * (speed * Time.deltaTime);
        
        float clampedX = Mathf.Clamp(transform.position.x, (isPoweredUp)?xMinPowerUp:xMin,(isPoweredUp)?xMaxPowerUp:xMax);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        // Calcular la velocidad basada en el cambio de posición
        Velocity = (transform.position - previousPosition) / Time.deltaTime;

        // Actualizar la posición previa
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
            _timer = powerUpDuration;
            isPoweredUp = true;
            transform.localScale = new Vector3(powerUpSize,transform.localScale.y,transform.localScale.z);
        }
    }
}
