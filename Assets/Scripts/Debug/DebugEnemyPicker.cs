using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugEnemyPicker : MonoBehaviour
{
	public EnemyObject enemy = null;
	[SerializeField] private TMP_Text nameText = null;

    private void Start()
    {
        InitializeButton();
    }

    public void InitializeButton()
    {
        nameText.text = enemy.displayName;
    }

    public void OnClick()
    {
        BattleManager.instance.InitializeEnemy(enemy);
    }
}
