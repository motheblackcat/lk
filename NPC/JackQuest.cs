using UnityEngine;

public class JackQuest : MonoBehaviour {
	[SerializeField] GameObject sWeapon = null;
	public Quest quest;
	DialogManager dialogManager = null;

	void Start() {
		dialogManager = GameObject.FindObjectOfType<DialogManager>();
		quest.name = "Jack's Quest";
		quest.npcName = gameObject.name;
		Quest savedQuest = PlayerState.Instance.quests.Find(q => q.name == quest.name);
		if (savedQuest != null) {
			quest.isActive = savedQuest.isActive;
			quest.isComplete = savedQuest.isComplete;
		}
	}

	void Update() {
		if (!quest.isComplete) {
			if (dialogManager.npc == gameObject && dialogManager.inDialog && !quest.isActive) {
				quest.isActive = true;
				PlayerState.Instance.quests.Add(quest);
			}
			if (quest.isActive && !dialogManager.inDialog) {
				quest.isComplete = true;
				GameObject.Find("Player").GetComponent<PlayerSWeapons>().sWeapons.Add(sWeapon);
				PlayerState.Instance.Save();
			}
		} else {
			GetComponent<NpcManager>().dialog.sentences[0] = "Go on then!";
		}
	}
}