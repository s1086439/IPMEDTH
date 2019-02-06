using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System.Collections.Generic;

public class Hololens : MonoBehaviour, ISpeechHandler
{
	private HololensState currentState, standbyState, preSurgeryState, postSurgeryState;
	[SerializeField]
	private SceneManager sceneController;

	/*	Aanmaken en toewijzen states.
		Het programma start in standbyState.
	*/
	void Start()
	{
		standbyState = new StandbyState(sceneController, this);
		preSurgeryState = new PreSurgeryState(sceneController, this);
		postSurgeryState = new PostSurgeryState(sceneController, this);
		currentState = standbyState;
		currentState.Init();
	}

	// Afhandelen stemcommando's voor huidige state.
	// Naast de aangeven voiceommands wordt er ook gereageerd op de namen van onderdelen van het been.
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
