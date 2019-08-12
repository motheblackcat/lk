using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour {
    GameObject lastselect;
    void Start() {
        lastselect = new GameObject();
    }
    void Update() {
        if (EventSystem.current.currentSelectedGameObject == null) {
            EventSystem.current.SetSelectedGameObject(lastselect);
        } else {
            lastselect = EventSystem.current.currentSelectedGameObject;
        }
    }
}