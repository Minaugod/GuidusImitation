using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum StatusType { Atk, AtkSpd, Copperkey, Coin, Hp, Shield }
public class Status : MonoBehaviour
{


    public StatusType type;

    Slider slider;
    TMPro.TMP_Text text;
    private void Awake()
    {
        text = GetComponentInChildren<TMPro.TMP_Text>();
        slider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case StatusType.Atk:
                text.text = string.Format("{0:F0}", GameManager.Instance.playerAtk);
                break;
            case StatusType.AtkSpd:
                text.text = string.Format("{0:F2}", GameManager.Instance.playerAtkSpd);
                break;
            case StatusType.Copperkey:
                text.text = string.Format("{0:F0}", GameManager.Instance.playerCopperkey);
                break;
            case StatusType.Coin:
                text.text = string.Format("{0:F0}", GameManager.Instance.playerCoin);
                break;
            case StatusType.Hp:
                int currentHp = GameManager.Instance.playerHp;
                int maxHp = GameManager.Instance.playerMaxHp;
                text.text = string.Format("{0:F0}/{1:F0}", GameManager.Instance.playerHp, GameManager.Instance.playerMaxHp);

                slider.maxValue = maxHp;
                slider.value = currentHp;

                break;
            case StatusType.Shield:
                text.text = string.Format("{0:F0}", GameManager.Instance.playerShield);
                if(GameManager.Instance.playerShield <= 0)
                {
                    slider.value = 0;
                }
                else
                {
                    slider.value = 1;
                }
                
                break;
        }
    }

}
