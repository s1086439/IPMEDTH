using System;
using System.Collections;
using UnityEngine;

/*	Een aparte klasse voor een MPU-6050 (Gyroscoop- en versnellingssensor).
	Deze wordt gebruikt om zijn waarden op te slaan.
*/
	
public class MPU6050: GyroAccSensor
{
	private float x, y, z; // De x, y en z waarden van de mpu6050.

	// Getters en setters voor de deze klasse.
	public override float X { get { return x; } set{ x = value; } }
	public override float Y { get { return y; } set { y = value; } }
	public override float Z { get { return z; } set { z = value; } }
}
