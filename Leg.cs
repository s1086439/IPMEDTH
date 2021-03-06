﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : MonoBehaviour {

	[SerializeField]
	private GameObject handle; // Handle voor het roteren van de voet middel.
	private List<Transform> legParts; // Opslag voor de onderdelen van het been.
	private List<Material> legPartsDefaultMaterials; // Opslag voor de standaard materialen van de onderdelen van het been.
	[SerializeField]
	private Material inactiveObjectMaterial; // Inactieve materiaal voor een onderdeel van het been.
	private int partsCounter; // Teller voor het bijhouden van het aantal onderdelen van het been.

	private void Awake()
	{
		legParts = new List<Transform>();
		legPartsDefaultMaterials = new List<Material>();
	}

	/*	Namen childs van Leg omzetten naar kleine letters. 
		Plaatsen childs van Leg in legParts. 
		partsCounter verhogen */
	private void Start()
	{
		foreach (Transform child in this.transform) { child.name = child.name.ToLower(); legParts.Add(child); partsCounter++; };
		StoreDefaultMaterials();
	}

	/*	Roteren been middels de verkregen x, y en z waarden, voor de pre surgery fase. 
		Hier wordt het object 'handle' gedraaid. */
	public void RotatePre(float x, float y, float z)
	{
		handle.transform.rotation = Quaternion.Lerp(handle.transform.rotation, Quaternion.Euler((x / 2) - 35, -(y) / 2, -(z / 2)), 0.1f);
	}

	/*	Roteren been middels de verkregen x, y en z waarden, voor de post surgery fase.  
		Hier wordt het object 'handle' gedraaid. */
	public void RotatePost(float x, float y, float z)
	{
		handle.transform.rotation = Quaternion.Lerp(handle.transform.rotation, Quaternion.Euler((-(x) / 2) - 35, -(y) / 2, (z / 2)), 0.1f);
	}

	/*	Oplichten onderdeel been middels verkregen array toHighlight.
		Itereren over legParts middels p.
		Itereren over array toHighlight, en vergelijken object van legParts. 
		Zet de material van een niet gevonden onderdeel van het been op een speciale material genaamd inactiveObjectMaterial */
	public void HighlightLegPart(string[] toHighlight)
	{
		for (int p = 0; p < partsCounter; p++)
		{
			legParts[p].gameObject.GetComponent<Renderer>().material = legPartsDefaultMaterials[p];
			bool found = false;
			for (int h = 0; h < toHighlight.Length; h++)
			{
				if (toHighlight[h] == legParts[p].name)
				{
					found = true;
				}
			}
			if (!found)
			{
				legParts[p].gameObject.GetComponent<Renderer>().material = inactiveObjectMaterial;
			}
		}
	}
	
	//	Opslaan van de standaard materials van de onderdelen van het been.
	private void StoreDefaultMaterials()
	{
		for (int p = 0; p < partsCounter; p++)
		{
			legPartsDefaultMaterials.Add(legParts[p].gameObject.GetComponent<Renderer>().material);
		}
	}

	// Highlights uitzetten op alle onderdelen van het been middels het verkrijgen van de opgeslagen standaard materials in legPartsDefaultMaterials.
	public void DisableHighlightLegParts()
	{
		for (int p = 0; p < partsCounter; p++)
		{
			legParts[p].gameObject.GetComponent<Renderer>().material = legPartsDefaultMaterials[p];
		}
	}
}
