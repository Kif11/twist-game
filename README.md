#Description 
>**“The world doesn’t revolve around you,”**

Well, in Game Twist, it actually does! Game Title is a 2.5D action puzzler where the player progresses through the game by rotating entire levels around their self while they fight enemies with a variety of weapons, using the depth of the level to their advantage.

##List of Mechanics/Features
- Rotating levels around the player
- 2.5D gameplay and levels
- Maybe split level layouts into 2 zones [front & back]
- Have camera switch/rotate around player to other side of level whenever player enters different zone
- Swapping between multiple weapons at any time
- Taking extra damage from attacks while possessing no armor

##Asset List
###Player Classes
- Weapon classes instead of one big script

###Player Functions
- Weapon handling
- Switching weapons [switch statement]
- Taking away/adding ammo
- How aiming/firing works [Ray cast from player to mouse position]
- [Main Mechanic] Rotating level

###Player Variables
- Health
- Armor
- Weapon damage values
- Damage
- Fire rate
- Ammo
- Move speed
- Amount to rotate level [Might vary on certain levels]

###Enemy Classes
- Enemy Behavior (how it acts and certain values)
- Enemy Values (health)

###Enemy Functions
- Different States [switch statement]
- Idling
- Attacking
- Checking line of sight [ray cast/line cast]
- Attack (what happens when it attacks)
- Take damage (what happens when damage is taken)

###Enemy Variables
- Health
- Damage
- Fire rate
- Detection Range
- Pickups
- Health
- Armor
- Ammo
- Quest Item
