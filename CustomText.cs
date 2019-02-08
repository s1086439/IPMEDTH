using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*	Een aparte klasse voor Text om gebruik te maken van lists i.p.v. arrays, 
	zonder de originele klasse aan te passen.
*/
public class CustomText : MonoBehaviour {

	// Een coroutine voor het vervagen van de meegegeven tekst.
	public IEnumerator Fade(Text text)
	{
		// Kleur van de tekst op groen zetten.
		text.color = new Color(0, 1, 0, 1); // r, g, b, a
		for (float i = 1; i >= 0; i -= Time.deltaTime)
		{
			// Alpha veranderen met i (1-0)
			text.color = new Color(0, 1, 0, i);
			yield return null;
		}
	}
}
