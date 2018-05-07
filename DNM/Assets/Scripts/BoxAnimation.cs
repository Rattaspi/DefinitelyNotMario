using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxAnimation : InteractableItem {
    [SerializeField] private float coinSpeed = 2, maxCoinHeight = 1;
    [HideInInspector] public bool play = false;
    private Transform coinTransform;
    private GameLogic gamelogic;

	// Use this for initialization
	void Start () {
        play = false;
        coinTransform = gameObject.transform.GetChild(0);
        gamelogic = FindObjectOfType<GameLogic>();
	}
	
	// Update is called once per frame
	void Update () {
        AddToGamelogic(gamelogic,this);
        if (play) {
            coinTransform.localPosition = new Vector3(coinTransform.localPosition.x, coinTransform.localPosition.y + coinSpeed * Time.deltaTime, coinTransform.localPosition.z);
            if(coinTransform.localPosition.y > maxCoinHeight) {
                gameObject.SetActive(false);
            }
        }
	}

    public override void Restart() {
        coinTransform.localPosition = new Vector3(0, 0, 0);
        play = false;
        gameObject.SetActive(true);
    }
}
