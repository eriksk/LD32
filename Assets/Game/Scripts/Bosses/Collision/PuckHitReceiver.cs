using UnityEngine;
using System;

public class PuckHitReceiver : MonoBehaviour
{
	public String Tag = "puck";

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == Tag)
		{
			Health health = null;

			if(gameObject.transform.parent != null)
			{
				health = gameObject.transform.parent.GetComponent<Health>();
			}
			else
			{
				health = gameObject.GetComponent<Health>();
			}

			health.DealDamage(collision.gameObject.GetComponent<Puck>().Damage);

			Destroy(collision.gameObject);
		}
	}
}