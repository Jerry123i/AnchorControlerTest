using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnchorLayout : MonoBehaviour
{
	public int nOfColums;
	public int nOfRows;

	public float elementH;
	public float elementL;

	public float leftBorder;
	public float rightBorder;
	public float topBorder;
	public float bottomBorder;

	public bool centerFinalLine;

	public List<RectTransform> GetComponentsInDirectChildren()
	{
		List<RectTransform> components = new List<RectTransform>();
		for (int i = 0; i < gameObject.transform.childCount; ++i)
		{
			RectTransform component = gameObject.transform.GetChild(i).GetComponent<RectTransform>();
			if (component != null)
				components.Add(component);
		}

		return components;
	}

}
