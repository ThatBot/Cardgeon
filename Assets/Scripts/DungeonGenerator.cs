using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardgeon.Dungeon{
	public class DungeonGenerator : MonoBehaviour
	{
		[Header("Rooms")]
		public List<GameObject> roomsUp = new List<GameObject>();
		public List<GameObject> roomsRight = new List<GameObject>();
		public List<GameObject> roomsDown = new List<GameObject>();
		public List<GameObject> roomsLeft = new List<GameObject>();
		public List<GameObject> specialRooms = new List<GameObject>();

		[Header("Generator Settings")]
		public int specialRoomChance = 25;
		public Transform roomContainer = null;

		public static DungeonGenerator Instance;

        private void Awake()
        {
			Instance = this;
        }
    }
}
