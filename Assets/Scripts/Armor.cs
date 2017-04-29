using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Armor : MonoBehaviour {

	public PlayerProto Script;

	public Text ArmorText;
	public Slider ArmorGage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		float perArmor = Script.Armor / Script.ArmorMax;
		ArmorText.text = "Armor : " + Script.Armor;
		ArmorGage.value = perArmor;
	}
}
