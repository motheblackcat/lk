using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationControl : MonoBehaviour {
	Animator anim;

	void Start () {
		anim = GetComponent<Animator>();
	}
	
	void Update () {
		if (GetComponent<EnemyHealthControl>().hasTakenDamage) {
			anim.SetBool("hurt", true);
		} else {
			anim.SetBool("hurt", false);			
		}

		if (GetComponent<EnemyHealthControl>().isDead) {
			anim.SetTrigger("die");
		}
	}
}
