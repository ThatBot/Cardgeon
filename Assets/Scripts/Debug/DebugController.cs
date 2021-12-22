using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugController : MonoBehaviour
{
    [Header("Debug Menu")]
    [SerializeField] private GameObject debugOpenerButton = null;
    [SerializeField] private KeyCode debugKey = KeyCode.KeypadEnter;
    [SerializeField] private GameObject debugMenu = null;
    [SerializeField] private GameObject gameplayUI = null;

    [SerializeField] private Transform deckCardPickerHolder = null;
    [SerializeField] private Transform handCardPickerHolder = null;
    [SerializeField] private TMP_Text handSlotsLeft = null;

    [SerializeField] private TMP_Text enemyDisplayText = null;

	public static DebugController instance;
    private void Awake()
    {
        instance = this;
        if(Application.platform == RuntimePlatform.Android)
        {
            debugOpenerButton.SetActive(true);
        }

        BattleManager.instance.onEnemyInitialized += ChangeEnemyDisplayed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(debugKey))
        {
            OpenDebugInterface();
        }

        if (debugMenu.activeInHierarchy)
        {
            handSlotsLeft.text = $"Remaining free slots: {BattleManager.instance.maxFreeHandSlots.ToString()}";
        }
    }

    public void OpenDebugInterface()
    {
        gameplayUI.SetActive(false);
        debugMenu.SetActive(true);

        for (int i = 0; i < deckCardPickerHolder.childCount; i++)
        {
            deckCardPickerHolder.GetChild(i).GetComponent<DebugCardSelector>().InitializeToggle();
        }

        for (int i = 0; i < handCardPickerHolder.childCount; i++)
        {
            handCardPickerHolder.GetChild(i).GetComponent<DebugHandCardButton>().InitializeToggle();
        }
    }

    public void CloseDebugInterface()
    {
        gameplayUI.SetActive(true);
        debugMenu.SetActive(false);
    }

    public void ChangeDeckCapacity(int capacity)
    {
        RunStats.instance.deckCapacity = capacity;
    }

    public void ChangeEnemyDisplayed()
    {
        enemyDisplayText.text = $"Current enemy: {BattleManager.instance.enemyObject.displayName}";
    }

}
