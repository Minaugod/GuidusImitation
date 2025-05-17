using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UiManager : MonoBehaviour
{
    public AbilityDesc abilityDesc;

    public Transform abilityList;

    public AbilityInfoDisplay abilityInfo;

    public FadeController fadeController;

    public Result uiResult;

    public GameObject shopPanel;
    public void FadeScreen()
    {
        StartCoroutine(fadeController.FadeIn());
    }

    public void DisplayStageName(string floor)
    {
        StartCoroutine(fadeController.StageName(floor));
    }

    public void GameWin()
    {
        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        uiResult.SetResult();
    }

    public void GameOver()
    {
        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        uiResult.SetResult();
    }


    private static UiManager instance;
    public static UiManager Instance
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
