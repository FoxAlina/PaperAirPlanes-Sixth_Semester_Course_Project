using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScoreHealthCoinsManager : MonoBehaviour
{
    public Text scoreCounter;
    public Text healthCounter;
    public Slider healthBar;
    public Text enemiesCounter;
    public Text hitsCounter;
    public Text bulletsCounter;
    public Slider bulletsBar;
    public Text ringsCounter;

    public Text bulletsIncrease;
    public Text healthIncrease;

    float score;
    public int maxHealth = 10;
    int health;
    int enemies;
    int hits;
    int max_bullets;
    int bullets;
    int rings;

    public GameObject gameOverPanel;
    public Text scoreResult;
    public Text coinsResult;
    public Text hitsResult;
    public Text enemiesResult;
    public Text ringsResult;

    public Player playerScript;
    [HideInInspector]
    public bool isGameOver;
    bool isPausedGame;

    private const string saveKey = "mainSave";

    void Start()
    {
        score = 0f;
        health = maxHealth;
        healthBar.maxValue = health;
        healthBar.value = health;
        healthCounter.text = health.ToString();
        gameOverPanel.SetActive(false);

        bullets = playerScript.maxAmountBullets;
        bulletsCounter.text = bullets.ToString();
        bulletsBar.maxValue = playerScript.maxAmountBullets;
        bulletsBar.value = bullets;

        isPausedGame = false;

        _saveProgress = 0;

        // Reset saved progress!
        //resetProgress();
        //Load();
        //Debug.Log(totalKilledEnemies + "  " + flightMeterRecord + "  " + globalCoins);
    }

    void Update()
    {
        isGameOver = playerScript.isGameOver;
        if (playerScript.isGameOver)
        {
            Time.timeScale = 0f;

            if (_saveProgress == 0)
            {
                saveProgress();
                //Debug.Log(totalKilledEnemies + "  " + flightMeterRecord + "  " + globalCoins);

                _saveProgress++;
            }

            gameOverPanel.SetActive(true);
            scoreResult.text = score.ToString("F");
            hitsResult.text = hits.ToString();
            enemiesResult.text = enemies.ToString();
            ringsResult.text = rings.ToString();

            coinsResult.text = coinsIncrease.ToString();
        }
        else if (!isPausedGame)
        {
            _saveProgress = 0;

            score += playerScript.getMeters() / 40;

            bullets = playerScript.bullets;

            hits = playerScript.hits;

            enemies = playerScript.enemies;


            if (playerScript.getMainDamage)
            {
                this.damage();
                playerScript.getMainDamage = false;
            }
            if (playerScript.getBigDamage)
            {
                this.damage(playerScript.bigDamage);
                playerScript.getBigDamage = false;
            }

            if (playerScript.ring)
            {
                int bul_temp = bullets + playerScript.bulletIncrease;
                int health_temp = health + playerScript.healthIncrease;

                if (bul_temp <= playerScript.maxAmountBullets)
                {
                    bulletsIncrease.text = "+" + playerScript.bulletIncrease.ToString();

                    bullets += playerScript.bulletIncrease;
                    playerScript.bullets = bullets;
                }
                else
                {
                    bulletsIncrease.text = "+" + (playerScript.maxAmountBullets - bullets).ToString();

                    bullets = playerScript.maxAmountBullets;
                    playerScript.bullets = bullets;
                }

                if (health_temp <= maxHealth)
                {
                    healthIncrease.text = "+" + playerScript.healthIncrease.ToString();

                    health += playerScript.healthIncrease;
                }
                else
                {
                    healthIncrease.text = "+" + (maxHealth - health).ToString();

                    health = maxHealth;
                }

                healthBar.value = health;
                healthCounter.text = health.ToString();

                playerScript.ring = false;

                rings++;

                StartCoroutine(wait());
                //StartCoroutine(ExampleCoroutine());
            }

            ringsCounter.text = rings.ToString();

            scoreCounter.text = score.ToString("F");

            bulletsCounter.text = bullets.ToString();
            bulletsBar.value = bullets;
            //Debug.Log(bulletsBar.value +" - "+ bullets);

            hitsCounter.text = hits.ToString();

            enemiesCounter.text = enemies.ToString();
        }
    }

    //IEnumerator ExampleCoroutine()
    //{
    //    //Print the time of when the function is first called.
    //    Debug.Log("Started Coroutine at timestamp : " + Time.time);

    //    //yield on a new YieldInstruction that waits for 5 seconds.
    //    yield return new WaitForSeconds(5);

    //    //After we have waited 5 seconds print the time again.
    //    Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    //}

    void clean()
    {
        bulletsIncrease.text = "";
        healthIncrease.text = "";
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(2.0f);
        clean();
    }

    public void damage(int damage = 1)
    {
        health -= damage;
        if (health >= 0)
        {
            healthBar.value = health;
            healthCounter.text = health.ToString();
        }
        if (health == 0)
        {
            playerScript.isGameOver = true;
        }
    }

    public void pauseGame()
    {
        isPausedGame = true;
    }

    public void resumeGame()
    {
        isPausedGame = false;
    }

    private void resetProgress()
    {
        Save();
    }

    private void calculationOfProgress()
    {
        totalKilledEnemies += this.enemies;
        if (flightMeterRecord < score)
            flightMeterRecord = score;
        coinsIncrease += 10 * enemies;
        coinsIncrease += 5 * hits;
        coinsIncrease += 10 * rings;
        int temp = (int)Math.Log(score);
        coinsIncrease += (temp >= 0) ? temp : 0;
        globalCoins += coinsIncrease;
    }

    private void saveProgress()
    {
        Load();

        calculationOfProgress();

        Save();
    }

    int _saveProgress = 0;
    int totalKilledEnemies = 0;
    float flightMeterRecord = 0;
    int globalCoins = 0;
    int coinsIncrease = 0;

    private void Load()
    {
        var data = SaveManager.Load<ProgressData>(saveKey);
        totalKilledEnemies = data.enemies;
        flightMeterRecord = data.maxScore;
        globalCoins = data.coins;
    }

    private void Save()
    {
        SaveManager.Save(saveKey, GetSaveSnapshot());
    }

    private ProgressData GetSaveSnapshot()
    {
        var data = new ProgressData()
        {
            enemies = this.totalKilledEnemies,
            maxScore = this.flightMeterRecord,
            coins = this.globalCoins,
        };
        return data;
    }
}
