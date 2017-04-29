using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletE : MonoBehaviour {

    private float T = 0.0f;
    private float DeleteTime = 5.0f;
    private Rigidbody RB;
	public float speed = 50;
	private float AP = 100;
	private PlayerProto PlayerProtoSC;

	//private Enemy EnemyScript;

	// Use this for initialization
	void Start () {

		PlayerProtoSC = GameObject.FindWithTag ("Player").GetComponent<PlayerProto> ();
        //EnemyScript = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Player>();
        RB = GetComponent<Rigidbody>();
		RB.velocity = transform.forward * speed;
		
	}
	
	// Update is called once per frame
	void Update () {
        T += Time.deltaTime;
        if (T >= DeleteTime)
        {
            Destroy(gameObject);
        }

    }

	void OnTriggerEnter (Collider col) {
		if (col.tag == "Player") {
			PlayerProtoSC.Armor -= AP;
			Destroy (gameObject);
		}
	}
}
