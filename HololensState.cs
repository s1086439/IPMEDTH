public abstract class HololensState {
	protected SceneManager sceneController;
	protected Hololens hololens;

	public abstract void Init();
	public abstract void Calibrate();
	public abstract void Next();
	public abstract void Back();
	public abstract void Restart();
	public abstract void Stop();
}
