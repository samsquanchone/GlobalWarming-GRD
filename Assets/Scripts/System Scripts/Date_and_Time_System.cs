using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Date_and_Time_System : MonoBehaviour
{
    [Header("Time Data")]
    [SerializeField] public Time_Data TimeData;
    [Header("Starting Date")]
    [SerializeField] public TMP_Text Date_Text;
    [SerializeField] public int Year = 2023;
    [SerializeField] public int Month = 1;

    [NonSerialized] public float Unchecked_Game_Timer = 0.0f; //Total time spend in the game.
    //Game time in secounds, first of his name, protector of the time and time networks. All calculations are made according to this timeframe.
    [NonSerialized] public float Game_Secounds = 0f; //Can be different from normal secound speed becouse of timer changes.

    //Here to calculate if secounds increased.
    [NonSerialized] private float _Old_Secounds = 0f;

    //Time management -> Is changed by the buttons: Stop, Normal Speed, Fast Speed
    [SerializeField] public bool Stop_Time = false;
    [SerializeField] public bool Normal_Time_Speed = true;
    [SerializeField] public bool Fast_Time_Speed = false;

    [Header("Button Allocations for time management")]
    [SerializeField] public Button Stop_BUTTON;
    [SerializeField] public Button Normal_Speed_BUTTON;
    [SerializeField] public Button Fast_Speed_BUTTON;

    [Header("Stopped Indication Canvas")]
    [SerializeField] public Canvas Stop_Indication_Canvas;

    void Start()
    {
        //Button Listeners
        Stop_BUTTON.onClick.AddListener(Stop_Speed);
        Normal_Speed_BUTTON.onClick.AddListener(Normal_Speed);
        Fast_Speed_BUTTON.onClick.AddListener(Fast_Speed);

        //
        this.Year = TimeData.Year;
        this.Month = TimeData.Month;

        //Start game with a stopped time.
        Stop_Speed();
    }

    // Update is called once per frame
    void Update()
    {
        //Timer Calculations according to the selected time pass type -> Stop, Normal, Fast
        if (!Stop_Time)
        {
            if (Normal_Time_Speed)
            {
                Unchecked_Game_Timer += Time.deltaTime / 2;
            }
            else if (Fast_Time_Speed) {
                Unchecked_Game_Timer += Time.deltaTime;
            }
        }

        //Old Secounds
        _Old_Secounds = Game_Secounds;

        //Calculates Secounds Passed
        Game_Secounds = (int)Unchecked_Game_Timer % 60;


        //Checks if month passes (which equals a game turn pass)
        if(_Old_Secounds < Game_Secounds)
        {
            _Old_Secounds++;

            //Turn Passes
            On_Month_Pass();

            //EVENT Month Passes
        }

        //Debug for cheking the time passed.
        //Debug.Log("Secounds Passed: " + Secounds);

        //If space button pressed, stop time.
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Space Button Pressed");
            if (Stop_Time) //Continue Time (NORMAL)
            {
                Normal_Speed();
            }
            else           //Stop Time
            {
                Stop_Speed();
            }
        }
    }

    #region Button Functions
    public void Stop_Speed()
    {
        Stop_Time = true;
        Normal_Time_Speed = false;
        Fast_Time_Speed = false;
        Stop_Indication_Canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        Stop_BUTTON.enabled = false;
        Normal_Speed_BUTTON.enabled = true;
        Fast_Speed_BUTTON.enabled = true;
    }


    public void Normal_Speed()
    {
        Stop_Time = false;
        Normal_Time_Speed = true;
        Fast_Time_Speed = false;
        Stop_Indication_Canvas.renderMode = RenderMode.WorldSpace;

        Stop_BUTTON.enabled = true;
        Normal_Speed_BUTTON.enabled = false;
        Fast_Speed_BUTTON.enabled = true;
    }

    public void Fast_Speed()
    {
        Stop_Time = false;
        Normal_Time_Speed = false;
        Fast_Time_Speed = true;
        Stop_Indication_Canvas.renderMode = RenderMode.WorldSpace;

        Stop_BUTTON.enabled = true;
        Normal_Speed_BUTTON.enabled = true;
        Fast_Speed_BUTTON.enabled = false;
    }

    #endregion


    public void On_Month_Pass()
    {
        //Calculate Month (Cant be bigger than 12
        Month++;
        TimeManager.instance.UpdateTreeGrowth();


        if (Month > 12) { Month = 1; /*Increase Year */ Year++; }
        


        switch (Month)
        {
            case 1:
                //Write new year to the UI
                this.Date_Text.text =  "January " + Year.ToString();
                break;
            case 2:
                //Write new year to the UI
                this.Date_Text.text = "February " + Year.ToString();
                break;
            case 3:
                //Write new year to the UI
                this.Date_Text.text = "March " + Year.ToString();
                break;
            case 4:
                //Write new year to the UI
                this.Date_Text.text = "April " + Year.ToString();
                break;
            case 5:
                //Write new year to the UI
                this.Date_Text.text = "May " + Year.ToString();
                break;
            case 6:
                //Write new year to the UI
                this.Date_Text.text = "June" + Year.ToString();
                break;
            case 7:
                //Write new year to the UI
                this.Date_Text.text = "July " + Year.ToString();
                break;
            case 8:
                //Write new year to the UI
                this.Date_Text.text = "August " + Year.ToString();
                break;
            case 9:
                //Write new year to the UI
                this.Date_Text.text = "September " + Year.ToString();
                break;
            case 10:
                //Write new year to the UI
                this.Date_Text.text = "October " + Year.ToString();
                break;
            case 11:
                //Write new year to the UI
                this.Date_Text.text = "November " + Year.ToString();
                break;
            case 12:
                //Write new year to the UI
                this.Date_Text.text = "December " + Year.ToString();
                break;
        }


    }
}
