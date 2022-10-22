using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;
public class MainMenu : MonoBehaviour
{
    public AudioSource buttonSoundEffect;
    public AudioMixer audioMixer;

    public TextMeshProUGUI difficulty;
    public int diff = 0;


    private void Start()
    {
        Time.timeScale = 1f;
    }
    public void playSound()
    {
        buttonSoundEffect.Play();
    }
    public void playGame()
    {
        SceneManager.LoadScene(1);
    }

    public void playGametitle()
    {
        conditions.diff = diff;
        SceneManager.LoadScene(1);
    }

    public void backtoMenu()
    {
        SceneManager.LoadScene(0);
        
    }

    public void quit()
    {
        Application.Quit();
    }

    public void SetVolume(float volume) //master volume
    {
        audioMixer.SetFloat("mvolume", volume);
    }

    public void DifficultyData(int val)
    {
        if (val == 0)
        {
            diff = 0;
            
        }

        if (val == 1)
        {
            diff = 1;
        }

        if (val == 2)
        {
            diff = 2;
        }
    }







}
