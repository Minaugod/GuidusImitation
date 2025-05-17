using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AbilityInfoDisplay : MonoBehaviour
{


    public TMP_Text abilityName;
    public TMP_Text abilityDesc;


    public void showInfo(Ability ability)
    {

        abilityName.text = ability.abilityName;
        abilityDesc.text = ability.abilityDesc;

    } 

    public IEnumerator FadeIn()
    {
        gameObject.SetActive(true);
        float fadeCount = 0;
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.02f;

            abilityName.color = new Color(255, 255, 255, fadeCount);
            abilityDesc.color = new Color(255, 255, 255, fadeCount);

            yield return new WaitForSeconds(0.01f);

        }

    }
    public IEnumerator FadeOut()
    {
        float fadeCount = 1;
        while (fadeCount == 0f)
        {
            fadeCount -= 0.02f;

            abilityName.color = new Color(255, 255, 255, fadeCount);
            abilityDesc.color = new Color(255, 255, 255, fadeCount);

            yield return new WaitForSeconds(0.01f);

        }
        gameObject.SetActive(false);


    }

}
