using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugCardSelector : MonoBehaviour
{
	public CardObject card = null;
    [SerializeField] private Text nameText = null;
    [SerializeField] private Toggle toggleObject = null;

    private void Start()
    {
        InitializeToggle();
    }

    public void InitializeToggle()
    {
        nameText.text = card.displayName;
        toggleObject.isOn = RunStats.instance.cardsInDeck.Contains(card);
    }

    public void ToggleCard(bool toggle)
    {
        if (toggle)
        {
            RunStats.instance.AddCardToDeck(card);
            return;
        }

        RunStats.instance.RemoveCardFromDeck(card);
    }
}
