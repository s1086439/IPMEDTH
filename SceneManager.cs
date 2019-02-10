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
	private MPU6050 mpu6050;
	[SerializeField]
	private GameObject speaker, interactiveContainer;
	[SerializeField]
	private Text voiceCommandText, stateText;
	private CustomText customText;
	[SerializeField]
	private Hololens hololens;

	public Text VoiceCommandText { get { return voiceCommandText; } set { voiceCommandText = value; } }
	public Text StateText { get { return stateText; } set { stateText = value; } }
	public CustomText CustomText { get { return customText; } set { customText = value; } }
	public Leg Leg { get { return leg; } }
	public ESP8266 Esp8266 { get { return esp8266; } }
	public MPU6050 Mpu6050 { get { return mpu6050; } }
	public GameObject Speaker { get { return speaker; } }
	public GameObject InteractiveContainer { get { return interactiveContainer; } }

	private void Awake()
	{
		customText = this.GetComponent<CustomText>();
		esp8266 = this.GetComponent<ESP8266>();
		mpu6050 = this.GetComponent<MPU6050>();
	}

	private void Start()
	{
		StartCoroutine(esp8266.GetValues(mpu6050));
	}

	void Update()
	{
		if (hololens.GetCurrentState() != hololens.GetStandbyState())
		{
			if (hololens.GetCurrentState() != hololens.GetPostSurgeryState())
			{
				leg.RotatePre(mpu6050.X, mpu6050.Y, mpu6050.Z);
			}
			else
			{
				leg.RotatePost(mpu6050.X, mpu6050.Y, mpu6050.Z);
			}
		}
	}
}
