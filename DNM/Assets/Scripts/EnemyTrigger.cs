using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour {
    [SerializeField]private Enemy enemy;

    private void OnTriggerEnter2D(Collider2D c) {
        if (c.tag.Equals("Player")) {
            enemy.currentState = Enemy.STATE.PLAY;
        }
    }
}
