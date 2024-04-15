using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRandomizer : MonoBehaviour
{
    private Animator _anim;

    public float minMultiplier, maxMultiplier;
    
    void Start()
    {
        _anim = GetComponent<Animator>();
        RandomizeAnimation();
    }

    private void RandomizeAnimation()
    {
        if (_anim is null)
        {
            Debug.Log("No Animator component attached");
            return;
        }
        
        _anim.SetFloat("SpeedModifier", Random.Range(minMultiplier, maxMultiplier));
        _anim.SetFloat("OffsetModifier", Random.Range(0f, 1f));
    }
}
