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
        playerUI = GameObject.Find("PlayerUI");

        // Setting the ones we will keep between scenes
        GameObject.DontDestroyOnLoad(player);
        GameObject.DontDestroyOnLoad(playerUI);
    }
}