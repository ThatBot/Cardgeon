using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("Cosmetic")]
    [SerializeField] private TMP_Text versionText = null;
    [SerializeField] private Transform cogRight = null;
    [SerializeField] private Transform cogLeft = null;
    [SerializeField] private float cogSpeed = 10f;

    [Header("Settings")]
    [SerializeField] private GameObject mainMenuObject = null;
    [SerializeField] private GameObject settingsMenu = null;

    [Header("Audio Settings")]
    [SerializeField] private GameObject audioSettingsMenu = null;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private string masterVolumeKey = "_masterVolume";
    [SerializeField] private string musicVolumeKey = "_musicVolume";
    [SerializeField] private string sfxVolumeKey = "_sfxVolume";

	public void Play()
    {
        GameManager.instance.LoadScene(SceneIndexes.MENU, SceneIndexes.DUNGEON);
    }

    private void Start()
    {
        versionText.text = $"Version: {Application.version}";
    }

    private void SetUpSettings()
    {

    }

    private void Update()
    {
        cogRight.Rotate(Vector3.forward * cogSpeed * Time.deltaTime);
        cogLeft.Rotate(Vector3.forward * -cogSpeed * Time.deltaTime);
    }

    public void Exit()
    {
        Application.Quit();
    }

    #region Settings

    public void OpenSettings()
    {
        mainMenuObject.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void BackToMenu()
    {
        settingsMenu.SetActive(false);
        mainMenuObject.SetActive(true);
    }

    #region Audio

    public void OpenAudio()
    {
        settingsMenu.SetActive(false);
        audioSettingsMenu.SetActive(true);
    }

    public void SetMasterVolume(float vol)
    {
        mixer.SetFloat(masterVolumeKey, vol);
    }
    
    public void SetMusicVolume(float vol)
    {
        mixer.SetFloat(musicVolumeKey, vol);
    }
    
    public void SetSFXVolume(float vol)
    {
        mixer.SetFloat(sfxVolumeKey, vol);
    }

    public void BackAudio()
    {
        settingsMenu.SetActive(true);
        audioSettingsMenu.SetActive(false);
    }
    #endregion

    #region Video
    #endregion

    #region Graphics
    #endregion

    #endregion
}
