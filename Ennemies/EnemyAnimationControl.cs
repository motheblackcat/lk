using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationControl : MonoBehaviour {
	Animator anim;

	// TODO: MAKE IT GENERIC (care for states names and in Attack)
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	void Update () {
		anim.SetBool("hurt", GetComponent<EnemyHealthControl>().tookDamage ? true : false);
		if (GetComponent<EnemyHealthControl>().isDead) { anim.SetTrigger("die"); }
	}

	void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
			anim.PlayInFixedTime("Slime_Atk", 0, 1.0f);
		}		
	}
}
