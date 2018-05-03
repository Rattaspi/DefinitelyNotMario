using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOffset : MonoBehaviour {
    [SerializeField] private Follow camera;
    [SerializeField] private float x = 0, y = 0;

    private void Start() {
        //camera = FindObjectOfType<Follow>();
    }

    private void ChangeCameraOffset() {
        print("change called");
        camera.extraX = x;
        camera.extraY = y;
    }

    private void OnTriggerEnter2D(Collider2D c) {
        print(c.tag);
        if (c.tag.Equals("Player")) {
            ChangeCameraOffset();
        } 
    }
}
