using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cardgeon.Card;

namespace Cardgeon.Base
{
	public class RunController : MonoBehaviour
	{
        public StarterDeckScriptableObject starterDeck;
		public Deck deck = new Deck();

        #region Singleton
        private static RunController _instance;
        public static RunController Instance { get { return _instance; } }


        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }
        #endregion

        public void Start()
        {
            deck.deckUpgrades = starterDeck.defaultUpgrades;
            deck.storedCards = starterDeck.defaultCards;
        }

        public void AddCardToDeck(CardScriptableObject card)
        {
            deck.storedCards.Add(card);
        }

        public void ApplyDeckUpgrade(DeckUpgrades upgrade)
        {
            deck.deckUpgrades.Add(upgrade);
        }
    }
}
