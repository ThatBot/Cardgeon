using Cardgeon.Base;
using UnityEngine;

namespace Cardgeon.Combat{
	public class BattleManager : MonoBehaviour
	{
        public Enemy currentEnemy;
        public GameObject battleCam;
        public BattleEvents battleEvents = new BattleEvents();

        #region Singleton
        private static BattleManager _instance;
        public static BattleManager Instance { get { return _instance; } }


        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }
        #endregion

        public void Start()
        {
            for (int i = 0; i < 4; i++)
            {
                CardController.Instance.AddCardToHand();
            }  
        }

        public void OnCombatBegin()
        {

        }

        public void HurtEnemy(int damage)
        {
            currentEnemy.Hurt(damage);
        }

        public void EnemyDied()
        {
            Debug.Log("Enemy died");
        }
    }
}
