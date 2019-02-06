using UnityEngine;

// State om het programma in rust te zetten.
public class StandbyState : HololensState
{
	public StandbyState(AppManager s, Hololens h)
	{
		base.sceneController = s;
		base.hololens = h;
	}

	public override void Back()
	{
		//base.sceneController.GetSpeaker().GetComponent<AudioSource>().Play();
	}

	public override void Calibrate()
	{
		//base.sceneController.GetSpeaker().GetComponent<AudioSource>().Play();
	}

	public override void Init()
	{
		base.sceneController.GetInteractiveContainer().SetActive(false);
	}

	public override void Next()
	{
		base.hololens.SetCurrentState(base.hololens.GetPreSurgeryState());
		base.hololens.GetCurrentState().Init();
	}

	public override void Restart()
	{
		//base.sceneController.GetSpeaker().GetComponent<AudioSource>().Play();
	}

	public override void Stop()
	{
		//base.sceneController.GetSpeaker().GetComponent<AudioSource>().Play();
	}
}
