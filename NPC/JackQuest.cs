﻿using UnityEngine;

public class JackQuest : MonoBehaviour {
    [SerializeField] Quest quest;
    [SerializeField] DialogManager dialogManager;
    [SerializeField] GameObject sWeapon;

    void Start() {
        quest.name = "AxeEvent";
        quest.npcName = gameObject.name;
        Quest savedQuest = PlayerState.Instance.quests.Find(q => q.name == quest.name);
        if (savedQuest != null) {
            quest.isActive = savedQuest.isActive;
            quest.isComplete = savedQuest.isComplete;
        }
    }

    void Update() {
        if (!quest.isComplete) {
            if (dialogManager.npc == gameObject && dialogManager.inDialog) {
                quest.isActive = true;
                PlayerState.Instance.quests.Add(quest);
            }
            if (quest.isActive && !dialogManager.inDialog) {
                quest.isComplete = true;
                GameObject.Find("Player").GetComponent<PlayerSWeapons>().sWeapons.Add(sWeapon);
                PlayerState.Instance.Save();
            }
        } else {
            /** TODOL: Make global way to affect dialogs from Quests */
            GetComponent<NpcManager>().dialog.sentences[0] = "Go on then!";
        }
    }
}