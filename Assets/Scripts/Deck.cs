using System.Collections.Generic;

namespace Cardgeon.Card
{
	public enum DeckUpgrades
    {
		GreedInfused,
		SideDecked,
		BottomedDecked,
		Sealed
    }

	public class Deck
	{
		public List<DeckUpgrades> deckUpgrades = new List<DeckUpgrades>();
		public List<CardScriptableObject> storedCards = new List<CardScriptableObject>();
	}
}
