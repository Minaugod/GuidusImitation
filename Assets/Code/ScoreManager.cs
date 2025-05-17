using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public bool startAdventure;

    public float adventureTime;

    public int adventureMinutes;

    public int moveTileCount;

    public int earnCoinCount;

   
    private void Update()
    {
        if (startAdventure)
        {
            adventureTime += Time.deltaTime;

            if((int)adventureTime > 59)
            {
                adventureTime = 0;
                adventureMinutes++;
            }
        }
    }

    public void ResetScore()
    {
        adventureTime = 0f;
        adventureMinutes = 0;
        moveTileCount = 0;
        earnCoinCount = 0;
    }

    private static ScoreManager instance;
    public static ScoreManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
