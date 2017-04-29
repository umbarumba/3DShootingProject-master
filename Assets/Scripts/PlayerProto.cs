using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProto : MonoBehaviour {

	private float speed = 10.0f;
	private float jumpSpeed = 10.0f;
	private float boostForce = 50.0f;
	private float turnSpeed = 100.0f;

	public float Energy;
	public float EnergyMax = 2000.0f;

	public float Armor;
	public float ArmorMax = 10000.0f;

	private int ButtonCount = 0;

	private float lastBoostTime = 0f;
	private float boostInterval = 0.5f;

	public Rigidbody RB;

	private float StickOnTime = 0f;
	private float ButtonOnTime = 0f;

	private bool _StickOn = false;
	private bool _ButtonOn = false;

	private bool _isBoosting = false;
	private bool _canAxel = false;
	private bool _canFloat = true;
	private bool _canCharge = true;
	public bool _isGrounding = false;

	private Vector3 NowPos;
	private Vector3 BeforePos;
	public Vector3 PlayerV = Vector3.zero;

	//public Camera MainCamera;

	// Use this for initialization
	void Start () {
		RB = GetComponent<Rigidbody> ();
		Energy = EnergyMax;
		Armor = ArmorMax;
		NowPos = transform.position;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		float LStickX = Input.GetAxis ("Horizontal");
		float LStickY = Input.GetAxis ("Vertical");

		float RStickX = Input.GetAxis ("Horizontal2");
		float RStickY = Input.GetAxis ("Vertical2");

		if (LStickX == 0 && LStickY == 0) {
			_StickOn = false;
		} else {
			_StickOn = true;
		}

		if (Input.GetButton ("Cross")) {
			_ButtonOn = true;
		} else {
			_ButtonOn = false;
		}

		float translationX = LStickX * speed * Time.deltaTime;
		float translationY = LStickY * speed * Time.deltaTime;
		float rotationY = RStickX * turnSpeed * Time.deltaTime;
		float rotationX = RStickY * turnSpeed * Time.deltaTime;

		if (_StickOn) {
			StickOnTime += Time.deltaTime;
		} else {
			StickOnTime = 0f;
		}

		if (_ButtonOn) {
			ButtonOnTime += Time.deltaTime;
			ButtonCount += 1;
		} else {
			ButtonOnTime = 0f;
			ButtonCount = 0;
		}

		if (_ButtonOn) {
			if (StickOnTime > ButtonOnTime) {
				if (Time.time > lastBoostTime + boostInterval) {
					if (Energy >= 200f) {
						if (ButtonCount < 2) {
							lastBoostTime = Time.time;
							StartCoroutine (Boost (new Vector3 (LStickX, 0, LStickY)));
						}
						//if (ButtonOnTime > 0.5f) {
						//if (_canAxel) {
						//translationX *= 2;
						//translationY *= 2;
						//Energy -= 100f * Time.deltaTime;
						//}
						//}
					}
					if (ButtonOnTime > 0.5f) {
						if (_canAxel) {
							if (Energy > 0) {
								translationX *= 2;
								translationY *= 2;
								RB.velocity = new Vector3 (RB.velocity.x, 0, RB.velocity.z);
								_canCharge = false;
								Energy -= 100f * Time.deltaTime;
							} else {
								_canAxel = false;
								_canCharge = true;
							}
						}
					}

				}
			} else if (!_isBoosting) {
				if (_canFloat) {
					if (Energy > 0) {
						_canCharge = false;
						_canAxel = false;
						RB.velocity = new Vector3 (0, jumpSpeed, 0);
						Energy -= 100 * Time.deltaTime;
					} else {
						_canFloat = false;
						_canCharge = true;
					}
				}
			}
		} else {
			if (!_isBoosting) {
				_canCharge = true;
				_canFloat = true;
			}
		}

		transform.Translate (translationX, 0, translationY);
		transform.Rotate (0, rotationY, 0);
		Camera.main.transform.Rotate (rotationX * -1, 0, 0);

		BeforePos = NowPos;
		NowPos = transform.position;
		PlayerV = (NowPos - BeforePos) / Time.deltaTime;

		Energy = Mathf.Floor (Energy);
		EnergyManage ();
		ArmorManage ();
	}

	IEnumerator Boost (Vector3 direction) {
		_isBoosting = true;
		_canCharge = false;
		RB.velocity = Vector3.zero;
		if (_isGrounding) {
			RB.AddRelativeForce (direction * boostForce, ForceMode.Impulse);
		} else {
			direction += new Vector3 (0f, 0.01f, 0f);
			RB.AddRelativeForce (direction * boostForce, ForceMode.Impulse);
		}
		Energy -= 200f;
		yield return new WaitForSeconds (0.5f);
		RB.velocity = Vector3.zero;
		_isBoosting = false;
		_canAxel = true;
		_canCharge = true;
	}

	void EnergyManage () {
		if (_canCharge) {
			if (Energy < EnergyMax) {
				Energy += 200f * Time.deltaTime;
			} else {
				Energy = EnergyMax;
			}
		}
		if (Energy <= 0) {
			Energy = 0;
		}
	}

	public void ArmorManage () {
		if (Armor < 0.0f) { 
			//Destroy (gameObject);
		}
	}
}
