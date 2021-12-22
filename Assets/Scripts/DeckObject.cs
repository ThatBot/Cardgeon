using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Deck", menuName = "CardgeonObjects/Deck", order = 2)]
public class DeckObject : ScriptableObject
{
	public List<CardObject> deckCards;
}
