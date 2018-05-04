using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {
    [SerializeField] private GameObject target;
    [SerializeField] private float offsetX = 0, offsetY = 0;
    [HideInInspector] public  float extraX = 0, extraY = 0; //estos offsets los controlan los triggers que varian el offset de la camara.
    private Vector3 speed; //variable velocidad de la camara para hacer el SmoothDamp()
    private float interpolatedOffset;

	// Use this for initialization
	void Start () {
        transform.position = target.transform.position;
        Vector3 pos = transform.position;
        pos.x += (offsetX + extraX);
        pos.y = pos.y + offsetY + extraY;
        pos.z = -10;
        transform.position = pos;
        speed = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        interpolatedOffset = Mathf.Lerp(transform.position.y, offsetY + extraY, 0.1f);
        print("interpolated "+interpolatedOffset);
        print("extra " + extraY);
        if(Mathf.Abs(interpolatedOffset) < 0.3f) {
            interpolatedOffset = extraY;
        }
        transform.position = new Vector3(target.transform.position.x + offsetX + extraX, offsetY + extraY, -10);
	}
}
