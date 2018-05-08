using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCoin : InteractableItem {
    private GameLogic gamelogic;
    public int bigCoinID;

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
