using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CardPlayer : MonoBehaviour
{
    [Header("Card Visuals")]
	[SerializeField] private RawImage cardImage = null;
	[SerializeField] private RawImage cardEventImage = null;
	[SerializeField] private RawImage cardSecondaryEventImage = null;
    [SerializeField] private TMP_Text nameText = null;
    [SerializeField] private TMP_Text manaText = null;
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
        cardImage.gameObject.SetActive(true);
    }

    private void SetCardVisuals(CardObject card)
    {
        cardEventImage.gameObject.SetActive(false);
        cardSecondaryEventImage.gameObject.SetActive(false);

        cardImage.texture = card.sprite.texture;
        if (card.hasEventPrimary)
        {
            cardEventImage.texture = card.cardEventSprite.texture;
            cardEventImage.gameObject.SetActive(true);
        }
        if (card.hasEventSecondary)
        {
            cardSecondaryEventImage.texture = card.secondaryCardEventSprite.texture;
            cardSecondaryEventImage.gameObject.SetActive(true);
        }
        nameText.text = card.displayName;
        manaText.text = card.manaCost.ToString();
        damageText.text = card.damage.ToString();
    }

    public void DismissCard()
    {
        card.SwitchEnabled();
        CleanPlayer();
    }

    public void PlayCard()
    {
        Debug.Log("Played card");
        Destroy(card.gameObject);
        BattleManager.instance.PlayCard(cardObject);
        CleanPlayer();
    }

    private void CleanPlayer()
    {
        cardImage.gameObject.SetActive(false);
        card = null;
        cardObject = null;
    }
}
