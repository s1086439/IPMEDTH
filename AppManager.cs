using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

/*	AppManager dient als een centrale klasse voor objecten in de scene.
	In deze klasse wordt tevens de methode Update uitgevoerd voor onze applicatie. */	

public class AppManager : MonoBehaviour
{
	/*	SerializeField om de variabelen private te houden voor andere klassen, 
		maar wel toegankelijk te houden voor de editor.
		Eén klasse voor interactieve objecten.
		Handle om te bewegen voor IK. */
	[SerializeField]
	private Leg leg; // Het been,
	private ESP8266 esp8266; // De Arduino met de microcontroller.
	[SerializeField]
	private GameObject speaker, interactiveContainer; // Speaker voor geluidsmeldingen, interactieve container voor de eindgebruiker.
	[SerializeField]
	private Text voiceCommandText, stateText; // VoiceCommandText voor weergave stemcommando, stateText voor weergave fase.
	private CustomText customText; // Aangepaste klasse Text.
	[SerializeField]
	private Hololens hololens; // Context voor state pattern.

	// Getters en setters voor de variabelen in deze klasse.
	public Text VoiceCommandText { get { return voiceCommandText; } set { voiceCommandText = value; } }
	public Text StateText { get { return stateText; } set { stateText = value; } }
	public CustomText CustomText { get { return customText; } set { customText = value; } }
	public Leg Leg { get { return leg; } }
	public GameObject Speaker { get { return speaker; } }
	public GameObject InteractiveContainer { get { return interactiveContainer; } }

	// Toewijzen waarden door middel van het verkrijgen van de bijbehorende componenten in de AppManager prefab.
	private void Awake()
	{
		customText = this.GetComponent<CustomText>();
		esp8266 = this.GetComponent<ESP8266>();
	}

	private void Start()
	{
		/*	Starten coroutine om de waarden van de mpu6050 te verkijgen 
			gedurende het 'draaien' van het programma. */
		StartCoroutine(esp8266.RequestValues());
	}

	/*	Methode die gedurende het programma draait en controleert in welke state het programma zich bevindt.
		Bevindt het zich niet in de standby state, controleer dan of het programma zich in post surgery state bevindt.
		Zo niet, voer de methode RotatePre aan in Leg. Voer anders RotatePost uit. */
	void Update()
	{
		if (hololens.CurrentState != hololens.StandbyState)
		{
			if (hololens.CurrentState != hololens.PostSurgeryState)
			{
				leg.RotatePre(esp8266.GyroAccSensor.X, esp8266.GyroAccSensor.Y, esp8266.GyroAccSensor.Z); // Haal de waardes op uit mpu6050.
			}
			else
			{
				leg.RotatePost(esp8266.GyroAccSensor.X, esp8266.GyroAccSensor.Y, esp8266.GyroAccSensor.Z); // Haal de waardes op uit mpu6050.
			}
		}
	}
}
