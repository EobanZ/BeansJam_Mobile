using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




public class GameManager : GenericSingletonClass<GameManager> {

    [Header("Game Values")]
    public int floorSize;
    public GameObject PlayerPrefab;
    public Floor floor;
    public Tile[,] tiles;
    public GameObject Player;
    public Transform Center;
    public int itemsNeedetToRepairFloor = 10;
    public float getHarderAfterThisIntervall = 5;
  

    bool gameOver = false;
    public bool GameOver { get { return gameOver; } set { gameOver = value; } }

    public int MaxLifes = 10;
    int currentlifes;

    int highScore;
    int currentScore = 0;
    int secondsPlayed = 0;
    float multiplier = 1.0f;
    public float Multiplier { get { return multiplier; } }
    public int pointsPerSecond = 10;
    int repairItemsCollected = 0;

    float timer1 = 0;

    [Space]
    [Header("Colectables")]
    public GameObject Life;
    public GameObject Repair;
    public GameObject Points;

    [Space]
    [Header("UI Elements")]
    public TMPro.TMP_Text score_text;
    public TMPro.TMP_Text health_text;
    public Slider health_slider;
    public TMPro.TMP_Text repaircount_text;
    public Slider repaircount_slider;
    public GameObject GameOverPanel;
    public TMPro.TMP_Text highscoreText;

    [Space]
    [Header("Spawing Items")]
    public GameObject RepairItemPrefab;
    public GameObject HealthItemPrefab;
    public GameObject PointsItemPrefab;
    GameObject[] items = new GameObject[3];
    public float SpawningIntervals = 5;

    private void Awake()
    {
        Time.timeScale =1 ;
        AudioListener.pause = false;
        Player = Instantiate(PlayerPrefab, new Vector3(floorSize, 1, floorSize), Quaternion.identity);
        
    }

    void Start () {

        tiles = floor.Tiles;
        currentlifes = MaxLifes;
        //Display the Booster and jump on the ui
        //Player.GetComponent<PlayerMovement>().GetBoostDelay()

        items[0] = RepairItemPrefab;
        items[1] = HealthItemPrefab;
        items[2] = PointsItemPrefab;

        LoadHighscore();

        //setup UI
        health_slider.maxValue = MaxLifes;
        repaircount_slider.maxValue = itemsNeedetToRepairFloor;
        UpdateUI();

        SpawnItems();
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
        UpdateUI();
    }

    public void AddHealth(int amount)
    {
        currentlifes++;
        if (currentlifes > MaxLifes)
            currentlifes = MaxLifes;
    }
	
    
	
	void Update () {
        
     
        if (gameOver)
        {
            //look if score is heiger than highscore
            if(highScore < currentScore)
            {
                SaveHighscore();
                highScore = currentScore;
            }
            

            GameOverPanel.SetActive(true);
            highscoreText.SetText(highScore.ToString());

            Time.timeScale = 0;
            AudioListener.pause = true;
        }
        if (Time.time - timer1 >= 1.0f)
        {
            timer1 = Time.time;
            secondsPlayed++;
            AddToScore(Mathf.RoundToInt(pointsPerSecond * multiplier));
        }

        if(secondsPlayed >= getHarderAfterThisIntervall)
        {
            getHarderAfterThisIntervall += getHarderAfterThisIntervall;

            multiplier += 0.5f;
        }

        if(secondsPlayed >= SpawningIntervals)
        {
            SpawningIntervals += SpawningIntervals;
            SpawnItems();
        }
        

	}

    void UpdateUI()
    {
        score_text.SetText("Score: " + currentScore.ToString());
        health_text.SetText("Health: " + currentlifes);
        health_slider.value = currentlifes;
        repaircount_text.SetText(repairItemsCollected + "/" + itemsNeedetToRepairFloor);
        repaircount_slider.value = repairItemsCollected;
    }

    public void OnPauseButton()
    {
        Time.timeScale = 0;
        //Open Pause Menu
        //Disable Other Panels
    }

    public void AddRepairItem()
    {
        repairItemsCollected++;
        if(repairItemsCollected >= itemsNeedetToRepairFloor)
        {
            repairItemsCollected = 0;
            RebuildField();
        }
    }

    void RebuildField()
    {
        StartCoroutine(floor.BuildField());
    
    }

    void SpawnItems()
    {
        int posX;
        int posY;

        

         posX = Random.Range(0, 100);
         posY = Random.Range(0, 100);

        var rand = Mathf.RoundToInt(Random.Range(0, 3));
        if (rand >= 3) rand = 0;
        Instantiate(items[rand], new Vector3(posX,20,posY), Quaternion.identity);

        //play sound

    }

    public void LoadHighscore()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0);

    }

    public void SaveHighscore()
    {
        PlayerPrefs.SetInt("highScore", currentScore);
        PlayerPrefs.Save();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

}


