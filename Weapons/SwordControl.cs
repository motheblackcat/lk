using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordControl : MonoBehaviour {
	Animator animator;
	GameObject player;
	Animator playerAnim;
	string[] animParams;
	
	void Start () {
		animator = GetComponent<Animator>();
		player = GameObject.Find("Player");
		playerAnim = player.GetComponent<Animator>();
	}
	
	void Update () {
		Position();
		Animate();
	}

	void Position() {
		if (player.GetComponent<SpriteRenderer>().flipX) {
			GetComponent<SpriteRenderer>().flipX = true;
			transform.localPosition = new Vector2(-0.74f, transform.localPosition.y);
		} else {
			GetComponent<SpriteRenderer>().flipX = false;
			transform.localPosition = new Vector2(0.74f, transform.localPosition.y);
		}
	}

	void Animate() {
		// TODO: Fix sword animation lag
		string[] animParams = new string[animator.parameterCount];
		for (int i = 0; i < animator.parameterCount; i++) {
			animParams[i] = animator.GetParameter(i).name;
				if (playerAnim.GetBool(animParams[i])) {
					animator.SetBool(animParams[i], true);
				} else {
					animator.SetBool(animParams[i], false);
				}
		}
	}
}
