using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "CardgeonObjects/Enemy", order = 1)]
public class EnemyObject : ScriptableObject
{
	[Header("Cosmetic")]
	public string displayName;
	public GameObject prefab;

	[Header("Cards")]
	public DeckObject deck;

	[Header("Stats")]
	public int health;
	public int mana;
}
