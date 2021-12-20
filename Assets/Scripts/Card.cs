using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    [SerializeField] private float hoverDisplacement = 20f;
	public CardObject cardObject = null;  
    private bool cardEnabled = true;

    [Header("Visuals")]
    [SerializeField] private RawImage cardSprite = null;
    [SerializeField] private RawImage event1Sprite = null;
    [SerializeField] private RawImage event2Sprite = null;
    [SerializeField] private TMP_Text nameText = null;
    [SerializeField] private TMP_Text manaText = null;
    [SerializeField] private TMP_Text damageText = null;

    private void Awake()
    {
        InitializeCard();
    }

    public void InitializeCard()
    {
        cardSprite.texture = cardObject.sprite.texture;
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

        nameText.text = cardObject.displayName;
        manaText.text = cardObject.manaCost.ToString();
        damageText.text = (cardObject.isSpell) ? "--" : cardObject.damage.ToString();
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
