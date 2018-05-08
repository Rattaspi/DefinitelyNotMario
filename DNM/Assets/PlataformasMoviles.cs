using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformasMoviles : InteractableItem {
    [SerializeField] private GameObject arriba, abajo;
    [SerializeField] private float speed = 2;
    private Vector3 initialPosArriba, initialPosAbajo;
    private GameLogic gamelogic;
    public enum PLATSTATE {IDLE, MOVE };
    [HideInInspector] public PLATSTATE currentState;

	// Use this for initialization
	void Start () {
        gamelogic = FindObjectOfType<GameLogic>();
        initialPosArriba = arriba.transform.position;
        initialPosAbajo = abajo.transform.position;
        currentState = PLATSTATE.IDLE;

    }
	
	// Update is called once per frame
	void Update () {
        AddToGamelogic(gamelogic, this);
        if (currentState == PLATSTATE.MOVE) {
            if(abajo.transform.position.y > initialPosAbajo.y - 0.5f) {
                abajo.transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
            }
            if(arriba.transform.position.y < initialPosArriba.y + 0.5f) {
                arriba.transform.position += new Vector3(0, speed * Time.deltaTime, 0);
            }
        }
	}

    public override void Restart() {
        arriba.transform.position = initialPosArriba;
        abajo.transform.position = initialPosAbajo;
        currentState = PLATSTATE.IDLE;
    }
}
