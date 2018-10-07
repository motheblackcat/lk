using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationControl : MonoBehaviour {
	Animator anim;

	// TODO: MAKE IT GENERIC
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	void Update () {
		if (GetComponent<EnemyHealthControl>().isDead) { anim.SetTrigger("die"); }
	}

	void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
			anim.PlayInFixedTime("Slime_Atk", 0, 1.0f);
		}		
	}
}
