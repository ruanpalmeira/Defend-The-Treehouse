using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour{

    public Slider slider;
    public Text gameOverText;
    public Text scoreText;
    bool gameOver = false;

    // Start is called before the first frame update
    void Start(){
        gameOverText.enabled = false;
    }
    private void Update() {
        if(gameOver && Input.GetMouseButtonDown(0)){
            restartGame();
        }
    }

    public void setGameOver(int slimesKilled){
        gameOver = true;
        slider.enabled = false;
        scoreText.enabled = false;
        gameOverText.enabled = true;
        gameOverText.text = "You killed " + slimesKilled + " Slimes! Click to restart";
    }

    public void updateScore(int slimesKilled){
        scoreText.text = "Slimes: " + slimesKilled;
    }

    public void restartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void SetMaxHealth(int health){
        slider.maxValue = health;
        slider.value = health;
    }
    
    public void SetHealth(int health){
        slider.value = health;
    }
}
