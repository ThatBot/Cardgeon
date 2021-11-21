using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardgeon.Dungeon{
	public class DungeonGenerator : MonoBehaviour
	{
		[Header("Generator Properties")]
		[SerializeField] private Vector2 gridSize = new Vector2(20, 20);
        [SerializeField] private float roomSize = 10f;
		[SerializeField] private int maxSteps = 10;
        [SerializeField] private GameObject generator = null;
        [SerializeField] private GameObject dungeonContainer = null;
        [SerializeField] private GameObject playerPrefab = null;
        [SerializeField] private GameObject testRoomPrefab = null;

        int dir = 0; //0 - Up | 1 - Right | 2 - Down | 3 - Left
        [SerializeField] private List<Vector3> generatedCells = new List<Vector3>();
        Vector2 startCell = new Vector2(0, 0);
        int elapsedSteps = 0;

        private void Start()
        {
            startCell = new Vector2((int)Random.Range(0, gridSize.x), (int)Random.Range(0, gridSize.y));
            generator.transform.position = startCell * roomSize;
        }

        private void Update()
        {
            MoveWalker();
        }

        private void MoveWalker()
        {
            if(elapsedSteps < maxSteps)
            {
                dir = Random.Range(0, 3);

                switch (dir)
                {
                    case 0:
                        if(!CheckGeneratedCell(generator.transform.position + Vector3.up * roomSize)) 
                        {
                            generator.transform.position += Vector3.up * roomSize;
                            Instantiate(testRoomPrefab, generator.transform.position, Quaternion.identity, dungeonContainer.transform);
                            generatedCells.Add(generator.transform.position);
                            elapsedSteps++;
                        }
                        break;

                    case 1:
                        if (!CheckGeneratedCell(generator.transform.position + Vector3.right * roomSize))
                        {
                            generator.transform.position += Vector3.right * roomSize;
                            Instantiate(testRoomPrefab, generator.transform.position, Quaternion.identity, dungeonContainer.transform);
                            generatedCells.Add(generator.transform.position);
                            elapsedSteps++;
                        }
                        break;

                    case 2:
                        if (!CheckGeneratedCell(generator.transform.position - Vector3.up * roomSize))
                        {
                            generator.transform.position -= Vector3.up * roomSize;
                            Instantiate(testRoomPrefab, generator.transform.position, Quaternion.identity, dungeonContainer.transform);
                            generatedCells.Add(generator.transform.position);
                            elapsedSteps++;
                        }
                        break;

                    case 3:
                        if (!CheckGeneratedCell(generator.transform.position - Vector3.right * roomSize))
                        {
                            generator.transform.position -= Vector3.right * roomSize;
                            Instantiate(testRoomPrefab, generator.transform.position, Quaternion.identity, dungeonContainer.transform);
                            generatedCells.Add(generator.transform.position);
                            elapsedSteps++;
                        }
                        break;
                }
            }
        }

        private bool CheckGeneratedCell(Vector3 cellToGenerate)
        {
            foreach (Vector3 cell in generatedCells)
            {
                if(cell == cellToGenerate)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
