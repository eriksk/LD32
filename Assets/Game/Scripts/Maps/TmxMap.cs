using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class TmxMap : MonoBehaviour
{
	public TextAsset File;

	public int Width;
	public int Height;
	public int TileWidth;
	public int TileHeight;

	public void Load()
	{
		// remove all children
		var children = new List<GameObject>();
		foreach (Transform child in transform) 
			children.Add(child.gameObject);
		children.ForEach(child => DestroyImmediate(child));
		

		var content = JSON.Parse(File.text);
		
		// props
		Width = content["width"].AsInt;
		Height = content["height"].AsInt;
		TileWidth = content["tilewidth"].AsInt;
		TileHeight = content["tileheight"].AsInt;

		// layers
		var layers = content["layers"];
		for (int i = 0; i < layers.Count; i++) 
		{
			var layer = layers[i];
			var l = new GameObject(layer["name"]);
			l.transform.parent = transform;
			l.AddComponent<TmxMapLayer>();
			l.GetComponent<TmxMapLayer>().CreateFromJson(layer);	
			l.transform.position = new Vector3(0, 0, i * -0.1f);		
		}
	}
}