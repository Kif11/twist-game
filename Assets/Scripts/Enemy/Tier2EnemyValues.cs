using UnityEngine;
using System.Collections;
// Young Chu
// Handles health value and what happens when it takes damage/dies

public class Tier2EnemyValues : EnemyValuesClass 
{

	void Reset()
	{
		_enemyHealth = 25f;
	}


	// Change what happens when enemy takes damage
	public override void TakeDamage (float damage)
	{
		_enemyHealth -= damage;

		// change new color parameter when appropiate (ie, enemy current color is red)
		StartCoroutine(ColorChange(this.gameObject, this.renderer.material.color, Color.red, .2f)); 
		if(_enemyHealth <= 0)
		{
			// spawn something
			Destroy (this.gameObject);
		}
	}

	// Change what happens during color change coroutine
	public override IEnumerator ColorChange(GameObject thisThing, Color oldColor, Color newColor, float length)
	{
		thisThing.renderer.material.color = newColor; 
		yield return new WaitForSeconds(length); 
		thisThing.renderer.material.color = oldColor; 
	}

}
