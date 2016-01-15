using UnityEngine;
using System.Collections;
// Young Chu
// Value class for enemies

public class EnemyValuesClass : MonoBehaviour 
{
	// Standard enemy health
	public float _enemyHealth = 17;


	public virtual void TakeDamage (float damage)
	{
		// subtract damage from health
		_enemyHealth -= damage;
		// run color change coroutine to show got hit
		StartCoroutine(ColorChange(this.gameObject, this.GetComponent<Renderer>().material.color, Color.red, .2f)); 

		// check health values
		if(_enemyHealth <= 0)
		{
			// spawn something
			Destroy (this.gameObject);
		}
	}

	// overridable basic enemy color change to show enemy was hit
	public virtual IEnumerator ColorChange(GameObject thisThing, Color oldColor, Color newColor, float length)
	{
		thisThing.GetComponent<Renderer>().material.color = newColor; 
		yield return new WaitForSeconds(length); 
		thisThing.GetComponent<Renderer>().material.color = oldColor; 
	}
}
