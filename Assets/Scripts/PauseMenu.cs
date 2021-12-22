using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
	[SerializeField] private GameObject pauseMenu = null;
    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;

	public void BackToMenu()
    {
        GameManager.instance.LoadScene(SceneIndexes.DUNGEON, SceneIndexes.MENU);
    }

    public void BackToGame()
    {
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        }
    }
}
