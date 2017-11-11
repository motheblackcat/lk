using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {
	void FixedUpdate () {
		transform.position = new Vector3(GameObject.Find("Player").transform.position.x, 0, -15);
	}
}
