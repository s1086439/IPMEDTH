using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ESP8266 : MonoBehaviour, Arduino
{
	[SerializeField]
	private String localIpESP; // IP-adres van de ESP8266
	[SerializeField]

	public String LocalIpESP {
		get { return localIpESP; }
		set { localIpESP = value; }
	}

	public IEnumerator GetValues(Sensor sensor)
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
				ConvertValues(www.downloadHandler.text, sensor);
			}
		}
	}

	private void ConvertValues(String text, Sensor sensor)
	{
		string[] data = text.Split(',');
		String v1 = data[0], v2 = data[1], v3 = data[2];
		sensor.X = float.Parse(v1, System.Globalization.CultureInfo.InvariantCulture);
		sensor.Y = float.Parse(v2, System.Globalization.CultureInfo.InvariantCulture);
		sensor.Z = float.Parse(v3, System.Globalization.CultureInfo.InvariantCulture);
	}
}