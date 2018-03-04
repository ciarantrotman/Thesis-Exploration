using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{

    public Animator animator;
    //public Motion nod;
    //public Motion shake;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
            animator.Play("Nod");

        if (Input.GetKeyDown(KeyCode.DownArrow))
            animator.Play("Shake");
    }

    public void Shake()
    {
        animator.Play("Nod");
        Debug.Log("Nod");
    }

    public void Nod()
    {
        animator.Play("Shake");
        Debug.Log("Shake");
    }
}   