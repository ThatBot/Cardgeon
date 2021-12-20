using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCardListings : MonoBehaviour
{
	[SerializeField] private List<CardObject> cardObjects = new List<CardObject>();

    [Header("Deck")]
    [SerializeField] private Transform deckContentHolder = null;
    [SerializeField] private GameObject deckPickerPrefab = null;
    private GameObject newDeckPicker = null;
    
    [Header("Hand")]
    [SerializeField] private Transform handContentHolder = null;
    [SerializeField] private GameObject handPickerPrefab = null;
    private GameObject newHandPicker = null;

    private void Start()
    {
        for (int i = 0; i < cardObjects.Count; i++)
        {
            newDeckPicker = Instantiate(deckPickerPrefab, deckContentHolder);
            newDeckPicker.GetComponent<DebugCardSelector>().card = cardObjects[i];
            newDeckPicker.GetComponent<DebugCardSelector>().InitializeToggle();

            newHandPicker = Instantiate(handPickerPrefab, handContentHolder);
            newHandPicker.GetComponent<DebugHandCardButton>().card = cardObjects[i];
            newHandPicker.GetComponent<DebugHandCardButton>().InitializeToggle();
        }
    }
}
