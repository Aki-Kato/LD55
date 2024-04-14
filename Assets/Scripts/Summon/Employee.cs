using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Employee
{
    public string employeeName;
    public float speed;
    public GameObject model;

    public enum Perk{
        None,
    }

    public Perk perk1 = Perk.None;
    public Perk perk2 = Perk.None;

    public void SelectPerks(){
        int _numberOfPerks = Random.Range(0,3);
        switch (_numberOfPerks){
            case 0:
            break;

            case 1:
            break;
            
            case 2:
            break;
        }
    }
}
