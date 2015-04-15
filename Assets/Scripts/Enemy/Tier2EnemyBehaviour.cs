using UnityEngine;
using System.Collections;
// Young Chu
// How enemies act


public class Tier2EnemyBehaviour : EnemyBehaviourClass
{
	void Reset()
	{
		// change vars under here
		dmg = 15f;
	}

	public override IEnumerator EnemyAttack()
	{
		canFire = false;
		Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);

		// Get bullet prefab's bulletbehaviour component and set enemyclass var as that
		enemyBulletClass = bulletPrefab.GetComponent<BulletBehaviour>();
		
		// set bullet damage
		enemyBulletClass.damage = dmg;


		yield return new WaitForSeconds (delay);
		canFire = true;
	}


}


