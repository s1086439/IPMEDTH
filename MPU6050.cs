using System;
using System.Collections;
using UnityEngine;

/*	Een aparte klasse voor een MPU-6050 (Gyroscoop- en versnellingssensor).
	Deze wordt gebruikt om zijn waarden op te slaan.
*/
	
public class MPU6050: MonoBehaviour, GyroAccSensor
{
	private float x, y, z; // De x, y en z waarden van de mpu6050.

	// Getters en setters voor de deze klasse.
	public float X { get { return x; } set{ x = value; } }
	public float Y { get { return y; } set { y = value; } }
	public float Z { get { return z; } set { z = value; }}
}
