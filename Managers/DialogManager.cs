using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {
    public GameObject npc;
    Canvas dialogUI;
    PlayerControl playerControl;
    PlayerInputActions playerInputs;
    Queue<string> sentences;
    public bool inDialog = false;

    void Awake() {
        playerInputs = new PlayerInputActions();
        playerInputs.Player.Jump.performed += ctx => TriggerDialog();
    }

    void Start() {
        playerControl = FindObjectOfType<PlayerControl>();
        dialogUI = GameObject.Find("DialogUI").GetComponent<Canvas>();
        sentences = new Queue<string>();
    }

    void Update() {
        npc = playerControl.npc;
        inDialog = dialogUI.enabled;
        bool isGamepad = PlayerState.Instance.isGamepad;

        if (npc && playerControl.isGrounded) {
            bool autoStartDialog = npc.GetComponent<NpcManager>().autoStart;
            if (!autoStartDialog) {
                SpriteRenderer[] buttons = npc.GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer button in buttons)
                    if (button.name != npc.name && button.tag == "NPCButton") button.enabled = button.name == (PlayerState.Instance.isGamepad ? "ButtonA" : "SpaceBar");
            } else TriggerDialog();
        }

        if (!npc || inDialog) {
            GameObject[] buttons = GameObject.FindGameObjectsWithTag("NPCButton");
            foreach (GameObject button in buttons) button.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void TriggerDialog() {
        if (npc) {
            if (!inDialog) StartDialog(npc.GetComponent<NpcManager>());
            else DisplayNextDialog();
        }
    }

    void StartDialog(NpcManager npcManager) {
        sentences.Clear();
        dialogUI.enabled = true;
        npcManager.autoStart = false;
        GameObject.Find("DialogName").GetComponent<Text>().text = npc.name + ":";

        foreach (string sentence in npcManager.dialog.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextDialog();
    }

    void DisplayNextDialog() {
        if (sentences.Count == 0) {
            dialogUI.enabled = false;
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(DisplayLetters(sentence));
    }

    IEnumerator DisplayLetters(string sentence) {
        Text dialogText = GameObject.Find("DialogText").GetComponent<Text>();
        dialogText.text = "";

        foreach (char letter in sentence.ToCharArray()) {
            dialogText.text += letter;
            yield return null;
        }
    }

    void OnEnable() {
        playerInputs.Enable();
    }

    void OnDisable() {
        playerInputs.Disable();
    }
}