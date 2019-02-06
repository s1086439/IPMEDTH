using UnityEngine;

// State voor het tonen van de spieren.
public class PreSurgeryState : HololensState
{
	public PreSurgeryState(AppManager s, Hololens h)
	{
		base.sceneController = s;
		base.hololens = h;
	}

	public override void Back()
	{
		base.hololens.SetCurrentState(base.hololens.GetPreSurgeryState());
		base.hololens.GetCurrentState().Init();
	}

	public override void Calibrate()
	{
		//base.sceneController.GetSpeaker().GetComponent<AudioSource>().Play();
	}

	public override void Init()
	{
		// Meegeven onderdelen om te laten 'oplichten'.
		base.sceneController.GetLeg().DisableHighlightLegParts();
		base.sceneController.GetInteractiveContainer().SetActive(true);
		base.sceneController.GetStateText().text = "Pre surgery";
	}

	public override void Next()
	{
		base.hololens.SetCurrentState(base.hololens.GetPostSurgeryState());
		base.hololens.GetCurrentState().Init();
	}

	public override void Restart()
	{
		base.sceneController.GetSpeaker().GetComponent<AudioSource>().Play();
	}

	public override void Stop()
	{
		base.sceneController.GetLeg().DisableHighlightLegParts();
		base.hololens.SetCurrentState(base.hololens.GetStandbyState());
		base.hololens.GetCurrentState().Init();
	}
}
