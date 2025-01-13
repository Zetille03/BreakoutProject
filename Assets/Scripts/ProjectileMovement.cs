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
    
    
    
    private Vector2 _velocity;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        transform.position = new Vector3(playerGameObject.transform.position.x, playerGameObject.transform.position.y+0.5f, playerGameObject.transform.position.z);
        _velocity = new Vector2(Random.Range(-.5f, .5f),1);
        
        _rb.AddForce(_velocity*speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
