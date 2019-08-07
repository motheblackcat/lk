using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSlimes : MonoBehaviour {
	public GameObject enemy;
	public Transform pos;

	void Update() {
		if (Input.GetKeyDown("m")) { Instantiate(enemy, pos.position, pos.rotation); }
	}
}