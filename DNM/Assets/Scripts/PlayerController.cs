using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rb;
    private float distToGround;
    private Collider2D collider;
    [SerializeField] private float jumpForce = 5, speedUpForce = 5, speedX = 50;
    [SerializeField] private GameObject spawnPoint;
    private float lateralDist;
    [HideInInspector] public bool pause = false; //controla el pausado
    private GameLogic gamelogic;

    [Header("Points")]
    [SerializeField] private int coinPoints = 20;
    [SerializeField] private int boxPoints = 40;
    [SerializeField] private int enemyPoints = 60;
    [SerializeField] private int bigCoinPoints = 150;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        distToGround = collider.bounds.extents.y + 0.1f;
        lateralDist = collider.bounds.extents.x;
        transform.position = spawnPoint.transform.position;
        gamelogic = FindObjectOfType<GameLogic>();
	}
	
	// Update is called once per frame
	void Update () {
        if (pause) {

        }
        else {
            //Movimiento horizontal
            Vector3 pos = transform.position;
            pos.x += speedX * Time.deltaTime;
            transform.position = pos;

            //Control de gravedad en el salto.
            if (rb.velocity.y < 0) {
                rb.gravityScale = 10;
            }
            else {
                rb.gravityScale = 4;
            }

            //Control de aterrizaje a alta velocidad
            if(IsGrounded() && rb.velocity.y < 0) {
                rb.velocity = Vector3.zero;
            }

            //Salto
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) {
                Jump(jumpForce);
            }

            //Comprovacion de colision frontal
            if (IsCollidingFront() && transform.position.y > -2) {
                print("collided front");
                gamelogic.Restart();
                Restart();
            }
        }
	}

    private void Jump(float f) {
        Vector2 v = rb.velocity;
        v.y = 0;
        rb.velocity = v;
        rb.AddForce(Vector3.up * f, ForceMode2D.Impulse);
    }

    private bool IsGrounded() {
        return (Physics2D.Raycast(transform.position - new Vector3(lateralDist,0,0), -Vector3.up, distToGround) || Physics2D.Raycast(transform.position - new Vector3(lateralDist,0,0), -Vector3.up, distToGround));
    }

    private bool IsCollidingFront() {
        return Physics2D.Raycast(transform.position, Vector2.right, distToGround - 0.2f);
    }

    private void Restart() {
        transform.position = spawnPoint.transform.position;
        rb.velocity = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D c) {
        if (c.tag.Equals("Kill")) {
            print("trigger kill");
            gamelogic.Restart();
            Restart();
        }
        else if (c.tag.Equals("Jump")) {
            Jump(speedUpForce);
        }
        else if (c.tag.Equals("Coin")) {
            gamelogic.pointCounter += coinPoints;
            c.gameObject.SetActive(false);
        }
        else if (c.tag.Equals("Big_Coin")) {
            gamelogic.bigCoinGrabbed[c.gameObject.GetComponent<BigCoin>().bigCoinID] = true;
            gamelogic.pointCounter += bigCoinPoints;
            c.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D c) {
        if (c.gameObject.tag.Equals("Box")) {
            c.gameObject.GetComponent<BoxAnimation>().play = true;
            gamelogic.pointCounter += boxPoints;
        }
        else if (c.gameObject.tag.Equals("Enemy")) {
            Jump(jumpForce);
            c.gameObject.GetComponent<Enemy>().currentState = Enemy.STATE.DIE;
            gamelogic.pointCounter += enemyPoints;
        }
    }
}
