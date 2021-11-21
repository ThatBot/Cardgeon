using System.Collections.Generic;
using UnityEngine;

namespace Cardgeon.Card
{
	[CreateAssetMenu(fileName = "Deck", menuName = "ScriptableObjects/Deck", order = 0)]
	public class StarterDeckScriptableObject : ScriptableObject
	{
		public List<DeckUpgrades> defaultUpgrades;
		public List<CardScriptableObject> defaultCards;
	}
}
