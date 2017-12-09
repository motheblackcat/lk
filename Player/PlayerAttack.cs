using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

public bool isAttacking = false;
public float attackDuration = 0.1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Attack")) {
			StartCoroutine(Attack());
		}
	}

	IEnumerator Attack() {
		isAttacking = true;
		yield return new WaitForSeconds(attackDuration); // Can be switched with the sword attack duration
		isAttacking = false;
	}
}
