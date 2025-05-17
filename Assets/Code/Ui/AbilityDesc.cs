using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class AbilityDesc : MonoBehaviour
{

    public Image icon;
    public TMP_Text abilityName;
    public TMP_Text abilityDesc;


    public void showInfo(Ability ability)
    {
        icon.sprite = ability.image;
        abilityName.text = ability.abilityName;
        abilityDesc.text = ability.abilityDesc;

    }


}
