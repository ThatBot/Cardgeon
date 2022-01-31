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

	[Header("Statistics")]
	public int damage;
	public int manaCost;
	public CardEventObject cardEvent;
	public CardEventObject sCardEvent; //s = secondary
	public CardEventObject tCardEvent; //t = tertiary
	public bool isRune;

	[Header("Dungeon")]
	public int rarity;
}
