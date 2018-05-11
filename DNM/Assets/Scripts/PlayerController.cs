using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rb;
    private float distToGround;
    private Collider2D collider;
    public float jumpForce = 5, speedUpForce = 5, speedX = 50;
    [SerializeField] private GameObject spawnPoint;
    private float lateralDist;
    [HideInInspector] public bool pause = false; //controla el pausado
    private GameLogic gamelogic;
    private bool jumping; //controla la orden de saltar
    private float jumpTimer, initialSpeed;
    private bool final; //controla el final de la partida
    private SoundManager sm;

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
        jumping = false;
        jumpTimer = 0.0f;
        final = false;
        initialSpeed = speedX;
        sm = FindObjectOfType<SoundManager>();
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

            if (!final) {
                //Control de gravedad en el salto.
                if (rb.velocity.y < 0) {
                    rb.gravityScale = 10;
                }
                else {
                    rb.gravityScale = 4;
                }

                //Control de aterrizaje a alta velocidad
                if (IsGrounded() && rb.velocity.y < 0) {
                    rb.velocity = Vector3.zero;
                }

                //checkeo de la tecla espacio para hacer saltos consecutivos
                jumping = CheckJumpKey();

                //Salto
                if (jumping && IsGrounded()) {
                    sm.PlaySound(SoundManager.SFX.JUMP);
                    Jump(jumpForce);
                    gamelogic.saltosCounter++;
                }

                //Comprovacion de colision frontal
                if (!gamelogic.godMode) {
                    if (IsCollidingFront()) {
                        sm.PlaySound(SoundManager.SFX.DEATH);
                        gamelogic.Restart();
                        Restart();
                    }
                }
            }
            else { //final
                //deceleracion final
                if (speedX > 0.5f) {
                    speedX /= 1.03f;
                }
                else {
                    speedX = 0;
                }
            }

        }
	}
    
    private bool CheckJumpKey() {
        if (jumpTimer > 0.03) {
            jumpTimer = 0.0f;
            return Input.GetKey(KeyCode.Space);
        }
        else {
            jumpTimer += Time.deltaTime;
            return jumping;
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

    public void Restart() {
        transform.position = spawnPoint.transform.position;
        final = false;
        speedX = initialSpeed;
        rb.velocity = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D c) {
        if (!gamelogic.godMode && c.tag.Equals("Kill")) {
            sm.PlaySound(SoundManager.SFX.DEATH);
            gamelogic.Restart();
            Restart();
        }
        else if (c.tag.Equals("Jump")) {
            Jump(speedUpForce);
        }
        else if (c.tag.Equals("Coin")) {
            sm.PlaySound(SoundManager.SFX.COIN);
            gamelogic.pointCounter += coinPoints;
            c.gameObject.SetActive(false);
        }
        else if (c.tag.Equals("Big_Coin")) {
            sm.PlaySound(SoundManager.SFX.STAR);
            gamelogic.bigCoinGrabbed[c.gameObject.GetComponent<BigCoin>().bigCoinID] = true;
            gamelogic.pointCounter += bigCoinPoints;
            c.gameObject.SetActive(false);
        }
        else if (c.tag.Equals("Final")) {
            final = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D c) {
        if (c.gameObject.tag.Equals("Box")) {
            c.gameObject.GetComponent<BoxAnimation>().play = true;
            gamelogic.pointCounter += boxPoints;
            sm.PlaySound(SoundManager.SFX.BOX);
        }
        else if (c.gameObject.tag.Equals("Enemy")) {
            Jump(jumpForce);
            sm.PlaySound(SoundManager.SFX.MONSTER);
            c.gameObject.GetComponent<Enemy>().currentState = Enemy.STATE.DIE;
            gamelogic.pointCounter += enemyPoints;
        }
    }
}
