using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour{
    public GameObject slime;
    public float interval = 0.5f;
    public Vector3 spawn = new Vector3(11, -31, 65);
    bool isSpawning = false;

    void Update(){
        if(!isSpawning){
            isSpawning = true;
            StartCoroutine("spawnEnemies");
        }
    }

    IEnumerator spawnEnemies(){
        yield return new WaitForSeconds(interval);
            GameObject gb = Instantiate(slime, transform.position, transform.rotation);
            gb.transform.parent = transform;
            isSpawning = false;
        if(interval < 0.5f){
            interval = 3.0f;
        }else{
            interval -= 0.01f;
        }
    }
}