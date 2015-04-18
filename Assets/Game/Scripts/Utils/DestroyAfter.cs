using UnityEngine;
using System;

public class DestroyAfter : MonoBehaviour
{
	public float AfterTime;

	void Update()
	{
		AfterTime -= Time.deltaTime * 1000.0f;

		if(AfterTime <= 0f)
			Destroy(gameObject);
	}
}