﻿using UnityEngine;

/*	State voor de inversie van de rechtervoet.
	In deze fase worden de bewegingen van de voet omgedraaid.
*/
public class PostSurgeryState : HololensState
{
	public PostSurgeryState(SceneManager s, Hololens h)
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
		base.sceneController.GetSpeaker().GetComponent<AudioSource>().Play();
	}

	public override void Init()
	{
		base.sceneController.GetLeg().HighlightLegPart(new string[] { "tibial nerve", "tibialis anterior", "tibialis posterior" });
		base.sceneController.GetStateText().text = "Post surgery";
	}

	public override void Next()
	{
		base.sceneController.GetSpeaker().GetComponent<AudioSource>().Play();
	}

	public override void Restart()
	{
		base.hololens.SetCurrentState(base.hololens.GetPreSurgeryState());
		base.hololens.GetCurrentState().Init();
	}

	public override void Stop()
	{
		base.hololens.SetCurrentState(base.hololens.GetStandbyState());
		base.hololens.GetCurrentState().Init();
	}
}
