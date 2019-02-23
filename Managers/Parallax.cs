using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {
	public Transform[] backgrounds;
	float[] paraScales;
	Transform cam;
	Vector3 prevCamPos;
	public float smoothing = 1f;

	void Start() {
		cam = Camera.main.transform;
		prevCamPos = cam.position;
		paraScales = new float[backgrounds.Length];
		for (int i = 0; i < backgrounds.Length; i++) {
			paraScales[i] = backgrounds[i].position.z * -1;
		}
	}

	void Update() {
		for (int i = 0; i < backgrounds.Length; i++) {
			float parallax = (prevCamPos.x - cam.position.x) * paraScales[i];
			float bgTargetPosX = backgrounds[i].position.x + parallax;
			Vector3 bgTargetPos = new Vector3 (bgTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
			backgrounds[i].position = Vector3.Lerp (backgrounds[i].position, bgTargetPos, smoothing * Time.deltaTime);
		}
		prevCamPos = cam.position;		
	}
}
