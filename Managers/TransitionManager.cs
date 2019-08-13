using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour {
    Animator animator;
    public float transitionTimer = 1.0f;
    void Start() {
        animator = GetComponent<Animator>();
    }

    void OpenScene() {
        animator.SetTrigger("start");
    }

    void EndScene() {
        animator.SetTrigger("end");
    }
}