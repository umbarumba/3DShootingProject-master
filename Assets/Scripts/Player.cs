using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private float jumpforce = 100f;
    public float Speed;
    public float RotationSpeed;
    private float AxisLR = 0f;
    //public GameObject BulletPrefab;
    //public float Gravity;
    private Rigidbody RB;
	private Vector3 BeforPos;
	private Vector3 NowPos;
    public Vector3 PlayerVel;
	private float T = 0;

	// Use this for initialization
	void Start () {
        //重力を強くする
        //Physics.gravity = new Vector3(0, Gravity * -1f, 0);

        RB = GetComponent<Rigidbody>();

		NowPos = transform.position;
		BeforPos = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		BeforPos = NowPos;
		NowPos = transform.position;

		PlayerVel = (BeforPos - NowPos) / Time.deltaTime;

        AxisLR = 0;
        if (Input.GetButton("L1"))
        {
            AxisLR = -1;
        }
        if(Input.GetButton("R1"))
        {
            AxisLR = 1;
        }

        float translationZ = Input.GetAxis("Vertical") * Speed;
		float rotation = Input.GetAxis ("Horizontal") * RotationSpeed;
        float translationX = AxisLR * Speed;
		PlayerVel = new Vector3 (translationX, 0, translationZ);
        translationZ *= Time.deltaTime;
        translationX *= Time.deltaTime;
		//PlayerVel = new Vector3 (translationX, 0, translationZ);
		rotation *= Time.deltaTime;
		//PlayerVel = new Vector3(translationX, 0, translationZ).normalized; //Bulletに渡す用
		transform.Translate(translationX, 0, translationZ);
        transform.Rotate(0, rotation, 0);

        //Spaceキーを押している間、上昇
		if (Input.GetButton("Circle"))
        {
			//Debug.Log("○が押されてます");
			RB.velocity = new Vector3(0, 10, 0);
			//T += Time.deltaTime;
			//if (T >= 1.0f) {
			//	Debug.Log (transform.position.y);
			//}
        }
	}
}
