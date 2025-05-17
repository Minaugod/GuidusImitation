using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{

    public List<Ability> abilities;

    void Start()
    {
        abilities = new List<Ability>();

        Ability[] allAbilities = FindObjectsOfType<Ability>();
        abilities.AddRange(allAbilities);

    }

    public void ResetAbility()
    {
        for(int i = 0; i < abilities.Count; i++)
        {
            abilities[i].Deactivate();
            abilities[i].allowCount = 0;
        }

        Transform[] activeAbilities = UiManager.Instance.abilityList.GetComponentsInChildren<Transform>();
        for(int i = 1; i < activeAbilities.Length; i++)
        {
            if(activeAbilities[i] != transform)
            {
                Destroy(activeAbilities[i].gameObject);
            }
        }
    }
    
    private static AbilityManager instance;
    public static AbilityManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
