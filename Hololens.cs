using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System.Collections.Generic;

//	De context voor het state pattern binnen het programma.

public class Hololens : MonoBehaviour, ISpeechHandler
{
	private HololensState currentState, standbyState, preSurgeryState, postSurgeryState; // De states binnen het programma.
	[SerializeField]
	private SceneManager sceneController; // Dependency om vanuit een state objecten te bereiken.

	//	Aanmaken en toewijzen states.
	void Start()
	{
		standbyState = new StandbyState(sceneController, this);
		preSurgeryState = new PreSurgeryState(sceneController, this);
		postSurgeryState = new PostSurgeryState(sceneController, this);
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
				sceneController.GetStateText().text = recognizedText;
				sceneController.GetLeg().HighlightLegPart(new string[] { recognizedText });
				break;
		}

		// Aanpassen interface-tekst voor de gebruiker.
		sceneController.GetVoiceCommandsText().text = eventData.RecognizedText;
		StartCoroutine(sceneController.GetCustomText().Fade(sceneController.GetVoiceCommandsText()));
	}

	public HololensState GetCurrentState()
	{
		return currentState;
	}

	public HololensState GetStandbyState()
	{
		return standbyState;
	}

	public HololensState GetPreSurgeryState()
	{
		return preSurgeryState;
	}

	public HololensState GetPostSurgeryState()
	{
		return postSurgeryState;
	}

	public void SetCurrentState(HololensState s)
	{
		currentState = s;
	}
}
