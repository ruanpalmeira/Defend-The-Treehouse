using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour{
    public NavMeshAgent enemy;
    public GameObject Player;
    public int enemyLife = 2;
    public Animator animator;
    public AudioSource jump;
    public CharacterMovement playerAlive;
    
    void Start(){
        enemy = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        jump = GetComponent<AudioSource>();
        Player = GameObject.FindGameObjectWithTag("Player");
        playerAlive = Player.GetComponent<CharacterMovement>();
        enemy.speed = Random.Range(5.0f, 8.0f);
        
    }

    void Update(){
        if(playerAlive.isAlive){
            enemy.SetDestination(Player.transform.position);
        }
    }

    private void OnCollisionEnter(Collision other){
        if(other.transform.tag == "Bullet"){
            enemyLife--;
            if(enemyLife <= 0){
                Player.GetComponent<CharacterMovement>().EnemyKilled();
                Destroy(this.gameObject);
            }else{
                animator.SetInteger("ChangeState", 1);
                animator.SetInteger("ChangeState", 0);
            }
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionExit(Collision other) {
        if(other.transform.tag == "Floor"){
            jump.Play();
        }
    }

    IEnumerator jumpSound(){
        yield return new WaitForSeconds(5f);
        
    }
}