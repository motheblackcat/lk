using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BobAxeEvent : MonoBehaviour {
    Canvas DialogUI;
    GameObject axe;
    List<GameObject> sWeapons;
    bool wasOpened = false;
    bool wasClosed = false;

    void Start() {
        DialogUI = GameObject.Find("DialogUI").GetComponent<Canvas>();
        axe = Resources.Load("Sweapons/Axe")as GameObject;
    }

    void Update() {
        // TODO: Make a general way to manage quests states
        sWeapons = GameObject.Find("Player").GetComponent<PlayerSWeapons>().sWeapons;
        if (DialogUI.enabled)wasOpened = true;
        wasClosed = wasOpened && !DialogUI.enabled;
        if (wasClosed) {
            if (sWeapons.Count == 0)sWeapons.Add(axe);
            GetComponent<Collider2D>().enabled = false;
            wasOpened = false;
            wasClosed = false;
        }
    }
}