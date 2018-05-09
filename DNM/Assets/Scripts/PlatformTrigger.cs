using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour {
    [SerializeField] private PlataformasMoviles pm;

    private void OnTriggerEnter2D(Collider2D c) {
        if (c.tag.Equals("Player")) {
            pm.currentState = PlataformasMoviles.PLATSTATE.MOVE;
        }
    }
}
