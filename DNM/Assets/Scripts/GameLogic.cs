using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {

    private PlayerController playerController;
    private bool pause = false;
    [SerializeField] private GameObject pauseCanvas;

	// Use this for initialization
	void Start () {
        playerController = FindObjectOfType<PlayerController>();
        pauseCanvas.SetActive(false);
        pause = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pause = !pause;
            playerController.pause = pause;
            if (pause) {
                pauseCanvas.SetActive(true);
            }
            else {
                pauseCanvas.SetActive(false);
            }
        }
	}
}
