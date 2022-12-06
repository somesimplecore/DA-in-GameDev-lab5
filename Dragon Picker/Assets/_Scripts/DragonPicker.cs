using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using TMPro;

public class DragonPicker : MonoBehaviour
{
    private void OnEnable() => YandexGame.GetDataEvent += GetLoadSave;
    private void OnDisable() => YandexGame.GetDataEvent += GetLoadSave;

    public GameObject energyShieldPrefab;
    public int numEnergyShield = 3;
    public float energyShieldBottomY = -6f;
    public float energyShieldRadius = 1.5f;
    public TextMeshProUGUI scoreGT;
    public TextMeshProUGUI playerName;
    public List<GameObject> shieldList;
    void Start()
    {
        if(YandexGame.SDKEnabled)
        {
            GetLoadSave();
        }

        shieldList = new List<GameObject>();

        for (int i = 1; i <= numEnergyShield; i++)
        {
            GameObject tShieldGo = Instantiate<GameObject>(energyShieldPrefab);
            tShieldGo.transform.position = new Vector3(0, energyShieldBottomY, 0);
            tShieldGo.transform.localScale = new Vector3(1 * i, 1 * i, 1 * i);
            shieldList.Add(tShieldGo);
        }
    }

    public void DragonEggDestroyed()
    {
        GameObject[] tDragonEggArray = GameObject.FindGameObjectsWithTag("Dragon Egg");
        foreach (GameObject tGO in tDragonEggArray)
            Destroy(tGO);
        int shieldIndex = shieldList.Count - 1;
        GameObject tShieldGo = shieldList[shieldIndex];
        shieldList.RemoveAt(shieldIndex);
        Destroy(tShieldGo);

        if (shieldList.Count == 0)
        {
            GameObject scoreGO = GameObject.Find("Score");
            scoreGT = scoreGO.GetComponent<TextMeshProUGUI>();
            if(!YandexGame.savesData.achievements.Contains("Береги щиты!"))
            {
                YandexGame.savesData.achievements.Add("Береги щиты!");  
            }    
            SaveData(int.Parse(scoreGT.text));
            YandexGame.NewLeaderboardScores("TopPlayerScore", YandexGame.savesData.bestScore);
            SceneManager.LoadScene("_0Scene");
            GetLoadSave();
        }
    }

    public void GetLoadSave()
    {
        Debug.Log(YandexGame.savesData.score);
        playerName = GameObject.Find("PlayerName").GetComponent<TextMeshProUGUI>();
        playerName.text = YandexGame.playerName;

    }

    public void SaveData(int currentScore)
    {
        YandexGame.savesData.score = currentScore;
        if(currentScore > YandexGame.savesData.bestScore)
        {
            YandexGame.savesData.bestScore = currentScore;
        }
        YandexGame.SaveProgress();
    }
}
