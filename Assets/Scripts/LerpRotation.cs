using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpRotation : MonoBehaviour{
    Vector3 relativePosition;
    Quaternion targetRotation;
    public Transform target;
    public Transform targetModel;
    public float speed = 0.1f;
    public Vector3 offset;
    public bool isCamera = false;

    void LateUpdate(){
        relativePosition = target.position - transform.position;
        targetRotation = Quaternion.LookRotation(relativePosition);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.time * speed);
       
        
    }
    private void FixedUpdate() {
        if(isCamera){
            Vector3 desiredPosition = new Vector3(transform.position.x, targetModel.position.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, 0.1f);
            transform.position = smoothedPosition;
        }
    }
}