using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UpgradeValue : MonoBehaviour
{

    public StatusType type;
    TMP_Text currentValue;
    TMP_Text nextValue;

    int upgradeLv;
    private void Awake()
    {
        currentValue = GetComponentsInChildren<TMP_Text>()[0];
        nextValue = GetComponentsInChildren<TMP_Text>()[1];
    }

    private void Update()
    {
        switch (type)
        {
            case StatusType.Atk:
                upgradeLv = GameManager.Instance.atkLv;
                currentValue.text = string.Format("현재 증가량 : {0:F0}", (upgradeLv - 1) * 1);
                nextValue.text = string.Format("다음 증가량 : {0:F0}", upgradeLv * 1);
                break;

            case StatusType.AtkSpd:
                upgradeLv = GameManager.Instance.atkSpdLv;
                currentValue.text = string.Format("현재 증가량 : {0:F2}", (upgradeLv - 1) * 0.15);
                nextValue.text = string.Format("다음 증가량 : {0:F2}", upgradeLv * 0.15);
                break;

            case StatusType.Hp:
                upgradeLv = GameManager.Instance.maxHpLv;
                currentValue.text = string.Format("현재 증가량 : {0:F0}", (upgradeLv - 1) * 2);
                nextValue.text = string.Format("다음 증가량 : {0:F0}", upgradeLv * 2);
                break;

            case StatusType.Shield:
                upgradeLv = GameManager.Instance.shieldBonusLv;
                currentValue.text = string.Format("현재 증가량 : {0:F0}", (upgradeLv - 1) * 2);
                nextValue.text = string.Format("다음 증가량 : {0:F0}", upgradeLv * 2);
                break;

        }
    }


}
