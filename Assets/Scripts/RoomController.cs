using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardgeon.Dungeon{
	public class RoomController : MonoBehaviour
	{
		[Header("Room Properties")]
		[SerializeField] private bool upDoorAvailable = false;
		[SerializeField] private bool rightDoorAvailable = false;
		[SerializeField] private bool downDoorAvailable = false;
		[SerializeField] private bool leftDoorAvailable = false;
		[SerializeField] private Transform[] roomSpawnSpots;
		[SerializeField] private Direction[] roomSpawnSpotsDir;

        private void Start()
        {
			int rand;

            for (int i = 0; i < roomSpawnSpots.Length; i++)
            {
				switch (roomSpawnSpotsDir[i])
                {
					case Direction.Up:
						rand = Random.Range(0, DungeonGenerator.Instance.roomsDown.Count);
						Instantiate(DungeonGenerator.Instance.roomsDown[rand], roomSpawnSpots[i].position, Quaternion.identity, DungeonGenerator.Instance.roomContainer);
						break;

					case Direction.Right:
						rand = Random.Range(0, DungeonGenerator.Instance.roomsLeft.Count);
						Instantiate(DungeonGenerator.Instance.roomsLeft[rand], roomSpawnSpots[i].position, Quaternion.identity, DungeonGenerator.Instance.roomContainer);
						break;

					case Direction.Down:
						rand = Random.Range(0, DungeonGenerator.Instance.roomsUp.Count);
						Instantiate(DungeonGenerator.Instance.roomsUp[rand], roomSpawnSpots[i].position, Quaternion.identity, DungeonGenerator.Instance.roomContainer);
						break;

					case Direction.Left:
						rand = Random.Range(0, DungeonGenerator.Instance.roomsRight.Count);
						Instantiate(DungeonGenerator.Instance.roomsRight[rand], roomSpawnSpots[i].position, Quaternion.identity, DungeonGenerator.Instance.roomContainer);
						break;
				}

            }
        }
    }
}
