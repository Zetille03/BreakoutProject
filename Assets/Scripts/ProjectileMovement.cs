using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProjectileMovement : MonoBehaviour
{
    [Header("Audio References")]
    private AudioSource _audioSource;
    [SerializeField] private AudioClip impactBrickAudioSource;
    
    [Header("Object References")]
    private Rigidbody2D _rb;
    [SerializeField] private GameObject playerGameObject;
    
    [Header("Movement Settings")]
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float angleMultiplier = 2f;
    [SerializeField] private float speedIncrement = 0.5f;
    
    [SerializeField] private Vector2 _velocityPrev;
    private float _currentSpeed; 
    private Vector2 _velocity;
    
    private const float MIN_ANGLE_RAD = Mathf.PI / 4;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _currentSpeed = baseSpeed;
        LanzamientoInicial();
    }

    public void ReseteoPunto()
    {
        _currentSpeed = baseSpeed;
        
        LanzamientoInicial();
    }
    
    private void LanzamientoInicial()
    {
        _rb.velocity = Vector2.zero;
        transform.position = new Vector3(playerGameObject.transform.position.x, playerGameObject.transform.position.y+0.5f, playerGameObject.transform.position.z);
        Vector2 initialVelocity = new Vector2(Random.Range(-0.5f, 0.5f), 1);
        _rb.AddForce(initialVelocity.normalized * _currentSpeed, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        _velocityPrev = _rb.velocity;
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        GameObject otherObject = other.gameObject;
        ContactPoint2D contact = other.contacts[0];
        Vector2 normal = contact.normal;
        
        if (otherObject.CompareTag("Player"))
        {
            ManejarColisionPlayer(contact, normal);
           
            
        }else if (otherObject.CompareTag("Floor"))
        {
            otherObject.GetComponent<AudioSource>().Play();
            ManejarColisionSuelo();
            
        }else if (otherObject.CompareTag("Brick"))
        {
            ManejarColisionLadrillos(otherObject,normal);
        }else if (otherObject.CompareTag("PowerUp"))
        {
            ManejarColisionPowerUps(otherObject, normal);
        }else if (otherObject.CompareTag("Wall") || otherObject.CompareTag("Roof"))
        {
            _audioSource.Play();
        }
        
    }

    
    private void ManejarColisionPlayer(ContactPoint2D contact, Vector2 normal)
    {
        PlayerMovement playerMovement = playerGameObject.GetComponent<PlayerMovement>();
        Vector2 playerVelocity = playerMovement != null ? playerMovement.Velocity : Vector2.zero;

        float paddleWidth = playerGameObject.GetComponent<Collider2D>().bounds.size.x;
        float impactOffset = (contact.point.x - playerGameObject.transform.position.x) / (paddleWidth / 2);

        Vector2 reflectedVelocity = Vector2.Reflect(_velocityPrev, normal);

        reflectedVelocity.x += impactOffset * angleMultiplier;
        reflectedVelocity.x += playerVelocity.x * 0.5f;

        reflectedVelocity.y = Mathf.Abs(reflectedVelocity.y);
        reflectedVelocity = EnsureMinimumAngle(reflectedVelocity);

        
        _rb.velocity = reflectedVelocity.normalized * _currentSpeed;
        _audioSource.Play();
    }
    
    private void ManejarColisionSuelo()
    {
        _currentSpeed = baseSpeed;
        GameManager.Instance.RestLife();
    }

    private void ManejarColisionLadrillos(GameObject brick, Vector2 normal)
    {
        brick.GetComponent<BrickBehaviour>().OnBrickHited?.Invoke();
        
        _currentSpeed += speedIncrement;

        Vector2 reflectedVelocity = Vector2.Reflect(_velocityPrev, normal);
        // reflectedVelocity = EnsureMinimumYVelocity(reflectedVelocity);
        _rb.velocity = reflectedVelocity.normalized * _currentSpeed;
        _audioSource.PlayOneShot(impactBrickAudioSource);
    }
    
    private void ManejarColisionPowerUps(GameObject brick, Vector2 normal)
    {
        brick.GetComponent<BrickPowerUpBehaviour>().OnBrickHited?.Invoke();
        
        _currentSpeed += speedIncrement;

        Vector2 reflectedVelocity = Vector2.Reflect(_velocityPrev, normal);
        // reflectedVelocity = EnsureMinimumYVelocity(reflectedVelocity);
        _rb.velocity = reflectedVelocity.normalized * _currentSpeed;
        _audioSource.PlayOneShot(impactBrickAudioSource);
    }
    
    private Vector2 EnsureMinimumAngle(Vector2 velocity)
    {
        float angle = Mathf.Atan2(Mathf.Abs(velocity.y), Mathf.Abs(velocity.x));

        if (angle < MIN_ANGLE_RAD)
        {
            float newY = Mathf.Abs(velocity.x) * Mathf.Tan(MIN_ANGLE_RAD);
            velocity.y = Mathf.Sign(velocity.y) * newY;
        }

        return velocity.normalized * _currentSpeed;
    }
    
    
    
}
