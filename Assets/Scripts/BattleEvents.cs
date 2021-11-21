using Cardgeon.Base;
using UnityEngine;

namespace Cardgeon.Combat{
	public enum CardEvents
	{
		None,
		Lightning,
		StrongLightning,
		Combustion,
		AddCardToHand
	};

	public class BattleEvents
	{
		public void ExecuteEvent(CardEvents eventIdentificator)
        {
			if(eventIdentificator == CardEvents.None)
            {
				Debug.LogWarning("Called execute event on null event!"); // Warning
				return;
            }

            switch (eventIdentificator)
            {
				case CardEvents.Lightning:
                    LightningEvent();
					break;

				case CardEvents.StrongLightning:
					StrongLightningEvent();
					break;

				case CardEvents.Combustion:
					CombustionEvent();
					break;

				case CardEvents.AddCardToHand:
					AddCardEvent();
					break;
			}
        }

		private void LightningEvent()
        {
			Debug.Log("Called the lightning event!");
			BattleManager.Instance.HurtEnemy(10);
        }

		private void StrongLightningEvent()
		{
			Debug.Log("Called the strong lightning event!");
			BattleManager.Instance.HurtEnemy(20);
		}

		private void CombustionEvent()
		{
			Debug.Log("Called the combustion event!");
		}

		private void AddCardEvent()
        {
			CardController.Instance.AddCardToHand(2);
        }
	}
}
