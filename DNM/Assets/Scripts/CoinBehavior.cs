using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : InteractableItem {
    private GameLogic gamelogic;

	// Use this for initialization
	void Start () {
        gamelogic = FindObjectOfType<GameLogic>();
	}
	
	// Update is called once per frame
	void Update () {
        AddToGamelogic(gamelogic, this);
	}

    public override void Restart() {
        gameObject.SetActive(true);
    }
}
