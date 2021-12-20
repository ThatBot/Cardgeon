using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugHandCardButton : MonoBehaviour
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
        toggleObject.isOn = BattleManager.instance.hand.Contains(card);
    }

    public void ToggleCard(bool toggle)
    {
        if (toggle)
        {
            BattleManager.instance.AddCardToHand(card);
            return;
        }

        BattleManager.instance.RemoveCardFromHand(card);
    }
}
