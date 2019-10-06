using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStateManager : MonoBehaviour {
    GameObject player;
    GameObject playerUI;
    GameObject cmCam;
    void Start() {
        // Getting ref to gameobjects
        player = GameObject.Find("Player");
        playerUI = GameObject.Find("Player UI");

        // Setting the ones we will keep between scenes
        if (player) {
            GameObject.DontDestroyOnLoad(player);
            GameObject.Find("CMCamera").GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
        }
        if (playerUI)GameObject.DontDestroyOnLoad(playerUI);

        // TO DO: make a better way to set player start position (no dontdestroy?)
        if (SceneManager.GetActiveScene().buildIndex == 3 && player) {
            player.transform.position = new Vector2(-12, 0.2f);
        }
    }
}