using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour {

    public virtual void Restart() {
        print("BASIC RESTART");
    }
    protected void AddToGamelogic(GameLogic gamelogic, InteractableItem item) {
        if (!gamelogic.interactableElements.Contains(item)) {
            gamelogic.interactableElements.Add(item);
        }
    }
}