using UnityEngine;

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
		base.hololens.CurrentState = base.hololens.PreSurgeryState;
		base.hololens.CurrentState.Init();
	}

	public override void Calibrate()
	{
		base.hololens.Appmanager.Speaker.GetComponent<AudioSource>().Play();
	}

	public override void Init()
	{
		base.hololens.Appmanager.Leg.HighlightLegPart(new string[] { "tibial nerve", "tibialis anterior", "tibialis posterior" });
		base.hololens.Appmanager.StateText.text = "Post surgery";
	}

	public override void Next()
	{
		base.hololens.Appmanager.Speaker.GetComponent<AudioSource>().Play();
	}

	public override void Restart()
	{
		base.hololens.CurrentState =  base.hololens.PreSurgeryState;
		base.hololens.CurrentState.Init();
	}

	public override void Stop()
	{
		base.hololens.CurrentState = base.hololens.StandbyState;
		base.hololens.CurrentState.Init();
	}
}
