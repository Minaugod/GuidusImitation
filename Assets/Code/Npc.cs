using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    private enum NpcType { Shop }

    [SerializeField]
    private NpcType type;

    public void DisplayPopup()
    {

        switch (type)
        {
            case NpcType.Shop:
                UiManager.Instance.shopPanel.SetActive(true);
                break;
        }
    }
}
