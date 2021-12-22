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
	public Sprite tertiaryCardEventSprite;

	[Header("Statistics")]
	public int damage;
	public int manaCost;
	public CardEvents cardEvent;
	public CardEvents secondaryCardEvent;
	public CardEvents tertiaryCardEvent;
	public bool hasEventPrimary;
	public bool hasEventSecondary;
	public bool hasEventTertiary;
	public bool isRune;

	[Header("Dungeon")]
	public int rarity;
}
