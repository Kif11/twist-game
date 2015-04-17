using UnityEngine;
using System.Collections;
// Young Chu
// Script handling health and armor values of player

public class PlayerValues : MonoBehaviour 
{
	private static PlayerValues _instanceTwo;

	// maximum health player can have
	[SerializeField]
	private float maxHealth = 100f;
	// max armor player can have
	[SerializeField]
	private float maxArmor = 100f;

	[SerializeField]
	private float _health = 100f;
	
	// armor absorbs all damage for a 1:1 ratio of the listed damage
	[SerializeField]
	private float _armor = 50f;

	// multiplier used in calculation of increased health damage, set to 1 for regular damage
	[SerializeField]
	private float healthDmgMultiplier = 2f;

	public bool isTeleporting = false;

	void Reset()
	{
		_health = 100f;
	}

	void Awake ()
	{
		if(_instanceTwo == null)
		{
			_instanceTwo = this;
		}
	}

	/// <summary>
	/// function used to deal damage to player
	/// typically from bullets/enemies
	/// </summary>
	public void PlayerHealthLoss (float damage)
	{
		// check if armor is more than 0 first
		if(_armor > 0)
		{
			// is the damage being dealt more than armor value? ...
			if(damage > _armor)
			{
				// store excessive damage into a float
				float leftoverDamage = damage - _armor;

				// apply damage to armor
				_armor -= damage;

				// check so it doesnt go negative armor
				if(_armor <= 0)
				{
					_armor = 0;
				}

				// deal damage with left over damage times multiplier
				_health = _health - (leftoverDamage * healthDmgMultiplier);

				//check if player is dead
				if(_health <= 0)
				{
					_health = 0;
					// game over
				}
			}
			// ... no? just deal damage to armor then
			_armor -= damage;
			if(_armor <= 0)
			{
				armor = 0;
			}
		}
		// dont have any armor? deal damage to health with following formula
		else
		{
			_health = _health - (damage * healthDmgMultiplier);
			if(_health <= 0)
			{
				_health = 0;
				// game over
			}
		}
	}


	/// <summary>
	/// function used to restore health
	/// typically from health collectables
	/// </summary>
	public void PlayerHealthGain (float gain)
	{
		_health += gain;
	}

	/// <summary>
	/// Used when healing, or taking scripted damage
	/// rather than from enemies
	/// </summary>
	public float health
	{
		get
		{
			return _health;
		}
		set
		{
			_health = value;
			
			/// checking if current health is lower than 0 (dead)
			if(_health <= 0)
			{
				_health = 0;
				// die/ game over
			}
			/// checking if current health value exceeds max health value
			/// is health greater than max Health value?
			if(_health >= maxHealth)
			{
				// if yes, set it to be just maxHealth value then
				_health = maxHealth;
			}
		}
	}
	
	public float armor
	{
		get
		{
			return _armor;
		}
		set
		{
			_armor = value;
			if(_armor <= 0)
			{
				_armor = 0;
			}
			if(_armor >= maxArmor)
			{
				_armor = maxArmor;
			}
		}
	}


	public static PlayerValues instanceTwo
	{
		get
		{
			if(_instanceTwo == null)
			{
				_instanceTwo = GameObject.FindObjectOfType<PlayerValues>();
			}
			
			return _instanceTwo;
		}
	}
}




