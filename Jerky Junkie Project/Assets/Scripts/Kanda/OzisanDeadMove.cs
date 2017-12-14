using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OzisanDeadMove : MonoBehaviour {

	[SerializeField]
	float y = .2f;
	RectTransform r_transform;
	Vector3 vec;

	// Use this for initialization
	void Start () {
		r_transform = this.GetComponent<RectTransform> ();
		vec = r_transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		vec.y += y;
		r_transform.position = vec; 
		y += .2f;
	}
}
