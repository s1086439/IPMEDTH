using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : MonoBehaviour {

	[SerializeField]
	private GameObject handle;
	private List<Transform> legParts;
	private List<Material> legPartsDefaultMaterials;
	[SerializeField]
	private Material inactiveObjectMaterial;
	private int partsCounter;

	private void Awake()
	{
		legParts = new List<Transform>();
		legPartsDefaultMaterials = new List<Material>();
	}

	private void Start()
	{
		foreach (Transform child in this.transform) { legParts.Add(child); partsCounter++; };
		StoreDefaultMaterials();
	}

	public void RotatePre(float x, float y, float z)
	{
		handle.transform.rotation = Quaternion.Lerp(handle.transform.rotation, Quaternion.Euler((-(x) / 2) - 35, -(y) / 2, (z / 2)), 0.1f);
	}

	public void RotatePost(float x, float y, float z)
	{
		handle.transform.rotation = Quaternion.Lerp(handle.transform.rotation, Quaternion.Euler((x / 2) - 35, -(y) / 2, -(z / 2)), 0.1f);
	}

	// Laten 'oplichten' de meegekregen spieren.
	public void HighLightLegPart(string[] toHighlight)
	{
		// Loop door de onderdelen van het been.
		for (int p = 0; p < partsCounter; p++)
		{
			legParts[p].gameObject.GetComponent<Renderer>().material = legPartsDefaultMaterials[p];
			bool found = false;
			// Controleren op overeenkomstige namen toHighlight h en onderdeel p.
			for (int h = 0; h < toHighlight.Length; h++)
			{
				if (toHighlight[h] == legParts[p].name)
				{
					found = true;
				}
			}
			if (!found)
			{
				// Onderdelen die niet overeenkomen met h een nieuwe material geven.
				legParts[p].gameObject.GetComponent<Renderer>().material = inactiveObjectMaterial;
			}
		}
	}

	private void StoreDefaultMaterials()
	{
		for (int p = 0; p < partsCounter; p++)
		{
			legPartsDefaultMaterials.Add(legParts[p].gameObject.GetComponent<Renderer>().material);
		}
	}

	// Alle onderdelen van het been hun oorspronkelijke material geven.
	public void DisableHighlightLegParts()
	{
		for (int p = 0; p < partsCounter; p++)
		{
			legParts[p].gameObject.GetComponent<Renderer>().material = legPartsDefaultMaterials[p];
		}
	}
}
