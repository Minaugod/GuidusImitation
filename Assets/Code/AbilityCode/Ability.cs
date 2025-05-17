using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{

    public Sprite image;

    public string abilityName;

    public string abilityDesc;

    public bool allow = false;

    public int allowCount;
    public abstract void Activate();
    public abstract void Deactivate();

}
