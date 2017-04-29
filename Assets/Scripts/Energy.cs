using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour {

	public PlayerProto Script;

	public Text EnergyText;
	public Slider EnergyGage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		float perEnergy = Script.Energy / Script.EnergyMax;

		EnergyText.text = "Energy : " + Script.Energy;
		EnergyGage.value = perEnergy;

	}
}
