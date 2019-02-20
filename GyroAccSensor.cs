using UnityEngine;

public abstract class GyroAccSensor: MonoBehaviour, Hardware
{
	public abstract float X { get; set; }
	public abstract float Y { get; set; }
	public abstract float Z { get; set; }
}