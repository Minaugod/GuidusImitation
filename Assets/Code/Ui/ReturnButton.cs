using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class ReturnButton : MonoBehaviour
{
    
    public void OnReturnButton()
    {
        Invoke("ReturnToSpawn", 1f);
    }
    

    public void ReturnToSpawn()
    {
        GameManager.Instance.DataSave();
        SceneManager.LoadScene(0);
    }
}
