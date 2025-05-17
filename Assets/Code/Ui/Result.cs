using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Result : MonoBehaviour
{


    public GameObject[] results;

    public TMP_Text timeText;

    public TMP_Text earnCoinText;

    public TMP_Text moveTileText;

    public void Win()
    {
        results[0].SetActive(true);
    }

    public void Lose()
    {
        results[1].SetActive(true);
    }

    public void SetResult()
    {
        int minutes = ScoreManager.Instance.adventureMinutes;
        float seconds = ScoreManager.Instance.adventureTime;

        timeText.text = string.Format("{0:F0}:{1:F0}", minutes, seconds);

        earnCoinText.text = ScoreManager.Instance.earnCoinCount.ToString();

        moveTileText.text = ScoreManager.Instance.moveTileCount.ToString();

    }
}
