using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : GenericSingletonClass<GameManager> {

    public int floorSize;
    public GameObject PlayerPrefab;
    public Floor floor;
    public Tile[,] tiles;
    public GameObject Player;
    public Transform Center;

    bool gameOver = false;
    public bool GameOver { get { return gameOver; } set { gameOver = value; } }

    public int lifes;
    int currentlifes;

    int highScore;
    int currentScore;
    int secondsPlayed;
    float multiplier = 1.0f;
    public int pointsPerSecond = 10;

    float timer1 = 0;


	void Start () {
        Player = Instantiate(PlayerPrefab, new Vector3(floorSize, 1 , floorSize ), Quaternion.identity);
        tiles = floor.Tiles;

        //Display the Booster and jump on the ui
        //Player.GetComponent<PlayerMovement>().GetBoostDelay()
	}

    public void ApplyDamage(int amount)
    {
        currentlifes -= amount;
        if(currentlifes <= 0)
        {
            gameOver = true;
        }
    }

    public void AddToScore(int amount)
    {
        currentScore += amount;
    }
	
    
	
	void Update () {
        if (gameOver)
        {
            //look if score is heiger than highscore

            SceneManager.LoadScene(0);
        }
        if (Time.time - timer1 >= 1.0f)
        {
            timer1 = Time.time;
            secondsPlayed++;
            AddToScore(Mathf.RoundToInt(pointsPerSecond * multiplier));
        }
        

	}

 
}
