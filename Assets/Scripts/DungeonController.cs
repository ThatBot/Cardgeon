using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardgeon.Dungeon{

    public enum Direction
    {
        Up,
        Right,
        Left,
        Down
    }

    public class DungeonController : MonoBehaviour
	{
        public GameObject dungeonCam;
        public GameObject pixelDungeonCam;
        public GameObject depthDungeonCam;
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

        public static int DirectionToInt(Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    return 0;
                case Direction.Right:
                    return 1;
                case Direction.Down:
                    return 2;
                case Direction.Left:
                    return 3;
            }

            return 4;
        }
    }
}
