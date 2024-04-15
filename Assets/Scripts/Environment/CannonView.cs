using System.Collections;
using System.Collections.Generic;
using Navigation.Views;
using UnityEngine;
using UnityEngine.Events;

public class CannonView : MonoBehaviour
{
    [SerializeField] private GameObject cannonModel;
    public void Start(){
        cannonModel.SetActive(false);
    }

    public void SetCannonState(bool _state){
        cannonModel.SetActive(_state);
    }
}
