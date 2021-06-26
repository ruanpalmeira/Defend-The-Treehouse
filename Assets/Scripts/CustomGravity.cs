﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[RequireComponent(typeof(Rigidbody))]
public class CustomGravity : MonoBehaviour{
    public float gravityScale = 1.0f;
    public static float globalGravity = -9.81f;
    Rigidbody m_rb;
 
    void OnEnable (){
        m_rb = GetComponent<Rigidbody>();
        m_rb.useGravity = false;
    }
 
    void FixedUpdate (){
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        m_rb.AddForce(gravity, ForceMode.Acceleration);
    }

    private void OnCollisionStay(Collision other) {
        if(other.gameObject.tag == "Floor"){
            gravityScale = 0.6f;
        }
    }
    
    private void OnCollisionExit(Collision other) {
        if (other.gameObject.tag == "Floor"){
            gravityScale = 3.0f;
        }
    }
}