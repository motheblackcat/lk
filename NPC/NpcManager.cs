﻿using UnityEngine;

public class NpcManager : MonoBehaviour {
	public DialogClass dialog;
	public bool autoStart = false;
	Animator animator;
	DialogManager dialogManager;

	void Start() {
		animator = GetComponent<Animator>();
		dialogManager = GameObject.Find("PlayerUI").GetComponent<DialogManager>();
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			if (animator) {
				animator.SetBool("watch", true);
				animator.SetBool("talk", dialogManager.inDialog);
			}
			if (tag == "NPC") {
				GetComponent<SpriteRenderer>().flipX = other.gameObject.transform.position.x > transform.position.x;
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			if (animator) {
				animator.SetBool("watch", false);
				animator.SetBool("talk", false);
			}
		}
	}
}