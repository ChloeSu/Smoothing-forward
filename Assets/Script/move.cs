using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

    public GameObject eye;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = eye.transform.position;

	}
}
