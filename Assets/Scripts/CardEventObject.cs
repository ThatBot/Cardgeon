using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "CardgeonObjects/Event", order = 2)]
public class CardEventObject : ScriptableObject
{
	public string eventName;
	public Sprite sprite;
	public GameObject playerEffectPrefab;
	public GameObject enemyEffectPrefab;
}
