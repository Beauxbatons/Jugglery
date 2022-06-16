using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class pauseMenu : MonoBehaviour
{
    public bool isPaused;
    public bool pause;
    public GameObject settingsMenuCanvas;
    public GameObject pauseMenuCanvas;
    public AudioMixer audioMixer;
    int i = 0;
    void Update()
    {
        GameObject Event = GameObject.Find("EventSystem");
        EventSystem EventManager = Event.GetComponent<EventSystem>();
        if (isPaused)
        {
            pauseMenuCanvas.SetActive(true);
            Time.timeScale = 0f;
            pause = true;
            if(i == 0)
            {
                EventManager.SetSelectedGameObject(GameObject.Find("resume"));
                i++;
            }
        }
        else
        {
            if(pause == true)
            {
                pauseMenuCanvas.SetActive(false);
                //settingsMenuCanvas.SetActive(false);
                Time.timeScale = 1f;
                if(i == 1)
                {
                    i--;
                }
            }
        }
        if(Input.GetButtonDown("Pause"))
        {
            isPaused = !isPaused;
        }
    }
    public void Resume()
    {
        isPaused = false;
    }
    public void Restart()
    {
        isPaused = !isPaused;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
    public void Settings()
    {
        pauseMenuCanvas.SetActive(false);
        settingsMenuCanvas.SetActive(true);
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
