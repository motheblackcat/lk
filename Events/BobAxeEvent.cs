using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BobAxeEvent : MonoBehaviour {
    Image dialogBox;
    GameObject axe;
    List<GameObject> sWeapons;
    bool wasOpened = false;
    bool wasClosed = false;

    void Start() {
        dialogBox = GameObject.Find("DialogBox").GetComponent<Image>();
        axe = Resources.Load("Sweapons/Axe")as GameObject;
    }

    void Update() {
        // TODO: Make a general way to manage quests states
        sWeapons = GameObject.Find("Player").GetComponent<PlayerSWeapons>().sWeapons;
        if (dialogBox.enabled)wasOpened = true;
        wasClosed = wasOpened && !dialogBox.enabled;
        if (wasClosed) {
            if (sWeapons.Count == 0)sWeapons.Add(axe);
            GetComponent<Collider2D>().enabled = false;
            wasOpened = false;
            wasClosed = false;
        }
    }
}