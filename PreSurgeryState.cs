using UnityEngine;

// State voor het tonen van de spieren.
public class PreSurgeryState : HololensState
{
	public PreSurgeryState(Hololens h)
	{
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
		base.hololens.SceneManager.Leg.DisableHighlightLegParts();
		base.hololens.SceneManager.InteractiveContainer.SetActive(true);
		base.hololens.SceneManager.StateText.text = "Pre surgery";
	}

	public override void Next()
	{
		base.hololens.SetCurrentState(base.hololens.GetPostSurgeryState());
		base.hololens.GetCurrentState().Init();
	}

	public override void Restart()
	{
		base.hololens.SceneManager.Speaker.GetComponent<AudioSource>().Play();
	}

	public override void Stop()
	{
		base.hololens.SceneManager.Leg.DisableHighlightLegParts();
		base.hololens.SetCurrentState(base.hololens.GetStandbyState());
		base.hololens.GetCurrentState().Init();
	}
}
