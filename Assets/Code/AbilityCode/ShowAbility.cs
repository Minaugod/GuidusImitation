using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class ShowAbility : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private GameObject abilityPanel;

    public Ability ability;
    
    bool isClicking = false;

    Image icon;
    private void Start()
    {
        abilityPanel = UiManager.Instance.abilityDesc.gameObject;
        icon = GetComponent<Image>();
        icon.sprite = ability.image;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!abilityPanel.activeSelf && !isClicking)
        {
            isClicking = true;

            abilityPanel.SetActive(true);
            UiManager.Instance.abilityDesc.showInfo(ability);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (abilityPanel.activeSelf && isClicking)
        {
            isClicking = false;
            abilityPanel.SetActive(false);

        }
    }

}
