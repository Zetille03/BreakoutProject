using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject playerGameObject;
    private Rigidbody2D _rb;

    
    private Vector2 _velocityPrev;
    
    private Vector2 _velocity;
    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _velocityPrev = _rb.velocity;
        LanzamientoInicial();
    }

    public void LanzamientoInicial()
    {
        
        
        _rb.velocity = Vector2.zero;
        transform.position = new Vector3(playerGameObject.transform.position.x, playerGameObject.transform.position.y+0.5f, playerGameObject.transform.position.z);
        _velocity = new Vector2(Random.Range(-.5f, .5f),1);
        
        _rb.AddForce(_velocity*speed, ForceMode2D.Impulse);
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
            // Obtener la velocidad del jugador desde PlayerMovement
            PlayerMovement playerMovement = playerGameObject.GetComponent<PlayerMovement>();
            Vector2 playerVelocity = playerMovement != null ? new Vector2(playerMovement.Velocity.x, 0f) : Vector2.zero;

            // Reflejar la velocidad previa de la pelota
            Vector2 reflectedVelocity = Vector2.Reflect(_velocityPrev, normal);

            // Verificar si el jugador está en movimiento en el eje X
            if (Mathf.Abs(playerVelocity.x) > 0.01f) // Umbral para detectar movimiento significativo
            {
                // Cambiar drásticamente la dirección de la pelota en X según el movimiento del jugador
                float xDirection = Mathf.Sign(playerVelocity.x); // Dirección del movimiento del jugador (-1 o 1)
                reflectedVelocity.x = xDirection * Mathf.Abs(reflectedVelocity.x);
            }

            // Asignar la nueva velocidad normalizada
            _rb.velocity = reflectedVelocity.normalized * _velocityPrev.magnitude;
            
            // Vector2 contactLocal = contact.point - (Vector2)otherObject.transform.position;
            // Vector2 newAngle = Vector2.Reflect(_velocityPrev, normal);
            // float angulo = Vector2.Angle(newAngle, normal);
            // if ((contactLocal.x < -0.5f || contactLocal.x > 0.5f) && angulo < 45f)
            // {
            //     Vector2 newDirection;
            //     if (_rb.velocity.x <= 0)
            //     {
            //         newDirection = new Vector2(-Mathf.Sqrt(2) / 2, Mathf.Sqrt(2) / 2);
            //     }
            //     else
            //     {
            //         newDirection = new Vector2(Mathf.Sqrt(2) / 2, Mathf.Sqrt(2) / 2);
            //     }
            //     _rb.velocity = newDirection * _rb.velocity.magnitude;
            // }
            // else
            // {
            //     _rb.velocity = Vector3.Reflect(_velocityPrev, normal);
            //
            // }
            
        }else if (otherObject.CompareTag("Floor"))
        {
            GameManager.Instance.RestLife();
            
        }
    }

    
}
