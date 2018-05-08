using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : InteractableItem {
    private GameLogic gamelogic;
    private Vector3 initialPos;
    public enum STATE {IDLE, PLAY, DIE};
    [HideInInspector]public STATE currentState;
    [SerializeField]private float speed = 5;
    private SpriteRenderer sprite;
    [SerializeField]private float lossAlphaSpeed = 10;
    private Vector4 initialColor;

	// Use this for initialization
	void Start () {
        gamelogic = FindObjectOfType<GameLogic>();
        initialPos = transform.position;
        currentState = STATE.IDLE;
        sprite = GetComponent<SpriteRenderer>();
        initialColor = sprite.color;
	}
	
	// Update is called once per frame
	void Update () {
        AddToGamelogic(gamelogic, this);
        if (currentState == STATE.PLAY) {
            transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        }
        else if (currentState == STATE.DIE) {
            if(sprite.color.a > 0) {
                sprite.color = new Vector4(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - lossAlphaSpeed*Time.deltaTime);
            }
            else {
                gameObject.SetActive(false);
            }
            if(transform.rotation.z < 90) {
                transform.Rotate(new Vector3(0, 0, 270 * Time.deltaTime));
            }
            if(transform.position.y > -1.5) {
                transform.position += new Vector3(0, -2*Time.deltaTime, 0);
            }
        }
	}

    public override void Restart() {
        gameObject.SetActive(true);
        sprite.color = initialColor;
        transform.position = initialPos;
        transform.rotation = Quaternion.identity;
        currentState = STATE.IDLE;
    }
}
