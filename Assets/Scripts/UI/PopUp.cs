using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    [SerializeField]Button ToggleButton;
    [SerializeField] TMP_Text ToggleButton_TEXT;
    [SerializeField] GameObject tg1;
    [SerializeField] GameObject tg2;
    [SerializeField] GameObject tg3;
    [SerializeField] bool button_pressed = false;
    [SerializeField] GameObject t1;
    [SerializeField] GameObject t2;
    [SerializeField] GameObject t3;
    // Start is called before the first frame update
    void Start()
    {
        ToggleButton.onClick.AddListener(onclick);
    }

    // Update is called once per frame
    void Update()
    {
        if (button_pressed)
        {
            tg1.SetActive(true);
            tg2.SetActive(true);
            tg3.SetActive(true);
            ToggleButton_TEXT.text = ">";
            t1.SetActive(true);
            t2.SetActive(true);
            t3.SetActive(true);

        }
        else
        {
            tg1.SetActive(false);
            tg2.SetActive(false);
            tg3.SetActive(false);
            ToggleButton_TEXT.text = "<";
            t1.SetActive(false);
            t2.SetActive(false);
            t3.SetActive(false);

        }
    }

    public void onclick()
    {
        if (button_pressed) { button_pressed = false; }
        else { button_pressed = true; }
    }
}
