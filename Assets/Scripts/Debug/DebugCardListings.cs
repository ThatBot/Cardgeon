using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCardListings : MonoBehaviour
{
	[SerializeField] private List<CardObject> cardObjects = new List<CardObject>();
    [SerializeField] private List<EnemyObject> enemyObjects = new List<EnemyObject>();

    [Header("Deck")]
    [SerializeField] private Transform deckContentHolder = null;
    [SerializeField] private GameObject deckPickerPrefab = null;
    private GameObject newDeckPicker = null;
    
    [Header("Hand")]
    [SerializeField] private Transform handContentHolder = null;
    [SerializeField] private GameObject handPickerPrefab = null;
    private GameObject newHandPicker = null;

    [Header("Enemies")]
    [SerializeField] private Transform enemyContentHolder = null;
    [SerializeField] private GameObject enemyPickerPrefab = null;
    private GameObject newEnemyPicker = null;

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

        for (int i = 0; i < enemyObjects.Count; i++)
        {
            newEnemyPicker = Instantiate(enemyPickerPrefab, enemyContentHolder);
            newEnemyPicker.GetComponent<DebugEnemyPicker>().enemy = enemyObjects[i];
            newEnemyPicker.GetComponent<DebugEnemyPicker>().InitializeButton();
        }
    }
}
