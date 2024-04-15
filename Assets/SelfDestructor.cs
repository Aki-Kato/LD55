using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructor : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroyAfterReading());
    }

    IEnumerator DestroyAfterReading()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}
