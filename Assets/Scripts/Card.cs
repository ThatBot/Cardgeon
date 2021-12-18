using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private float hoverDisplacement = 20f;
	[SerializeField] private CardObject cardObject = null;
    [SerializeField] private RawImage cardSprite = null;
    private bool cardEnabled = true;

    private void Awake()
    {
        cardSprite.texture = cardObject.sprite.texture;
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
