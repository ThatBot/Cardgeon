using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
	[SerializeField] private CardObject cardObject;
    private bool cardEnabled = true;

    public void OnHover()
    {
        transform.position += Vector3.up * 20;
    }

    public void OnStoppedHover()
    {
        transform.position -= Vector3.up * 20;
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
