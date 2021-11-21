using UnityEngine;
using Cardgeon.Combat;

namespace Cardgeon.Card
{
	[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/Card", order = 1)]
	public class CardScriptableObject : ScriptableObject
	{
		[Header("Information")]
		public Sprite cardSprite;
		public string cardName;		
		public string description;
		public bool isFoil;

		[Header("Attributes")]
		public int manaCost;
		public int damageValue;	
		public bool isSpell;
		public CardEvents cardEvent;
		public float eventRandomChance;		
	}
}

