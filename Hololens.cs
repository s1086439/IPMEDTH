using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System.Collections.Generic;

//	De context voor het state pattern binnen het programma.

public class Hololens : MonoBehaviour, ISpeechHandler
{
	private HololensState currentState, standbyState, preSurgeryState, postSurgeryState; // De states binnen het programma.
	[SerializeField]
	private AppManager appManager;

	public AppManager Appmanager { get { return appManager; } }
	public HololensState CurrentState { get { return currentState; } set { currentState = value; }}
	public HololensState StandbyState { get { return standbyState; } }
	public HololensState PreSurgeryState { get { return preSurgeryState; } }
	public HololensState PostSurgeryState { get { return postSurgeryState; } }

	//	Aanmaken en toewijzen states.
	void Start()
	{
		standbyState = new StandbyState(this);
		preSurgeryState = new PreSurgeryState(this);
		postSurgeryState = new PostSurgeryState(this);
		currentState = standbyState; // Starten in de standby-fase.
		currentState.Init(); // Aanroepen initialisatie van de huidige fase.
	}

	/*	Afhandelen stemcommando's voor de huidige state.
		Naast de aangeven voiceommands wordt er ook gereageerd op de namen van onderdelen van het been.
		Het resultaat eventData komt voort uit de herkende worden, gebaseerd op de aangegeven woorden in de editor,
		én de namen van de onderdelen van het been.
	*/
	public void OnSpeechKeywordRecognized(SpeechEventData eventData)
	{
		string recognizedText = eventData.RecognizedText.ToLower();
		switch (recognizedText)
		{
			case "start":
				if (currentState == standbyState) { standbyState.Next(); }
				break;
			case "on":
				if (currentState == standbyState) { standbyState.Next(); }
				break;
			case "calibrate":
				currentState.Calibrate();
				break;
			case "next":
				currentState.Next();
				break;
			case "back":
				currentState.Back();
				break;
			case "restart":
				currentState.Restart();
				break;
			case "stop":
				currentState.Stop();
				break;
			case "off":
				currentState.Stop();
				break;
			case "pre surgery":
				currentState = preSurgeryState;
				currentState.Init();
				break;
			case "post surgery":
				currentState = postSurgeryState;
				currentState.Init();
				break;
			default:
				appManager.VoiceCommandText.text = recognizedText;
				appManager.Leg.HighlightLegPart(new string[] { recognizedText });
				break;
		}

		// Aanpassen interface-tekst voor de gebruiker.
		appManager.VoiceCommandText.text = eventData.RecognizedText;
		StartCoroutine(appManager.CustomText.Fade(appManager.VoiceCommandText));
	}
}
