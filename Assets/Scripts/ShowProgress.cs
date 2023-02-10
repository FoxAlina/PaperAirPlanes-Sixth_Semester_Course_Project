using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowProgress : MonoBehaviour
{
    public Text scoreText;
    public Text enemiesText;
    public Text coinsText;

    float flightMeterRecord;
    int totalKilledEnemies;
    int coins;

    private const string saveKey = "mainSave";

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;

        Load();
        scoreText.text = flightMeterRecord.ToString();
        enemiesText.text = totalKilledEnemies.ToString();
        coinsText.text = coins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Load()
    {
        var data = SaveManager.Load<ProgressData>(saveKey);
        totalKilledEnemies = data.enemies;
        flightMeterRecord = data.maxScore;
        coins = data.coins;
    }
}
