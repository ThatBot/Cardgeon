using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    [SerializeField] private float hoverDisplacement = 20f;
	public CardObject cardObject = null;  
    public bool cardEnabled = true;

    [Header("Visuals")]
    [SerializeField] private RawImage runeSprite = null;
    [SerializeField] private RawImage cardSprite = null;
    [SerializeField] private RawImage event1Sprite = null;
    [SerializeField] private RawImage event2Sprite = null;
    [SerializeField] private RawImage event3Sprite = null;
    [SerializeField] private TMP_Text nameText = null;
    [SerializeField] private TMP_Text runeNameText = null;
    [SerializeField] private TMP_Text manaText = null;
    [SerializeField] private TMP_Text runeManaText = null;
    [SerializeField] private TMP_Text damageText = null;

    private void Awake()
    {
        InitializeCard();
    }

    public void InitializeCard()
    {
        if (!cardObject.isRune)
        {
            cardSprite.texture = cardObject.sprite.texture;
            runeSprite.gameObject.SetActive(false);

            if (cardObject.hasEventPrimary)
            {
                event1Sprite.texture = cardObject.cardEventSprite.texture;
                event1Sprite.gameObject.SetActive(true);
            }

            if (cardObject.hasEventSecondary)
            {
                event2Sprite.texture = cardObject.secondaryCardEventSprite.texture;
                event2Sprite.gameObject.SetActive(true);
            }

            if (cardObject.hasEventTertiary)
            {
                event3Sprite.texture = cardObject.tertiaryCardEventSprite.texture;
                event3Sprite.gameObject.SetActive(true);
            }

            nameText.text = cardObject.displayName.ToString();
            manaText.text = cardObject.manaCost.ToString();
            damageText.text = cardObject.damage.ToString();
            runeManaText.gameObject.SetActive(false);
            runeNameText.gameObject.SetActive(false);
        }
        else
        {
            runeSprite.texture = cardObject.sprite.texture;
            cardSprite.gameObject.SetActive(false);
            runeNameText.text = cardObject.displayName.ToString();
            runeManaText.text = cardObject.manaCost.ToString();
            nameText.gameObject.SetActive(false);
            manaText.gameObject.SetActive(false);
            damageText.gameObject.SetActive(false);
        }
    }

    public void OnHover()
    {
        transform.position += Vector3.up * hoverDisplacement;
    }

    public void OnStoppedHover()
    {
        transform.position -= Vector3.up * hoverDisplacement;
    }

    public void Play()
    {
        CardPlayer.instance.SetCard(cardObject, this);
    }

    public void SwitchEnabled()
    {
        if (cardEnabled)
        {
            cardEnabled = false;
            gameObject.SetActive(false);
            return;
        }

        cardEnabled = true;
        gameObject.SetActive(true);
    }
}
