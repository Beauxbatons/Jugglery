using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using FMODUnity;

public class Menu : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject mainMenu;
    public AudioMixer audioMixer;
    public TMPro.TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    void Start()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        GameObject Event = GameObject.Find("EventSystem");
        EventSystem EventManager = Event.GetComponent<EventSystem>();
        EventManager.SetSelectedGameObject(GameObject.Find("Play"));
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void playButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void settingsButton()
    {
        GameObject Event = GameObject.Find("EventSystem");
        EventSystem EventManager = Event.GetComponent<EventSystem>();
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        EventManager.SetSelectedGameObject(GameObject.Find("Dropdown"));
    }
    public void quitButton()
    {
        Application.Quit();
    }
    public void backButton()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        GameObject Event = GameObject.Find("EventSystem");
        EventSystem EventManager = Event.GetComponent<EventSystem>();
        EventManager.SetSelectedGameObject(GameObject.Find("Play"));
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    public void SetQuality(int QualityIndex)
    {
        QualitySettings.SetQualityLevel(QualityIndex);
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void setFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
