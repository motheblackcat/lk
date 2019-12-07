using System.Collections.Generic;
using UnityEngine;

public class CameraFixedChildren : MonoBehaviour {
    public List<GameObject> fixedElements;
    void Awake() {
        foreach (GameObject go in fixedElements) {
            go.transform.parent = transform;
        }
    }
}