using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour {

	public PlayerProto Script;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter () {
		Script._isGrounding = true;
	}

	void OnTriggerExit () {
		Script._isGrounding = false;
	}
}
