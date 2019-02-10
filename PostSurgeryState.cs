﻿using UnityEngine;

/*	State voor de inversie van de rechtervoet.
	In deze fase worden de bewegingen van de voet omgedraaid.
*/
public class PostSurgeryState : HololensState
{
	public PostSurgeryState(Hololens h)
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
		base.hololens.SceneManager.Speaker.GetComponent<AudioSource>().Play();
	}

	public override void Init()
	{
		base.hololens.SceneManager.Leg.HighlightLegPart(new string[] { "tibial nerve", "tibialis anterior", "tibialis posterior" });
		base.hololens.SceneManager.StateText.text = "Post surgery";
	}

	public override void Next()
	{
		base.hololens.SceneManager.Speaker.GetComponent<AudioSource>().Play();
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
