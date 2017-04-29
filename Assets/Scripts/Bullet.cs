using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private float T = 0.0f;
    private float DeleteTime = 5.0f;
    private Rigidbody RB;
	public float speed = 100;
    //private Player PlayerScript;
	//private Vector3 Pspeed;

	// Use this for initialization
	void Start () {

        //PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        RB = GetComponent<Rigidbody>();
		//RB.velocity = PlayerScript.PlayerVel;
		//Pspeed = PlayerScript.PlayerVel;
		//RB.velocity1 = Pspeed;
		RB.velocity = transform.forward * speed;
		
	}
	
	// Update is called once per frame
	void Update () {
        T += Time.deltaTime;

		//Pspeed *= Time.deltaTime;
		//transform.Translate (Pspeed);

        if (T >= DeleteTime)
        {
            Destroy(gameObject);
        }

    }
}
