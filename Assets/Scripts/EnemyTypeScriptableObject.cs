using UnityEngine;

namespace Cardgeon.Combat{
	[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy", order = 2)]
	public class EnemyTypeScriptableObject : ScriptableObject
	{
		public Sprite enemySprite;
		public string enemyName;
		public int enemyHealth;
		public int enemyMana;
	}
}
