using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public GameObject Gate;
    public bool isOpening = true;

    void Update(){
        if(isOpening){
            Gate.transform.Translate(Vector3.up * Time.deltaTime *1);
        }
        if (Gate.transform.position.y > 5f){
            isOpening = false;
        }
    }
}