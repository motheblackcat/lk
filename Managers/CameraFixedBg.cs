using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFixedBg : MonoBehaviour {
    public GameObject[] fixedElements;
    void Awake() {
        foreach (GameObject element in fixedElements) {
            element.transform.parent = transform;
        }
    }
}
