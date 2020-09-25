using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.SetSaveManager(this);
    }

    public void SaveMaxScore(int maxScore)
    {
        if(PlayerPrefs.GetInt("score_key") < maxScore)
        {
            PlayerPrefs.SetInt("score_key", maxScore);
        }
    }

    public int GetMaxScore()
    {
        return PlayerPrefs.GetInt("score_key");
    }

}
