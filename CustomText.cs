using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*	Een extra klasse voor Text.
	Het uitbreiden van de klasse Text is lastig en niet gewenst i.v.m. mogelijke Unity-updates.
*/
public class CustomText : MonoBehaviour {

	// Coroutine voor het vervagen van de meegegeven tekst.
	public IEnumerator Fade(Text text)
	{
		text.color = new Color(0, 1, 0, 1); // r, g, b, a
		for (float i = 1; i >= 0; i -= Time.deltaTime)
		{
			text.color = new Color(0, 1, 0, i);
			yield return null;
		}
	}
}
