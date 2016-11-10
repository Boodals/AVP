using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Path))]
public class PathEditor : Editor
{

	private Path path;

	private SerializedProperty nodesArrayProp;

	void OnEnable()
	{
		path = (Path)target;

		nodesArrayProp = serializedObject.FindProperty("nodes");

		if(nodesArrayProp.arraySize < 4)
		{
			nodesArrayProp.arraySize = 4;

			nodesArrayProp.GetArrayElementAtIndex(nodesArrayProp.arraySize - 4).FindPropertyRelative("position").vector3Value = new Vector3(0f, 0f, 0f);
			nodesArrayProp.GetArrayElementAtIndex(nodesArrayProp.arraySize - 3).FindPropertyRelative("position").vector3Value = new Vector3(1f, 0f, 1f);
			nodesArrayProp.GetArrayElementAtIndex(nodesArrayProp.arraySize - 2).FindPropertyRelative("position").vector3Value = new Vector3(-1f, 0f, 3f);
			nodesArrayProp.GetArrayElementAtIndex(nodesArrayProp.arraySize - 1).FindPropertyRelative("position").vector3Value = new Vector3(0f, 0f, 4f);

			serializedObject.ApplyModifiedProperties();
		}
	}

	public override void OnInspectorGUI()
	{
		GUI.enabled = false;
		EditorGUILayout.IntField("Number of segments", (nodesArrayProp.arraySize - 1) / 3);
		GUI.enabled = true;

		GUILayout.BeginVertical();
		GUILayout.BeginHorizontal();

		if (GUILayout.Button("Add"))
		{
			nodesArrayProp.arraySize += 3;

			//Position of the end point of the path
			Vector3 end = nodesArrayProp.GetArrayElementAtIndex(nodesArrayProp.arraySize - 4).FindPropertyRelative("position").vector3Value;

			Vector3 endForward = Vector3.forward; //TODO: Get direction curve is facing

			nodesArrayProp.GetArrayElementAtIndex(nodesArrayProp.arraySize - 3).FindPropertyRelative("position").vector3Value = end + endForward * 1f;
			nodesArrayProp.GetArrayElementAtIndex(nodesArrayProp.arraySize - 2).FindPropertyRelative("position").vector3Value = end + endForward * 3f;
			nodesArrayProp.GetArrayElementAtIndex(nodesArrayProp.arraySize - 1).FindPropertyRelative("position").vector3Value = end + endForward * 4f;
		}
		if(nodesArrayProp.arraySize <= 4)
		{
			GUI.enabled = false;
		}
		if (GUILayout.Button("Remove"))
		{
			nodesArrayProp.arraySize -= 3;
		}
		GUI.enabled = true;

		GUILayout.EndHorizontal();
		GUILayout.EndVertical();

		serializedObject.ApplyModifiedProperties();
	}

	void OnSceneGUI()
	{
		foreach(SerializedProperty nodeProp in nodesArrayProp)
		{
			SerializedProperty posProp = nodeProp.FindPropertyRelative("position");

			posProp.vector3Value = Handles.PositionHandle(posProp.vector3Value, Quaternion.identity);
		}

		serializedObject.ApplyModifiedProperties();
	}

}
