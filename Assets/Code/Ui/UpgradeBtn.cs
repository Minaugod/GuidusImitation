using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class UpgradeBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public StatusType type;

    private Image buttonImage;
    private Color originalColor;
    public Color clickColor;

    int price;
    TMP_Text priceText;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        originalColor = buttonImage.color;
        priceText = GetComponentInChildren<TMP_Text>();

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(GameManager.Instance.playerCoin >= price)
        {
            buttonImage.color = clickColor;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
        if (GameManager.Instance.playerCoin >= price)
        {
            buttonImage.color = originalColor;
            switch (type)
            {
                case StatusType.Atk:
                    GameManager.Instance.playerAtk -= GameManager.Instance.atkLv - 1;
                    GameManager.Instance.atkLv++;
                    GameManager.Instance.playerAtk += GameManager.Instance.atkLv - 1;
                    break;
                case StatusType.AtkSpd:
                    GameManager.Instance.playerAtkSpd -= (GameManager.Instance.atkSpdLv - 1) * 0.15f;
                    GameManager.Instance.atkSpdLv++;
                    GameManager.Instance.playerAtkSpd += (GameManager.Instance.atkSpdLv - 1) * 0.15f;
                    break;
                case StatusType.Hp:
                    GameManager.Instance.playerMaxHp -= (GameManager.Instance.maxHpLv - 1) * 2;
                    GameManager.Instance.maxHpLv++;
                    GameManager.Instance.playerMaxHp += (GameManager.Instance.maxHpLv - 1) * 2;
                    GameManager.Instance.UpdateHp(2);
                    break;
                case StatusType.Shield:
                    GameManager.Instance.playerShield -= (GameManager.Instance.shieldBonusLv - 1) * 2;
                    GameManager.Instance.shieldBonusLv++;
                    GameManager.Instance.playerShield += (GameManager.Instance.shieldBonusLv - 1) * 2;
                    break;
            }

            GameManager.Instance.playerCoin -= price;
            GameManager.Instance.DataSave();
        }

    }

    private void Update()
    {

        if(GameManager.Instance.playerCoin < price)
        {
            priceText.color = Color.red;
        }
        else
        {
            priceText.color = Color.black;
        }

        switch (type)
        {
            case StatusType.Atk:
                price = GameManager.Instance.atkLv * 20;
                priceText.text = price.ToString();
                break;

            case StatusType.AtkSpd:
                price = GameManager.Instance.atkSpdLv * 20;
                priceText.text = price.ToString();
                break;

            case StatusType.Hp:
                price = GameManager.Instance.maxHpLv * 10;
                priceText.text = price.ToString();
                break;

            case StatusType.Shield:
                price = GameManager.Instance.shieldBonusLv * 10;
                priceText.text = price.ToString();
                break;

        }

    }

}
