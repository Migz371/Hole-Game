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

    private void Start()
    {
        currentTime = startingTime;
     
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
        //countdownText.text = currentTime.ToString("0"); //put time on text

        if (currentTime <= 0)
        {
            currentTime = 0;
        }

        Debug.Log(currentTime);

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
        score += 10;
        objectsremain--;
        //Debug.Log(objectsremain);
        Progress();
        Destroy(other.gameObject);
    }
}
