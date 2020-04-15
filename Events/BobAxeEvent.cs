using System.Collections.Generic;
using UnityEngine;

public class BobAxeEvent : MonoBehaviour {
    Canvas DialogUI;
    GameObject axe;
    List<GameObject> sWeapons;
    enum QuestState { DIALOG_IDLE, DIALOG_STARTED }
    QuestState questState;

    void Start() {
        DialogUI = GameObject.Find("DialogUI").GetComponent<Canvas>();
        axe = Resources.Load("Sweapons/Axe") as GameObject;
        questState = QuestState.DIALOG_IDLE;
    }

    void Update() {
        sWeapons = GameObject.Find("Player").GetComponent<PlayerSWeapons>().sWeapons;
        if (DialogUI.enabled) questState = QuestState.DIALOG_STARTED;
        if (questState == QuestState.DIALOG_STARTED && !DialogUI.enabled) {
            if (sWeapons.Count == 0) sWeapons.Add(axe);
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject);
        };
    }
}