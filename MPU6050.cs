using System;
using System.Collections;
using UnityEngine;

public class MPU6050: MonoBehaviour, Sensor
{
	private float x, y, z;

	public float X
	{
		get { return x; }
		set{ x = value; }
	}

	public float Y {
		get { return y; }
		set { y = value; }
	}

	public float Z {
		get { return z; }
		set { z = value; }
	}
}