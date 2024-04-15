using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeRoomController : MonoBehaviour
{
    public Animator _anim;

    public void Summmon()
    {
        _anim.SetTrigger("PickUp");
    }
    
    public void Approve()
    {
        _anim.SetTrigger("Approve");
    }
}
