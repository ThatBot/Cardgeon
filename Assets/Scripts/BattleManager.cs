using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void InitializedEnemy();

public class BattleManager : MonoBehaviour
{
    [Header("Hand")]
    public List<CardObject> hand = new List<CardObject>(4);
    public List<Card> handObjects = new List<Card>(4);
    public int maxCardsInHand = 4;
    public int maxFreeHandSlots = 4;
    [SerializeField] private GameObject cardPrefab = null;
    [SerializeField] private Transform cardHolderTransform = null;

    [Header("Battle")]
    public EnemyObject enemyObject = null;
    public GameObject enemyGameObject = null;
    public Transform enemyHolder = null;
    public event InitializedEnemy onEnemyInitialized;

    [Header("Enemy")]
    public int enemyHealth;
    public int enemyMana;
    public List<CardObject> deck = new List<CardObject>();

    [Header("Player")]
    public int playerHealth = 10;
    public int basePlayerHealth = 10;
    public int playerMana = 10;
    public int basePlayerMana = 10;
    public bool isPlayerTurn = true;

    public static BattleManager instance;
    private void Awake()
    {
        instance = this;
    }

    public void InitializeEnemy(EnemyObject _enemy)
    {
        if(enemyObject != null)
        {
            UnititializeEnemy();
        }

        enemyObject = _enemy;
        enemyGameObject = Instantiate(enemyObject.prefab, enemyHolder);
        deck = enemyObject.deck.deckCards;
        enemyHealth = enemyObject.health;
        enemyMana = enemyObject.mana;

        onEnemyInitialized?.Invoke();
    }

    public void UnititializeEnemy()
    {
        enemyObject = null;
        Destroy(enemyGameObject);
        enemyGameObject = null;
        deck = null;
        enemyHealth = 0;
        enemyMana = 0;
    }

    public void StartTurn()
    {
        playerMana = basePlayerMana;
        isPlayerTurn = true;
        TakeCard();
    }

    public void EndTurn()
    {
        enemyMana = enemyObject.mana;
        isPlayerTurn = false;
        EnemyPlayCard();
    }

    public void EnemyPlayCard()
    {
        int rand = Random.Range(0, deck.Count);

        CardObject card = deck[rand];

        if(enemyMana >= card.manaCost)
        {
            enemyMana -= card.manaCost;

            if (!card.isRune)
            {
                HurtPlayer(card.damage);
            }

            if (card.hasEventPrimary)
            {
                ProcessEvent(card.cardEvent, true);
                if (card.hasEventSecondary)
                {
                    ProcessEvent(card.secondaryCardEvent, true);
                    if (card.hasEventTertiary)
                    {
                        ProcessEvent(card.tertiaryCardEvent, true);
                    }
                }
            }
        }

        if(enemyMana >= enemyObject.mana / 2)
        {
            EnemyPlayCard();
            return;
        }
        else
        {
            StartTurn();
        }
    }

    public void TakeCard()
    {
        if(hand.Count < maxCardsInHand)
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
                if(handObjects[i].cardObject == card && !handObjects[i].GetComponent<Card>().cardEnabled)
                {
                    Debug.Log($"Card removed{handObjects[i].cardObject.displayName}");
                    Card cardToRemove = handObjects[i];
                    handObjects.RemoveAt(i);
                    Destroy(cardToRemove.gameObject);
                    break;
                }
            }
        }
    }

    public void PlayCard(CardObject card)
    {
        RemoveCardFromHand(card);

        playerMana -= card.manaCost;

        if (!card.isRune)
        {
            HurtEnemy(card.damage);
        }

        if (card.hasEventPrimary)
        {
            ProcessEvent(card.cardEvent, false);
            if (card.hasEventSecondary)
            {
                ProcessEvent(card.secondaryCardEvent, false);
                if (card.hasEventTertiary)
                {
                    ProcessEvent(card.tertiaryCardEvent, false);
                }
            }
        }
    }

    public void HurtEnemy(int damage)
    {
        enemyHealth -= damage;

        if(enemyHealth <= 0)
        {
            KillEnemy();
        }
    }

    public void HurtPlayer(int damage)
    {
        playerHealth -= damage;

        if(playerHealth <= 0)
        {
            KillPlayer();
        }
    }

    public void HealEnemy(int amount)
    {
        enemyHealth += amount;

        if(enemyHealth > enemyObject.health)
        {
            enemyHealth = enemyObject.health;
        }
    }

    public void HealPlayer(int amount)
    {
        playerHealth += amount;

        if(playerHealth > basePlayerHealth)
        {
            playerHealth = basePlayerHealth;
        }
    }

    public void KillEnemy()
    {
        Debug.Log("Enemy dead");
    }

    public void KillPlayer()
    {
        Debug.Log("You are dead");
    }

    public void ProcessEvent(CardEvents _cardEvent, bool _isEnemy)
    {

    }
}
