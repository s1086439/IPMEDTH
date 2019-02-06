using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ESP8266 : MonoBehaviour
{
	[SerializeField]
	private String localIpESP; // IP-adres van de ESP8266
	private float x, y, z;

	private void Start()
	{
		StartCoroutine(GetESPValues());
	}

	// GET requests naar de ESP8266.
	IEnumerator GetESPValues()
	{
		while (true)
		{
			UnityWebRequest www = UnityWebRequest.Get(localIpESP);
			yield return www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError)
			{
				Debug.Log(www.error);
			}
			else
			{
				ConvertESPValues(www.downloadHandler.text);
			}
		}
	}

	private void ConvertESPValues(String s)
	{
		string[] data = s.Split(',');
		String w1 = data[0], w2 = data[1], w3 = data[2];
		x = float.Parse(w1, System.Globalization.CultureInfo.InvariantCulture);
		y = float.Parse(w2, System.Globalization.CultureInfo.InvariantCulture);
		z = float.Parse(w3, System.Globalization.CultureInfo.InvariantCulture);
	}
	
	public float GetX(){
		return x;
	}
	
	public float GetY(){
		return y;
	}
	
	public float GetZ(){
		return z;
	}
}
