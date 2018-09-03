using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityAnimation : MonoBehaviour {

    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Expand()
    {
        animator.Play("Expansion");
    }

    public void Contract()    
    {
        animator.Play("Contraction");
    }
}   
