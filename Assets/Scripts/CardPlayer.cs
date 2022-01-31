using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CardPlayer : MonoBehaviour
{
    [Header("Card Visuals")]
    [SerializeField] private GameObject cardPlayerObject = null;
	[SerializeField] private RawImage cardImage = null;
	[SerializeField] private RawImage runeImage = null;
	[SerializeField] private RawImage cardEventImage = null;
	[SerializeField] private RawImage cardSecondaryEventImage = null;
	[SerializeField] private RawImage cardTertiaryEventImage = null;
    [SerializeField] private TMP_Text nameText = null;
    [SerializeField] private TMP_Text manaText = null;
    [SerializeField] private TMP_Text runeNameText = null;
    [SerializeField] private TMP_Text runeManaText = null;
    [SerializeField] private TMP_Text damageText = null;
    private CardObject cardObject = null;
    private Card card = null;
    
    public static CardPlayer instance;

    private void Awake()
    {
        instance = this;
    }

    public void SetCard(CardObject cardObject, Card cardComponent)
    {
        if(card != null)
            DismissCard();
        SetCardVisuals(cardObject);
        this.cardObject = cardObject;
        card = cardComponent;
        card.SwitchEnabled();
        cardPlayerObject.SetActive(true);
    }

    private void SetCardVisuals(CardObject card)
    {
        cardEventImage.gameObject.SetActive(false);
        cardSecondaryEventImage.gameObject.SetActive(false);
        cardTertiaryEventImage.gameObject.SetActive(false);

        if (!card.isRune)
        {
            manaText.gameObject.SetActive(true);
            nameText.gameObject.SetActive(true);
            damageText.gameObject.SetActive(true);
            cardImage.gameObject.SetActive(true);

            runeImage.gameObject.SetActive(false);
            runeManaText.gameObject.SetActive(false);
            runeNameText.gameObject.SetActive(false);

            cardImage.texture = card.sprite.texture;
            if (card.cardEvent)
            {
                cardEventImage.texture = card.cardEvent.sprite.texture;
                cardEventImage.gameObject.SetActive(true);
            }
            if (card.sCardEvent)
            {
                cardSecondaryEventImage.texture = card.sCardEvent.sprite.texture;
                cardSecondaryEventImage.gameObject.SetActive(true);
            }
            if (card.tCardEvent)
            {
                cardTertiaryEventImage.texture = card.tCardEvent.sprite.texture;
                cardTertiaryEventImage.gameObject.SetActive(true);
            }

            nameText.text = card.displayName;
            manaText.text = card.manaCost.ToString();
            damageText.text = card.damage.ToString();
        }
        else
        {
            runeImage.gameObject.SetActive(true);
            runeManaText.gameObject.SetActive(true);
            runeNameText.gameObject.SetActive(true);

            runeNameText.text = card.displayName;
            runeImage.texture = card.sprite.texture;
            runeManaText.text = card.manaCost.ToString();

            manaText.gameObject.SetActive(false);
            nameText.gameObject.SetActive(false);
            damageText.gameObject.SetActive(false);
            cardImage.gameObject.SetActive(false);
        }
    }

    public void DismissCard()
    {
        card.SwitchEnabled();
        CleanPlayer();
    }

    public void PlayCard()
    {
        if(BattleManager.instance.playerMana >= cardObject.manaCost && BattleManager.instance.isPlayerTurn)
        {
            Debug.Log("Played card");
            Destroy(card.gameObject);
            BattleManager.instance.PlayCard(cardObject);
            CleanPlayer();
        }
        else if(BattleManager.instance.playerMana < cardObject.manaCost)
        {
            Debug.Log("Not enough mana");
            DismissCard();
        }
    }

    private void CleanPlayer()
    {
        cardPlayerObject.SetActive(false);
        card = null;
        cardObject = null;
    }
}
