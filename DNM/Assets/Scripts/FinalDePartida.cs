using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalDePartida : MonoBehaviour {
    public GameObject i1, i2, i3;
    private GameLogic gamelogic;
    public Text puntos, saltos, intentos;

    private void Start() {
        gamelogic = FindObjectOfType<GameLogic>();
        gameObject.SetActive(false);
    }

    private void OnEnable() {
        if (gamelogic.bigCoinGrabbed[0]) {
            i1.SetActive(true);
        }
        else {
            i1.SetActive(false);
        }

        if (gamelogic.bigCoinGrabbed[1]) {
            i2.SetActive(true);
        }
        else {
            i2.SetActive(false);
        }

        if (gamelogic.bigCoinGrabbed[2]) {
            i3.SetActive(true);
        }
        else {
            i3.SetActive(false);
        }

        puntos.text = "Puntos: " + gamelogic.pointCounter;
        saltos.text = "Saltos: " + gamelogic.saltosCounter;
        intentos.text = "Intentos: " + gamelogic.intentosCounter;
    }
}
