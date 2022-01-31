using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

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
    private EventProcessor eventProcessor = new EventProcessor();

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

    [Header("Enemy Play Animation")]  
    [SerializeField] private float animDuration = 1f;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private Animator playedCardAnimator = null;
    [SerializeField] private GameObject normalCardGameObject = null;
    [SerializeField] private GameObject runeCardGameObject = null;
    AnimatorStateInfo defaultState;

    [Header("Battle UI")]
    [SerializeField] private Slider manaSlider = null;
    [SerializeField] private GameObject endTurnButtonObject = null;

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
        endTurnButtonObject.SetActive(true);
        playerMana = basePlayerMana;
        isPlayerTurn = true;
        TakeCard();
    }

    public void EndTurn()
    {
        endTurnButtonObject.SetActive(false);
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
            if (!card.isRune)
            {
                //Initialize the card visuals
                normalCardGameObject.GetComponent<RawImage>().texture = card.sprite.texture;
                normalCardGameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = card.displayName;
                normalCardGameObject.transform.GetChild(1).GetComponent<TMP_Text>().text = card.manaCost.ToString();
                normalCardGameObject.transform.GetChild(2).GetComponent<TMP_Text>().text = card.damage.ToString();
                if (card.cardEvent != null)
                {
                    normalCardGameObject.transform.GetChild(3).GetComponent<RawImage>().texture = card.cardEvent.sprite.texture;
                    if (card.sCardEvent != null)
                    {
                        normalCardGameObject.transform.GetChild(4).GetComponent<RawImage>().texture = card.sCardEvent.sprite.texture;
                        if (card.tCardEvent != null)
                        {
                            normalCardGameObject.transform.GetChild(5).GetComponent<RawImage>().texture = card.tCardEvent.sprite.texture;
                        }
                    }
                }
                normalCardGameObject.SetActive(true);

            }
            else
            {
                //Initialize the rune visuals
                runeCardGameObject.GetComponent<RawImage>().texture = card.sprite.texture;
                runeCardGameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = card.displayName;
                runeCardGameObject.transform.GetChild(1).GetComponent<TMP_Text>().text = card.manaCost.ToString();
                runeCardGameObject.SetActive(true);
            }

            playedCardAnimator.Play("Card Played");

            if(playedCardAnimator.GetCurrentAnimatorStateInfo(0).IsName("Default State"))
            {
                OnEnemyPlayAnimComplete(card);
            }
        }
    }

    public void OnEnemyPlayAnimComplete(CardObject card)
    {
        enemyMana -= card.manaCost;

        if (!card.isRune)
        {
            HurtPlayer(card.damage);
        }

        if (card.cardEvent != null)
        {
            ProcessEvent(card.cardEvent, true);
            if (card.sCardEvent != null)
            {
                ProcessEvent(card.sCardEvent, true);
                if (card.tCardEvent != null)
                {
                    ProcessEvent(card.tCardEvent, true);
                }
            }
        }

        if (enemyMana >= enemyObject.mana / 2)
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

        manaSlider.value = playerMana;

        if (!card.isRune)
        {
            HurtEnemy(card.damage);
        }

        if (card.cardEvent != null)
        {
            ProcessEvent(card.cardEvent, false);
            if (card.sCardEvent != null)
            {
                ProcessEvent(card.sCardEvent, false);
                if (card.tCardEvent != null)
                {
                    ProcessEvent(card.tCardEvent, false);
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

    public void ProcessEvent(CardEventObject _cardEvent, bool _isEnemy)
    {
        eventProcessor.ProcessEvent(_cardEvent, _isEnemy);
    }
}
