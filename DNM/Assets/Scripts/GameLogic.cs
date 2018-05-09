using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

    private PlayerController playerController;
    private bool pause = false;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private Text puntos, intentos;
    [HideInInspector]public int pointCounter;
    public bool[] bigCoinGrabbed = { false, false, false };
    private float counter;
    public int intentosCounter;
    [HideInInspector] public int saltosCounter;
    [HideInInspector] public List<InteractableItem> interactableElements;
    public GameObject canvasFinal;

	// Use this for initialization
	void Start () {
        playerController = FindObjectOfType<PlayerController>();
        pauseCanvas.SetActive(false);
        pause = false;
        pointCounter = 0;
        puntos.text = "Puntos: "+pointCounter;
        counter = 0.0f;
        bigCoinGrabbed = new bool[3] { false, false, false };
        intentosCounter = 1;
        intentos.text = "Intentos: " + intentosCounter;
        saltosCounter = 0;
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

        if (pause) {
            Time.timeScale = 0.0f;
        }
        else {
            Time.timeScale = 1.0f;
        }

        if(counter > 0.3f) {
            pointCounter++;
            counter = 0;
        }

        //control de final de partida
        if(playerController.speedX == 0) {
            pause = true;
            canvasFinal.SetActive(true);
        }

        counter += Time.deltaTime;
	}

    public void Restart() {
        pointCounter = 0;
        saltosCounter = 0;
        intentosCounter++;
        intentos.text = "Intentos: " + intentosCounter;
        bigCoinGrabbed = new bool[3] { false, false, false };
        playerController.Restart();
        foreach(InteractableItem i in interactableElements) {
            i.Restart();
        }
    }

    public void LoadScene(int id) {
        SceneManager.LoadScene(id);
    }
}
