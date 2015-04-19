using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class TmxMapLayer : MonoBehaviour
{
	public int[] data;
	public int width, height;
	public float opacity;

	public void CreateFromJson(JSONNode json)
	{
		data = new int[json["data"].AsArray.Count];
		for (int i = 0; i < json["data"].AsArray.Count; i++) 
		{
			data[i] = json["data"][i].AsInt - 1;			
		}
		width = json["width"].AsInt;
		height = json["height"].AsInt;
		opacity = json["opacity"].AsFloat;

		var meshFilter = gameObject.AddComponent<MeshFilter>();
		var renderer = gameObject.AddComponent<MeshRenderer>();

		renderer.receiveShadows = false;
		renderer.useLightProbes = false;
		renderer.sharedMaterial = transform.parent.GetComponent<MeshRenderer>().sharedMaterial;

		meshFilter.mesh = new TmxMeshCreator().CreateMesh(data, width, height, 8, 8, true, -1);
	}

	void Start()
	{
		GetComponent<MeshRenderer>().material.color = new Color(opacity, opacity, opacity, 1f);
	}
}