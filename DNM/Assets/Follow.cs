using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {
    [SerializeField] private GameObject target;
    [SerializeField] private float offsetX = 0, offsetY = 0;
    [HideInInspector] public  float extraX = 0, extraY = 0; //estos offsets los controlan los triggers que varian el offset de la camara.
    private float interpolatedOffset;

	// Use this for initialization
	void Start () {
        transform.position = target.transform.position;
        Vector3 pos = transform.position;
        pos.x += (offsetX + extraX);
        pos.y = offsetY;
        pos.z = -10;
        transform.position = pos;
	}
	
	// Update is called once per frame
	void Update () {
        interpolatedOffset = Mathf.Lerp(transform.position.y, offsetY + extraY, 0.2f);
        if (Mathf.Abs(transform.position.y - (offsetY+extraY)) < 0.2f) {
            interpolatedOffset = extraY;
        }
        transform.position = new Vector3(target.transform.position.x + offsetX + extraX, offsetY + interpolatedOffset, -10);
	}
}
