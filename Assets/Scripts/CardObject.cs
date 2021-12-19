using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "CardgeonObjects/Card", order = 0)]
public class CardObject : ScriptableObject
{
	[Header("Visual Properties")]
	public string displayName;
	public string description;
	public Sprite sprite;
	public Sprite cardEventSprite;
	public Sprite secondaryCardEventSprite;

	[Header("Statistics")]
	public int damage;
	public int manaCost;
	public CardEvents cardEvent;
	public CardEvents secondaryCardEvent;
	public bool hasEventPrimary;
	public bool hasEventSecondary;
	public bool isSpell;

	[Header("Dungeon")]
	public int rarity;
}
