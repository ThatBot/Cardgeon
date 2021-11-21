using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardgeon.Dungeon{
	public class DungeonController : MonoBehaviour
	{
        public GameObject dungeonCam;
        public GameObject player;

        #region Singleton
        private static DungeonController _instance;
        public static DungeonController Instance { get { return _instance; } }


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
    }
}
