using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Perk", menuName = "ScriptableObjects/Perks")]
public class PerkBase : ScriptableObject
{
    public string perkName;
    //Agile/Corpulent
    public float speedModifier;
    //Talented
    public int workUnitModifier;
    public bool isTrader;
    public float horseSpeedModifier;
    public bool isWillUseHorse;
    public bool isWillUseCatapult;
    public bool isGrumpy;
    public bool isBrute;
    public bool isDubious;
}
