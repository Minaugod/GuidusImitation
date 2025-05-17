using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{


    public void OnPauseButton()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }

    public void OnResumeButton()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void OnReturnButton()
    {
        Time.timeScale = 1;
        GameManager.Instance.playerHp = 0;
        gameObject.SetActive(false);
    }

    public void OnExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        GameManager.Instance.DataSave();
        Application.Quit();
#endif
    }

}
