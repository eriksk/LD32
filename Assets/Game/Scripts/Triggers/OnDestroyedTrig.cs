using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class OnDestroyedTrig : MonoBehaviour
{
	public List<GameObject> Prefabs;

	void OnDestroyed()
	{
		if(Prefabs == null)
			return;

		foreach (var obj in Prefabs) 
		{
			if(obj == null) continue;

			Instantiate(obj, transform.position, Quaternion.identity);			
		}
	}
}