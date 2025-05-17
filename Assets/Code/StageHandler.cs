using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageHandler : MonoBehaviour
{

    private enum StageType { Normal, Special }

    [SerializeField]
    private StageType type;

    private void Start()
    {
        switch (type)
        {
            case StageType.Normal:
                GameManager.Instance.normalStage = this;
                break;
            case StageType.Special:
                GameManager.Instance.specialStage = this;
                break;
        }
        
    }


    public GameObject CreateStage(GameObject stage)
    {
        GameObject StageN = Instantiate(stage, transform);
        return StageN;
    }



}
