using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public float RotMaxX = 60f;
    public float RotMaxY = 120f;
    private float AxisX;

    //private float _CanShot = 0;
    private float AxisY;

	//private float ShotT;
	private float attackInterval = 0.1f;
	private float lastAttackTime;

    public GameObject BulletPrefab;
    public GameObject TargetSprite;

    private RaycastHit hit;

	private GameObject Enemy;
	private Enemy EnemyScript;
	public Bullet BulletScript;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

        AxisX = Input.GetAxis("Horizontal2") * 0.5f;
        AxisY = Input.GetAxis("Vertical2") * 0.5f;

        //Debug.Log(AxisX + "," + AxisY);

        float RotationX = RotMaxX * AxisY;
        float RotationY = RotMaxY * AxisX;

        //this.transform.localRotation = Quaternion.Euler(0, RotationY, 0f);
		this.transform.localRotation = Quaternion.Euler (RotationX, RotationY, 0f);

        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, 10f))
        {
            //Debug.Log("HIT" + hit.point);
			if (hit.collider.tag != "Bullet") {
				TargetSprite.transform.position = hit.point;
			}
			//if (hit.collider.tag == "Enemy") {
				//Enemy = hit.collider.gameObject;
				//EnemyScript = Enemy.GetComponent<Enemy> ();
				//DeviationShot ();
			//}
        }
        else
        {
            TargetSprite.transform.localPosition = new Vector3 (0f, 0f, 10f);
        }

        if (Input.GetButton("R2"))
        {
			//撃つ(0秒)→0.5秒待つ→撃つ(0.5秒)→0.5秒待つ→撃つ(1.0秒)
			Shot ();
        }
    }

    void Shot ()
    {
		if (Time.time > lastAttackTime + attackInterval) {
			Instantiate (BulletPrefab, transform.position, transform.rotation);
			lastAttackTime = Time.time;
		}
    }

	void DeviationShot () {
		Vector3 NowPos = Enemy.transform.position;
		float distance = Vector3.Distance (Enemy.transform.position, transform.position);
		distance /= BulletScript.speed;
		Vector3 AfterPos = NowPos + (EnemyScript.EnemyV * distance);
		transform.LookAt (AfterPos);
		Shot ();
	}
}
