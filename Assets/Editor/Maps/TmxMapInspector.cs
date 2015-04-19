
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(TmxMap))]
public class TmxMapInspector : Editor{
	
	public override void OnInspectorGUI(){
		
		var map = (TmxMap)target;

		if(map != null){
			if(GUILayout.Button("Load")){
				map.Load();
			}
		}	

		base.OnInspectorGUI();
	}
}