using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

    private PlayerController playerController;
    private bool pause = false;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private Text puntos;
    public int pointCounter;
    private float counter;
    [HideInInspector] public List<InteractableItem> interactableElements;

	// Use this for initialization
	void Start () {
        playerController = FindObjectOfType<PlayerController>();
        pauseCanvas.SetActive(false);
        pause = false;
        pointCounter = 0;
        puntos.text = "Puntos: "+pointCounter;
        counter = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        puntos.text = "Puntos: " + pointCounter;
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

        if(counter > 0.3f) {
            pointCounter++;
            counter = 0;
        }

        counter += Time.deltaTime;
	}

    public void Restart() {
        foreach(InteractableItem i in interactableElements) {
            i.Restart();
        }
    }
}
