using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public GameObject myCamera, bullet, hero;
    public Rigidbody rgd;
    public float speed = 80.0f;
    public float jumpForce = 85.0f;
    public string direction = "right";
    public Vector3 spacing = new Vector3(0, 0.4f, 0);
    public Transform model;
    public int extraJumps = 2;
    public bool rotatedleft = true;
    public bool rotatedright = false;
    public int slimesKilled;
    public AudioSource enemyDeath;

    public int maxHealth = 100;
    public int currentHealth;
    public GameController gameController;
    public bool isAlive = true;

    void Start() {
        currentHealth = maxHealth;
        gameController.SetMaxHealth(maxHealth);
        rgd = gameObject.GetComponent<Rigidbody>();
        enemyDeath = GetComponent<AudioSource>();
    }

    void Update(){
        if(Input.GetKey(KeyCode.D)){
            rotatedleft = false;
            if(!rotatedright){
                hero.transform.Rotate(Vector3.up, 180);
                rotatedright = true;
            }

            direction = "right"; 
            transform.RotateAround(myCamera.transform.position, Vector3.up, speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.A)){
            rotatedright = false;
            if(!rotatedleft){
                hero.transform.Rotate(Vector3.up, 180);
                rotatedleft = true;
            }

            direction = "left"; 
            transform.RotateAround(myCamera.transform.position, Vector3.down, speed * Time.deltaTime); 
        }

        if((Input.GetKeyDown(KeyCode.Space)||(Input.GetKeyDown(KeyCode.W))) && extraJumps > 0){
            rgd.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }

        
    }
    private void LateUpdate() {
        if(Input.GetMouseButtonDown(0)){
            if (isAlive){
                GameObject gb = Instantiate(bullet, model.position + spacing, this.transform.rotation);
                gb.gameObject.GetComponent<AudioSource>().Play();
                gb.transform.rotation = FindClosestEnemy();
                gb.GetComponent<Rigidbody>().velocity = model.forward * 15;
                Destroy(gb, 1f);
            }
        }
    }

    private void OnCollisionStay(Collision other) {
        if(other.gameObject.tag == "Floor"){
            extraJumps = 2;
        }
    
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Slime"){
            TakeDamage(1);
        }
    }

    public void EnemyKilled(){
        enemyDeath.Play();
        slimesKilled++;
        gameController.updateScore(slimesKilled);
    }

    void TakeDamage(int damage){
        currentHealth -= damage;
        gameController.SetHealth(currentHealth);
        if(currentHealth <= 0){
            isAlive = false;
            gameController.setGameOver(slimesKilled);
            //gameObject.SetActive(false);
            gameObject.GetComponentInChildren<Light>().enabled = false;
        }
    }

    public Quaternion FindClosestEnemy(){
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Slime");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos){
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance){
                closest = go;
                distance = curDistance;
            }
        }
        return closest.transform.rotation;
    }
}