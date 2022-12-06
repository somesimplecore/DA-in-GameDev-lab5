using UnityEngine;
using YG;
using TMPro;

public class CheckConnectYG : MonoBehaviour
{
    private void OnEnable() => YandexGame.GetDataEvent += CheckSDK;
    private void OnDisable() => YandexGame.GetDataEvent -= CheckSDK;
    private TextMeshProUGUI bestScore;
    public TextMeshProUGUI achievements;
    void Start()
    {
        if(YandexGame.SDKEnabled)
        {
            CheckSDK();
        }
    }
    
    public void CheckSDK()
    {
        if (YandexGame.auth)
        {
            Debug.Log("User auth ok");
        }
        else
        {
            YandexGame.AuthDialog();
            Debug.Log("User not auth");
        }

        bestScore = GameObject.Find("BestScore").GetComponent<TextMeshProUGUI>();
        bestScore.text = "Best score: " + YandexGame.savesData.bestScore.ToString();

        if(YandexGame.savesData.achievements.Count == 0)
        {
            
        }
        else
        {
            foreach(var value in YandexGame.savesData.achievements)
            {
                achievements.text = achievements.text + value + '\n';
            }
        }
    }
}
