using UnityEngine;
using System;

public class Health : MonoBehaviour
{
	public int MaxHealth;
	private int health;	

	void Start()
	{
		health = MaxHealth;
	}

	public void DealDamage(int damage)
	{
		health -= damage;
		if(health < 0)
			health = 0;

		gameObject.SendMessage("OnDamaged", health);
	}
}