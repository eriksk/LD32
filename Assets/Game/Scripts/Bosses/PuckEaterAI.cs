using UnityEngine;
using System;

public class PuckEaterAI : MonoBehaviour
{
	private GameObject player;

	void Start()
	{
		player = GameObject.Find("Player");
	}

	void Update()
	{
		var currentAngle = transform.rotation.eulerAngles.z;
		var angleToPlayer = Mathf.Atan2(
			player.transform.position.y - transform.position.y, 
			player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
		var targetAngle = Mathf.LerpAngle(currentAngle, angleToPlayer, 0.1f);

        transform.rotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);
	}
}