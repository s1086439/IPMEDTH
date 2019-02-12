using UnityEngine;

// State om het programma in rust te zetten.
public class StandbyState : HololensState
{
	public StandbyState(Hololens h)
	{
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
		base.hololens.Appmanager.InteractiveContainer.SetActive(false);
	}

	public override void Next()
	{
		base.hololens.CurrentState = base.hololens.GetPreSurgeryState;
		base.hololens.CurrentState.Init();
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
