using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FractalEditor : ShaderGUI 
{
	int x = 1920;
	int y = 1080;

	public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
		//Draw default inspector
		base.OnGUI(materialEditor, properties);

		EditorGUILayout.Space();
		EditorGUILayout.Space();

		GUILayoutOption height = GUILayout.Height(50);

		EditorGUILayout.BeginVertical(EditorStyles.helpBox);

		x = EditorGUILayout.IntField("X resolution ", x);
		y = EditorGUILayout.IntField("Y resolution ", y);

		EditorGUILayout.Space();

		if (GUILayout.Button("Export", height))
		{
			ShaderExporter exporter = new ShaderExporter(x, y, (Material)materialEditor.target);
			exporter.ExportImage();
		}

		EditorGUILayout.EndVertical();
	}	
}
