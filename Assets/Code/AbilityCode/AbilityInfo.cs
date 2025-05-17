using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityInfo : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject infoObj;

    public float fadeDuration = 1.0f;

    public bool getThis;


    public bool infoShowing;

    public Ability nowAbility;

    public SpriteRenderer abilityImage;

    public GameObject abilityIcon;

    public void SetUp()
    {
        playerTransform = GameManager.Instance.player.transform;
        abilityImage = GetComponent<SpriteRenderer>();


        bool changed = false;
        while (!changed)
        {
            nowAbility = AbilityManager.Instance.abilities[Random.Range(0, AbilityManager.Instance.abilities.Count)];
            if (nowAbility.allow == false)
            {
                changed = true;
            }
        }
        abilityImage.sprite = nowAbility.image;

    }

    void Update()
    {

        if (!getThis)
        {
            if(transform.position == playerTransform.position)
            {
                getThis = true;

                GameObject ability = Instantiate(abilityIcon, UiManager.Instance.abilityList);
                ShowAbility showAbility = ability.GetComponent<ShowAbility>();
                showAbility.ability = nowAbility;
                nowAbility.Activate();

                Destroy(gameObject);

            }
        }

    }

    void OnTriggerEnter2D() 
    {

        UiManager.Instance.abilityInfo.showInfo(nowAbility);
        StartCoroutine(UiManager.Instance.abilityInfo.FadeIn());

    }

    void OnTriggerExit2D()
    {

        StartCoroutine(UiManager.Instance.abilityInfo.FadeOut());

    }

   

}
