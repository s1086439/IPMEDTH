using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

/*	Klasse voor de ESP8266 voor het ophalen en doorgeven van zijn verkregen waarden.
	Het ophalen gebeurt middels GET-requests in een IEnumerator via een meegegeven ip-adres.
	Het ip-adres kan ik de inspector van het programma worden meegegeven. */

public class ESP8266 : MonoBehaviour, Arduino
{
	[SerializeField]
	private String localIpESP; // Het IP-adres van de ESP8266.
	private GyroAccSensor gyroAccSensor;

	public String LocalIpESP { get { return localIpESP; } } // Get voor het ip-adres
	public GyroAccSensor GyroAccSensor { get { return gyroAccSensor; } } // Get voor een gyroAccSensor

	void Awake()
	{
		gyroAccSensor = GetComponent<MPU6050>();
	}

	/*	Voer een IEnumerator uit zolang het programma draait.
		Doet dit middels GET-requests en het ip-adres.
		Wanneer er geen netwerkfout optreedt, roep dan de methode ConvertValues aan en geef hierbij
		de verkregen sensordata en sensortype mee. */
	public IEnumerator RequestValues()
	{
		while (true)
		{
			UnityWebRequest www = UnityWebRequest.Get("http://"+localIpESP);
			yield return www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError)
			{
				Debug.Log(www.error); // Output als er een netwerkfout optreedt.
			}
			else
			{
				ConvertValues(www.downloadHandler.text);
			}
		}
	}

	/*	Zet de sensordata van een string om naar floats. 
		Hierdoor kunnen de waarden x, y en z worden gebruikt voor het roteren van het been. 
		Zet tevens de variabelen x, y en z van de meegekregen sensor op de verkregen data. */
	private void ConvertValues(String text)
	{
		string[] data = text.Split(','); // Split de meegekregen string op ',' en zet deze in een array.
		String v1 = data[0], v2 = data[1], v3 = data[2]; // Aparte strings voor de strings in data.
		gyroAccSensor.X = float.Parse(v1, System.Globalization.CultureInfo.InvariantCulture); // Conversie String naar float.
		gyroAccSensor.Y = float.Parse(v2, System.Globalization.CultureInfo.InvariantCulture); // Conversie String naar float.
		gyroAccSensor.Z = float.Parse(v3, System.Globalization.CultureInfo.InvariantCulture); // Conversie String naar float.
	}
}
