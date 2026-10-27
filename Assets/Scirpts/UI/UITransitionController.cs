using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UITransitionController : MonoBehaviour
{
    private Animator animator;

    public bool TransitionInEnd { get; private set; } = false;
    public bool TransitionOutEnd { get; private set; } = false;
   
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void ClearTriggers()
    {
        gameObject.SetActive(true);

        animator.ResetTrigger("In");
        animator.ResetTrigger("Out");
    }

    public void PlayTransitionIn()
    {
        ClearTriggers();
        TransitionInEnd = false;
        animator.SetTrigger("In");
    }
    public void PlayTransitionOut()
    {
        ClearTriggers();
        TransitionOutEnd = false;
        animator.SetTrigger("Out");
    }


    public void OnInEnd()
    {
        TransitionInEnd = true;
    }
    public void OnOutEnd()
    {
        TransitionOutEnd = true;
    }
}
