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
        GameObject.DontDestroyOnLoad(player);
        GameObject.DontDestroyOnLoad(playerUI);

        GameObject.Find("CMCamera").GetComponent<CinemachineVirtualCamera>().Follow = player.transform;

        if (SceneManager.GetActiveScene().buildIndex == 3) {
            player.transform.position = new Vector2(-12, 0.2f);
        }
    }
}