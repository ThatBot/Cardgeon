using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [Header("Hand")]
    public List<CardObject> hand = new List<CardObject>(4);
    public List<Card> handObjects = new List<Card>(4);
    public int maxCardsInHand = 4;
    public int maxFreeHandSlots = 4;
    [SerializeField] private GameObject cardPrefab = null;
    [SerializeField] private Transform cardHolderTransform = null;

    public static BattleManager instance;
    private void Awake()
    {
        instance = this;
    }

    public void StartTurn()
    {
        TakeCard();
    }

    public void TakeCard()
    {
        if(hand.Count < 8)
        {
            int rand = Random.Range(0, RunStats.instance.cardsInDeck.Count);

            AddCardToHand(RunStats.instance.cardsInDeck[rand]);
        }
    }

    public void AddCardToHand(CardObject card)
    {
        if(hand.Count < maxCardsInHand)
        {
            maxFreeHandSlots--;
            hand.Add(card);
            AddCardObjects(card);
        }
    }

    public void AddCardObjects(CardObject card)
    {
        GameObject cardInstance = Instantiate(cardPrefab, cardHolderTransform);
        cardInstance.GetComponent<Card>().cardObject = card;
        cardInstance.GetComponent<Card>().InitializeCard();
        handObjects.Add(cardInstance.GetComponent<Card>());
    }

    public void RemoveCardFromHand(CardObject card)
    {
        if (hand.Contains(card))
        {
            hand.Remove(card);
            maxFreeHandSlots++;
            for (int i = 0; i < handObjects.Count; i++)
            {
                if(handObjects[i].cardObject == card)
                {
                    Card cardToRemove = handObjects[i];
                    handObjects.RemoveAt(i);
                    Destroy(cardToRemove.gameObject);
                }
            }
        }
    }

    public void PlayCard(CardObject card)
    {
        RemoveCardFromHand(card);
    }
}
