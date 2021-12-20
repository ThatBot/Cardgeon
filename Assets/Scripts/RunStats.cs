using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunStats : MonoBehaviour
{
    [Header("Battle")]
    public List<CardObject> cardsInDeck = new List<CardObject>();
    public int deckCapacity = 10;


	public static RunStats instance;
    private void Awake()
    {
        instance = this;
    }

    public void AddCardToDeck(CardObject card)
    {
        if(cardsInDeck.Count < deckCapacity && !cardsInDeck.Contains(card))
        {
            cardsInDeck.Add(card);
        }
    }

    public void RemoveCardFromDeck(CardObject card)
    {
        if (cardsInDeck.Contains(card))
        {
            cardsInDeck.Remove(card);
        }
    }
}
