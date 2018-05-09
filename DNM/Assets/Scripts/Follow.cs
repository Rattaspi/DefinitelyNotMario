using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : InteractableItem {
    [SerializeField] private GameObject target;
    [SerializeField] private float offsetX = 0, offsetY = 0;
    [HideInInspector] public  float extraX = 0, extraY = 0; //estos offsets los controlan los triggers que varian el offset de la camara.
    private float interpolatedOffset;
    private GameLogic gamelogic;

	// Use this for initialization
	void Start () {
        gamelogic = FindObjectOfType<GameLogic>();
        transform.position = target.transform.position;
        Vector3 pos = transform.position;
        pos.x += (offsetX + extraX);
        pos.y = offsetY;
        pos.z = -10;
        transform.position = pos;
	}
	
	// Update is called once per frame
	void Update () {
        AddToGamelogic(gamelogic, this);
        interpolatedOffset = Mathf.Lerp(transform.position.y, offsetY + extraY, 0.2f);
        if (Mathf.Abs(transform.position.y - (offsetY+extraY)) < 0.2f) {
            interpolatedOffset = extraY;
        }
        transform.position = new Vector3(target.transform.position.x + offsetX + extraX, offsetY + interpolatedOffset, -10);
	}

    public override void Restart() {
        extraY = 0;
    }
}
