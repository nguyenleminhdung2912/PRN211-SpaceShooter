using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeUI;

    float startTime;
    private float elapsedTime = 0f;
    bool startCounter;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    startCounter = false;       
    //}


    public void StartTimeCounter()
    {
        startTime = Time.time;
        startCounter = true;    
    }

    public void StopTimeCounter()
    {
        startCounter = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (startCounter)
        {
            elapsedTime += Time.deltaTime;

            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);

            // Update the TextMeshProUGUI element
            timeUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
