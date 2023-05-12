using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//Sam addition: creating enum so i can determine what mode time is in when i spawn nav agents
public enum TimeModes{NORMAL, PAUSE, FASTFORWARD};
public class Date_and_Time_System : MonoBehaviour
{
    public static Date_and_Time_System instance => m_instance;
    private static Date_and_Time_System m_instance;
     
    private TimeModes timeMode; //Sams enum to check state

    [Header("Time Data")]
     public Time_Data TimeData;
    [Header("Starting Date")]
     public TMP_Text Date_Text;
     public int Year = 2023;
     public int Month = 1;

    [NonSerialized] public float Unchecked_Game_Timer = 0.0f; //Total time spend in the game.
    //Game time in secounds, first of his name, protector of the time and time networks. All calculations are made according to this timeframe.
    [NonSerialized] public float Game_Secounds = 0f; //Can be different from normal secound speed becouse of timer changes.

    //Here to calculate if secounds increased.
    [NonSerialized] private float _Old_Secounds = 0f;

    //Time management -> Is changed by the buttons: Stop, Normal Speed, Fast Speed
    public bool Stop_Time = false;
    public bool Normal_Time_Speed = true;
    public bool Fast_Time_Speed = false;

    [Header("Button Allocations for time management")]
    public Button Stop_BUTTON;
    public Button Normal_Speed_BUTTON;
    public Button Fast_Speed_BUTTON;

    [Header("Stopped Indication Canvas")]
    public Canvas Stop_Indication_Canvas;

    bool firstLoad = false; //Sam addition: pause time audio triggering on start, quick implementation to solve issue

    //Sam addition: Going to create events to subscribe to so i can control my boat nav agent speed :)
    public UnityEvent PauseEvent;
    public UnityEvent PlayEvent;
    public UnityEvent FastforwardEvent;

    public void Save() //Sam: part of my saving overhaul
    {
        SaveTimeData timeData = new SaveTimeData();
        timeData.year = this.Year;
        timeData.month = this.Month;

        JSONManager.SaveTimeJSON(timeData, "TimeData");
    }
    public void Load()
    {
        SaveTimeData timeData = JSONManager.LoadTimeData("TimeData");
        this.Year = timeData.year;
        this.Month = timeData.month;
    }
    void Awake()
    {
       m_instance = this;

       //Sam: Checking with my static class if new game or not
        if(!MenuData.GetGameType())
        {
            Load();
            this.Date_Text.text =  Year.ToString();
        }

        else
        {
            //Sam: Default start vals
            this.Year = TimeData.Year;
            this.Month = TimeData.Month;
            this.Date_Text.text =  "January " + Year.ToString();
            
        }
    }

    void Start()
    {
        
        
        //Button Listeners
        Stop_BUTTON.onClick.AddListener(Stop_Speed);
        Normal_Speed_BUTTON.onClick.AddListener(Normal_Speed);
        Fast_Speed_BUTTON.onClick.AddListener(Fast_Speed);


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
        

        //Sam: this should really be in player input, with check is playing to avoid having input stuff in a non input script.....
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
    
    //Used to get initial time mode when a nav agent spawns
    public TimeModes GetTimeMode()
    {
        TimeModes _timeMode = timeMode;
        
        return  _timeMode;
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
        
        timeMode = TimeModes.PAUSE;

        if(firstLoad)
        {
           AudioPlayback.PlayOneShot(AudioManager.instance.uiRefs.pauseTimeSelected, null);
        }

        PauseEvent.Invoke(); 
 

        firstLoad = true;
        
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

        timeMode = TimeModes.NORMAL;
        PlayEvent.Invoke();
        AudioPlayback.PlayOneShot(AudioManager.instance.uiRefs.startTimeSelected, null);
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
        timeMode = TimeModes.FASTFORWARD;
        FastforwardEvent.Invoke();
        AudioPlayback.PlayOneShot(AudioManager.instance.uiRefs.fastForwardTimeSelected, null);
    }

    #endregion

    public UnityEvent Month_Pass_Event;
    public void On_Month_Pass()
    {
        //Calculate Month (Cant be bigger than 12
        Month++;
        TimeManager.instance.UpdateTreeGrowth();

        Month_Pass_Event.Invoke();

        if (Month > 12) {TimeManager.instance.YearPassed(); Month = 1; /*Increase Year */ Year++; }
        if( Year >= 2100) { GameManager.instance.GameLost();  }

        
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
    
    //Sam memory management: you have an event system with listeners but these listeners are not unsubscribed from the event system, handling this on destory to avoid memory leaks 
    void OnDestroy()
    {
        Stop_BUTTON.onClick.RemoveListener(Stop_Speed);
        Normal_Speed_BUTTON.onClick.RemoveListener(Normal_Speed);
        Fast_Speed_BUTTON.onClick.RemoveListener(Fast_Speed);
    }
}

[System.Serializable]
public class SaveTimeData
{
    public int year;
    public int month;
}
