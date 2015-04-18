using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
	public float Speed = 10;

	void Start()
	{

	}

	void FixedUpdate()
	{
		var direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

		if(direction == Vector2.zero)
			return;

		var rigidBody = GetComponent<Rigidbody2D>();

		rigidBody.AddForce(direction * Speed);

         float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
         transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}