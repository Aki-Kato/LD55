using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PerkList", menuName = "ScriptableObjects/PerkList")]
public class PerksList : ScriptableObject
{
    public List<PerkBase> listOfPerks;

}
