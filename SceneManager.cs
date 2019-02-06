using UnityEngine;
//using SocketIO;
using System;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class SceneManager : MonoBehaviour
{
	/*	SerializeField om de variabelen private te houden voor andere klassen, 
		maar wel toegankelijk te houden voor de editor.
		Eén klasse voor interactieve objecten.
		Handle om te bewegen voor IK.
		Build: .NET
	*/
	[SerializeField]
	private Leg leg;
	private ESP8266 esp8266;
	[SerializeField]
	private GameObject speaker, interactiveContainer;
	[SerializeField]
	private Text voiceCommandText, stateText;
	[SerializeField]
	private Hololens hololens;

	private void Awake()
	{
		esp8266 = GetComponent<ESP8266>();
	}

	void Update()
	{
		if (hololens.GetCurrentState() != hololens.GetStandbyState())
		{
			// ConvertESPValues() na het binnenkrijgen van "sensordata" van de socket.
			if (hololens.GetCurrentState() != hololens.GetPostSurgeryState())
			{
				// Rotatie zetten van de handle voor de voet.
				leg.RotatePre(esp8266.GetX(), esp8266.GetY(), esp8266.GetZ());
			}
			else
			{
				// Rotatie zetten van de handle voor de voet 'na de ingreep'.
				leg.RotatePost(esp8266.GetX(), esp8266.GetY(), esp8266.GetZ());
			}
		}
	}

	public GameObject GetInteractiveContainer()
	{
		return interactiveContainer;
	}

	public GameObject GetSpeaker()
	{
		return speaker;
	}

	public Text GetVoiceCommandsText()
	{
		return voiceCommandText;
	}

	public Text GetStateText()
	{
		return stateText;
	}

	public Leg GetLeg()
	{
		return leg;
	}

	public CustomText GetCustomText()
	{
		return GetComponent<CustomText>();
	}
}
