using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float xLimit = 5.4f;
    
    Vector3 moveDirection = Vector3.zero;

    // Update is called once per frame
    void Update()
    {   
        float horizontalInput = Input.GetAxis("Horizontal"); 

        moveDirection = new Vector3(horizontalInput, 0f,0f);

        transform.position += moveDirection * (speed * Time.deltaTime);

        float clampedX = Mathf.Clamp(transform.position.x,-xLimit, xLimit);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        
    }
}
