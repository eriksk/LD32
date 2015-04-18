using UnityEngine;
using System;

public class PuckShooter : MonoBehaviour
{
	public GameObject Puck;
	
	private bool wasDown = false;
	public float MinForce = 2f;
	public float MaxForce = 20f;
	public float MaxChargeTime = 1000f;

	private float currentCharge;

	void Update()
	{
		if(Puck == null)
			throw new ArgumentException("Puckshooter must have a puck");

		var down = Input.GetButton("Jump");

		if(down)
		{
			// add to load....
			currentCharge += Time.deltaTime * 1000f;

			// Stay still
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}

		if(!down && wasDown) // shoot
		{
			// calculate force from charge
			currentCharge = Mathf.Clamp(currentCharge, 0f, MaxChargeTime);
			var force = Mathf.Lerp(MinForce, MaxForce, currentCharge / MaxChargeTime);

			ShootPuck(force);
			currentCharge = 0f;
		}

		wasDown = down;
	}

	private void ShootPuck(float puckForce)
	{
		var direction = new Vector2(
			Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad), 
			Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad));

		var puck = (GameObject)Instantiate(Puck, transform.position, Quaternion.identity);

		puck.GetComponent<Rigidbody2D>().velocity = direction * puckForce;
	}
}