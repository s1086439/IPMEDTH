/*	Abstracte klasse zoals in het state design pattern.
	Kan worden aangevuld met meerdere methodes. Deze moeten dan wel worden geimplementeerd door 
	de klassen die van deze klasse erven.
*/

public abstract class HololensState {
	protected Hololens hololens;

	public abstract void Init();
	public abstract void Calibrate();
	public abstract void Next();
	public abstract void Back();
	public abstract void Restart();
	public abstract void Stop();
}
