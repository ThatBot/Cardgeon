using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
	public void Play()
    {
        GameManager.instance.LoadGame();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
