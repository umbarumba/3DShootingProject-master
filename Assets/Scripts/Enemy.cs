using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private float speed = 3f;
    private float rotationSmooth = 1f;

	private float distance;

    private Vector3 targetPosition;

    private float changeTargetSqrDistance = 40f;

    private Transform player;

    private Transform muzzle;
    public GameObject bulletPrefab;

    private float attackInterval = 0.5f;
    private float turretRotationSmooth = 0.8f;
    private float lastAttackTime;

    private float attackSqrDistance = 30f;
    private float pursuitSqrDistance = 40f;

    private bool _chaseBool;

	private PlayerProto PlayerSC;
	public BulletE BulletESC;

	private Vector3 NowPos;
	private Vector3 BeforePos;
	public Vector3 EnemyV = Vector3.zero;

    //private RaycastHit hit;

	// Use this for initialization
	void Start () {

        targetPosition = GetRandomPositionOnLevel();

        player = GameObject.FindWithTag("Player").transform;
		PlayerSC = GameObject.FindWithTag ("Player").GetComponent<PlayerProto> ();
        muzzle = GameObject.FindWithTag("Muzzle").transform;

		NowPos = player.transform.position;
		BeforePos = transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {

        _chaseBool = true;

		distance = Vector3.Distance(transform.position, player.position);
        //Debug.Log(sqrDistanceToPlayer);
		if(distance < attackSqrDistance)
        {
            _chaseBool = false;
            Attack();
        }

		if (distance > pursuitSqrDistance)
        {
            _chaseBool = false;
            Loitering();
        }

        if(_chaseBool == true)
        {
            Chasing();
        }

		BeforePos = NowPos;
		NowPos = transform.position;
		EnemyV = (NowPos - BeforePos) / Time.deltaTime;
	}

    public void Loitering ()
    {
		//float sqrDistanceToTarget = Vector3.Distance(transform.position, targetPosition);
		if (distance < changeTargetSqrDistance)
        {
            targetPosition = GetRandomPositionOnLevel();
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmooth);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public Vector3 GetRandomPositionOnLevel()
    {
        float levelSize = 55f;
        return new Vector3(Random.Range(-levelSize, levelSize), 0, Random.Range(-levelSize, levelSize));
    }

    public void Chasing ()
    {
        //Debug.Log("Chasing" + player.transform.position);
        //Quaternion TargetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        //transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, Time.deltaTime * rotationSmooth);

        transform.LookAt(player);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void Attack()
    {
		float distance2 = Vector3.Distance (player.transform.position, transform.position);
		//Debug.Log("距離:" + distance2);
		distance2 /= BulletESC.speed;
		//Debug.Log ("距離/弾速" + distance2);
		Vector3 AfterPos = player.transform.position + (PlayerSC.PlayerV * distance2);
		//Debug.Log ("予測位置" + AfterPos);
		transform.LookAt(AfterPos);

        if(Time.time > lastAttackTime + attackInterval)
        {
            Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
            lastAttackTime = Time.time;
        }
    }
}
