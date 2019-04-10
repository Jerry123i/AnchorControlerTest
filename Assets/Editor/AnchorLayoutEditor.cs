using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnchorLayout))]
public class AnchorLayoutEditor : Editor
{
	public override void OnInspectorGUI()
	{
		AnchorLayout layout = (AnchorLayout)target;

		List<RectTransform> elements = layout.GetComponentsInDirectChildren();

		//foreach(RectTransform rt in layout.transform.GetComponentsInChildren<RectTransform>())
		//	elements.Add(rt);	

		//elements.RemoveAt(0);

		layout.nOfColums = EditorGUILayout.IntField("Colums", layout.nOfColums);
		if (layout.nOfColums <= 0)
			layout.nOfColums = 1;
				
		layout.nOfRows = Mathf.CeilToInt( (float)elements.Count/ (float)layout.nOfColums);
		if (layout.nOfRows <= 0)
			layout.nOfRows = 1;

		layout.elementH = EditorGUILayout.Slider("Element Height",layout.elementH, 0, 1);
		layout.elementL = EditorGUILayout.Slider("Element Lenght", layout.elementL, 0, 1);

		EditorGUILayout.Space();

		layout.leftBorder = EditorGUILayout.Slider("Left Border", layout.leftBorder, 0, 1);
		layout.rightBorder = EditorGUILayout.Slider("Right Border", layout.rightBorder, 0, 1);

		EditorGUILayout.Space();

		layout.topBorder = EditorGUILayout.Slider("Top Border", layout.topBorder, 0, 1);
		layout.bottomBorder = EditorGUILayout.Slider("Bottom Border", layout.bottomBorder, 0, 1);

		layout.centerFinalLine = EditorGUILayout.Toggle("Center Final Line", layout.centerFinalLine);

		int nOfElementsOnLast = elements.Count % layout.nOfColums;

		//Posicionamento

		int column = 0;
		int row = 0;


		float xGap;
		float yGap;

		if (layout.nOfColums > 1)
			xGap = (1 - (layout.nOfColums * layout.elementL) - (layout.rightBorder + layout.leftBorder)) / (layout.nOfColums - 1);
		else
			xGap = 0;

		if (xGap < 0)
			xGap = 0;

		if (layout.nOfRows > 1)
			yGap = (1 - (layout.nOfRows * layout.elementH) - (layout.topBorder + layout.bottomBorder)) / (layout.nOfRows - 1);
		else
			yGap = 0;

		if (yGap < 0)
			yGap = 0;

		for (int i = 0; i < elements.Count; i++)
		{
			column = i % layout.nOfColums;
			row = Mathf.FloorToInt(i / layout.nOfColums);

			RectTransform e = elements[i];

			float xMin;

			if (layout.centerFinalLine && row == layout.nOfRows-1 && nOfElementsOnLast > 0)
			{
				float normalLine = (layout.nOfColums * layout.elementL) + ((layout.nOfColums - 1) * xGap);
				float lastLine = nOfElementsOnLast * layout.elementL + ((nOfElementsOnLast - 1) * xGap);
				xMin = layout.leftBorder + ((normalLine - lastLine) / 2) + (column * (layout.elementL + xGap));
			}
			else
			{
				xMin = layout.leftBorder + (column * (layout.elementL + xGap));
			}

			float xMax = xMin + layout.elementL;

			float yMin = layout.bottomBorder + (row * (layout.elementH + yGap));
			float yMax = yMin + layout.elementH;

			e.anchorMin = new Vector2(xMin, yMin);
			e.anchorMax = new Vector2(xMax, yMax);

		}

		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Debug");
		EditorGUILayout.FloatField("xGap", xGap);
		EditorGUILayout.FloatField("yGap", yGap);
		EditorGUILayout.IntField("Number of Colums", layout.nOfColums);
		EditorGUILayout.IntField("Number of Rows", layout.nOfRows);
	}
}
