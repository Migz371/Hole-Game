using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class conditions : MonoBehaviour
{
    public int points = 0;
    public int objectsremain = 0;
    public changePos hole;
    public int score = 0;

    public float startingTime = 10f;
    float currentTime = 0f;

    public TextMeshProUGUI scoretxt;
    public TextMeshProUGUI objremaintxt;
    public TextMeshProUGUI timetxt;

    public TextMeshProUGUI scoretxtwin;
    public TextMeshProUGUI timetxtwin;
    public TextMeshProUGUI finalscorewin;

    public GameObject winUI;
    public GameObject loseUI;

    public GameObject floatingtextprefab;

    public AudioSource gameSoundEffect;
    public AudioSource pointSoundEffects;

    public MainMenu mainmenu; // call for difficulty number

    public static int diff = 0;
    private void Start()
    {
        if (diff == 0) //easy
        {
            startingTime = 400f;
        }

        if (diff == 1) //normal
        {
            startingTime = 200f;
        }

        if (diff == 2) //hard
        {
            startingTime = 130f;
        }

        currentTime = startingTime;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        scoretxt.text = "Score: " + score;
        objremaintxt.text = "Objects Remaining: " + objectsremain;
        timetxt.text = "Time Remaining: " + currentTime.ToString("0");

        scoretxtwin.text = "Score: " + score;
        timetxtwin.text = "Time Remaining: " + currentTime.ToString("0");
        finalscorewin.text = "Final Score: " + (score + currentTime).ToString("0");
            

        currentTime -= 1 * Time.deltaTime;


        if (currentTime <= 0)
        {
            gameSoundEffect.Stop();
            loseUI.SetActive(true);
            currentTime = 0;
        }



        if (objectsremain == 0)
        {
            Win();
        }




    }

    public void Win()
    {
        
        gameSoundEffect.Stop();
        winUI.SetActive(true);
        Time.timeScale = 0f;
        hole.GameIsPaused = true;
        
    }

    private void Progress()
    {
        points++;
        
        if(points % 2 == 0) // how many objects to eat to grow
        {
            StartCoroutine(hole.HoleGrow());
        }
    }




    private void OnTriggerEnter(Collider other)
    {
        pointSoundEffects.Play();
        showPoints();
        score += 10;
        objectsremain--;
        Progress();
        Destroy(other.gameObject);
    }

    void showPoints()
    {
        if (floatingtextprefab)
        {
            GameObject prefab = Instantiate(floatingtextprefab, hole.transform.position, Quaternion.identity); //spawns point when object eaten
        }
    }

}
