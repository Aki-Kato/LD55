using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class EmployeeRoomController : MonoBehaviour
{
    public Animator _anim;

    public List<Transform> employeePositions;
    public List<GameObject> employees;
    public Transform spawnPosition;
    
    private string[] leaveAnimationTriggers = {/* "Leave", "ManyNods", */"LeaveWrongWay" };
    
    public void Summon()
    {
        _anim.SetTrigger("PickUp");
        ProceedInLine();
    }
    
    public void Approve()
    {
        _anim.SetTrigger("Approve");
        Leave();
    }

    private void ProceedInLine()
    {
        for (int i = 0; i < employees.Count; i++)
        {
            var anim = employees[i].GetComponent<Animator>();
            anim.SetBool("IsWalking", true);
            employees[i].transform.DORotate(employeePositions[i].rotation.eulerAngles, 0.5f);
            employees[i].transform.DOMove(employeePositions[i].position, 0.5f).OnComplete(()=>anim.SetBool("IsWalking", false));
        }
    }

    private void Leave()
    {
        employees[0].GetComponent<Animator>().SetTrigger(leaveAnimationTriggers[Random.Range(0, leaveAnimationTriggers.Length)]);
        StartCoroutine(MoveTheLine());
    }

    private IEnumerator MoveTheLine()
    {
        yield return new WaitForSeconds(2f);
        var firstEmployee = employees[0];
        firstEmployee.transform.position = spawnPosition.position;
        firstEmployee.GetComponent<Animator>().SetTrigger("Reset");
        employees.Remove(firstEmployee);
        employees.Add(firstEmployee);
    }
}
