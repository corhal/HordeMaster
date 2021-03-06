﻿using UnityEngine;
using System.Collections;

public class CameraMover : MonoBehaviour {
    public float Speed = 6f;

    Vector3 movement;
    
    Rigidbody playerRigidbody;    

    void Awake() {        
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);        
    }

    void Move(float h, float v) {
        movement.Set(h, 0f, v);

        movement = movement.normalized * Speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }
}
