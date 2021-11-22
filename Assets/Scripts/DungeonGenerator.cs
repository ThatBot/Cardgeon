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
        [SerializeField] private GameObject roomContainer = null;
        [SerializeField] private GameObject doorContainer = null;
        [SerializeField] private GameObject playerPrefab = null;
        [SerializeField] private GameObject testRoomPrefab = null;
        [SerializeField] private GameObject[] doorPrefabs;

        int dir = 0; //0 - Up | 1 - Right | 2 - Down | 3 - Left
        [SerializeField] private List<Vector3> generatedCells = new List<Vector3>();
        Vector2 startCell = new Vector2(0, 0);
        int elapsedSteps = 0;

        private void Start()
        {
            startCell = new Vector2((int)Random.Range(0, gridSize.x), (int)Random.Range(0, gridSize.y));
            generator.transform.position = startCell * roomSize;

            for (int i = 0; i < gridSize.y; i++)
            {
                for (int j = 0; j < gridSize.x; j++)
                {
                    Vector2 gridPos = new Vector2(j * roomSize, i * roomSize);
                    Instantiate(testRoomPrefab, gridPos, Quaternion.identity, roomContainer.transform);
                }
            }

            for (int i = 0; i < maxSteps; i++)
            {
                MoveWalker();
            }
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
                            Instantiate(doorPrefabs[dir], generator.transform.position, Quaternion.identity, doorContainer.transform);
                            generatedCells.Add(generator.transform.position);
                            elapsedSteps++;
                        }
                        break;

                    case 1:
                        if (!CheckGeneratedCell(generator.transform.position + Vector3.right * roomSize))
                        {
                            generator.transform.position += Vector3.right * roomSize;
                            Instantiate(doorPrefabs[dir], generator.transform.position, Quaternion.identity, doorContainer.transform);
                            generatedCells.Add(generator.transform.position);
                            elapsedSteps++;
                        }
                        break;

                    case 2:
                        if (!CheckGeneratedCell(generator.transform.position - Vector3.up * roomSize))
                        {
                            generator.transform.position -= Vector3.up * roomSize;
                            Instantiate(doorPrefabs[dir], generator.transform.position, Quaternion.identity, doorContainer.transform);
                            generatedCells.Add(generator.transform.position);
                            elapsedSteps++;
                        }
                        break;

                    case 3:
                        if (!CheckGeneratedCell(generator.transform.position - Vector3.right * roomSize))
                        {
                            generator.transform.position -= Vector3.right * roomSize;
                            Instantiate(doorPrefabs[dir], generator.transform.position, Quaternion.identity, doorContainer.transform);
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
