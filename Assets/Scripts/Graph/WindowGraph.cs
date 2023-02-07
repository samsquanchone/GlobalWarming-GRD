using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SamSquanchLibrary.Functions;

public class WindowGraph : MonoBehaviour
{
   [SerializeField] private Sprite circleSprite;
   private RectTransform graphContainer;
   private RectTransform labelTemplateX;
   private RectTransform labelTemplateY;

   private RectTransform dashTemplateX;
   private RectTransform dashTemplateY;
   

   private List<GameObject> gameObjectList;


   private int startingYear = 2010;

   private void Awake()
   {
       //Set up references 
       graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();
       labelTemplateX = graphContainer.Find("LabelTemplateX").GetComponent<RectTransform>();
       labelTemplateY = graphContainer.Find("LabelTemplateY").GetComponent<RectTransform>();
       dashTemplateX = graphContainer.Find("DashTemplateX").GetComponent<RectTransform>();
       dashTemplateY = graphContainer.Find("DashTemplateY").GetComponent<RectTransform>();

       gameObjectList = new List<GameObject>();
       
       List<int> valueList = new List<int>() {5, 90, 80, 60, 70, 55, 43, 22, 99, 80};
       ShowGraph(valueList, (int _i) => "Year: " + (_i + 10), (float _f) => Mathf.RoundToInt(_f) + " Tons" );

       /////////TESTING PURPOSES WILL CREATE RANDOM CHART VALUES EVERY .5 OF A SECOND TO SHOW DYNAMIC GRAPH AT WORK////////////
       FunctionPeriodic.Create(() => {
            valueList.Clear();
            for(int i = 0; i < 15; i++)
            {
                valueList.Add(UnityEngine.Random.Range(0,500));
            }

            ShowGraph(valueList, (int _i) => "Year: " + (_i + 10), (float _f) => Mathf.RoundToInt(_f) + "Tons");
       }, .5f);
    }

   private GameObject CreateCircle(Vector2 anchoredPosition)
   {
      GameObject gameObject = new GameObject("circle", typeof(Image));
      gameObject.transform.SetParent(graphContainer, false);
      gameObject.GetComponent<Image>().sprite = circleSprite;
      RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
      rectTransform.anchoredPosition = anchoredPosition;
      rectTransform.sizeDelta = new Vector2(5, 5);
      rectTransform.anchorMin = new Vector2(0, 0);
      rectTransform.anchorMax = new Vector2(0, 0);

      return gameObject;
   }

   private void ShowGraph(List<int> valueList, Func<int, string> getAxisLabelX = null, Func<float, string> getAxisLabelY = null)
   {
      if(getAxisLabelX == null)
      {
        getAxisLabelX = delegate (int _i) {return _i.ToString(); }; //Default behavior delegated to set x label
      }
      if(getAxisLabelY == null)
      {
        getAxisLabelY = delegate (float _f) {return Mathf.RoundToInt(_f).ToString(); }; //Default behavior delegated to set Y label
      }
      

      //Clear previous list of values before displaying new ones
      foreach (GameObject gameObject in gameObjectList)
      {
        Destroy(gameObject);
      }

      gameObjectList.Clear();

      float graphHeight = graphContainer.sizeDelta.y;
      
      float yMaximum = valueList[0]; //Would be used for what data you want to show value for e.g. carbon captured
      float yMinimum = valueList[0]; //Setting minimum and max values based off first element of values list

      //Set Y graph values dynamically 
      foreach (int value in valueList)
      {
        if(value > yMaximum)
        {
            yMaximum = value;
        }

        if(value < yMinimum)
        {
            yMinimum = value;
        }
      }

      yMaximum = yMaximum + ((yMaximum - yMinimum) * 0.2f); //Set max size with 20 percent leway
      yMinimum = yMinimum - ((yMaximum - yMinimum) * 0.2f); //Set min size with 20 percent leway

      float xSize = 25f; //Would be used for time frame e.g. year or month

      GameObject lastCircleGameObject = null;

      //Setting up X axis e.g. time period
      for(int i = 0; i < valueList.Count; i++)
      {
        float xPosition = xSize + i * xSize;
        float yPosition = ((valueList[i] - yMinimum) / (yMaximum - yMinimum)) * graphHeight; //Nomralise value based off graph container

        //Create circle for graph
        GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
        gameObjectList.Add(circleGameObject); //Create new list of values to display

        //Check if circle has been placed previously and draw line to new point
        if(lastCircleGameObject != null)
        {
            GameObject dotConnectionGameObject = CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            gameObjectList.Add(dotConnectionGameObject);
        }

        lastCircleGameObject = circleGameObject;

        //Set up X axis labels for graph values
        RectTransform labelX = Instantiate(labelTemplateX);
        labelX.SetParent(graphContainer, false);
        labelX.gameObject.SetActive(true);
        labelX.anchoredPosition = new Vector2(xPosition, 0f);
        labelX.GetComponent<Text>().text = getAxisLabelX(i);
        gameObjectList.Add(labelX.gameObject);

        //Setting the positions for dash sprites for Y axis (Yes it is supposed to be here even tho X labels are made here)
        RectTransform dashY = Instantiate(dashTemplateY);
        dashY.SetParent(graphContainer, false);
        dashY.gameObject.SetActive(true);
        dashY.anchoredPosition = new Vector2(xPosition, 0f);
        gameObjectList.Add(dashY.gameObject);
        


      }

      int serpratorCount = 10; //The number of labels for y axis graph values
      for(int i = 0; i <= serpratorCount; i++)
      {
        //Set up Y axis labels for graph values
        RectTransform labelY = Instantiate(labelTemplateY);
        labelY.SetParent(graphContainer, false);
        labelY.gameObject.SetActive(true);
        float normalizedValue = i * 1f / serpratorCount; 
        labelY.anchoredPosition = new Vector2(-10f, (normalizedValue * graphHeight));
        labelY.GetComponent<Text>().text = getAxisLabelY(yMinimum + (normalizedValue * (yMaximum - yMinimum)));
        gameObjectList.Add(labelY.gameObject);

        //Setting the positions for dash sprites for X axis (Yes it is supposed to be here even tho Y labels are made here)
        RectTransform dashX = Instantiate(dashTemplateX);
        dashX.SetParent(graphContainer, false);
        dashX.gameObject.SetActive(true);
        dashX.anchoredPosition = new Vector2(0f, normalizedValue * graphHeight);
        gameObjectList.Add(dashX.gameObject);
      }
   }

   private GameObject CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
   {
     GameObject gameObject = new GameObject("dotConnection", typeof(Image));
     gameObject.transform.SetParent(graphContainer, false);
     gameObject.GetComponent<Image>().color = new Color(1,1,1, .5f);
     RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
     Vector2 dir = (dotPositionB - dotPositionA).normalized;
     float distance = Vector2.Distance(dotPositionA, dotPositionB);
     rectTransform.anchorMin = new Vector2(0, 0);
     rectTransform.anchorMax = new Vector2(0, 0);
     rectTransform.sizeDelta = new Vector2(distance, 3f);
     rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
     rectTransform.localEulerAngles = new Vector3(0, 0, HelperFunctions.GetAngleFromVectorFloat(dir)); //Rotate line towards direction

     return gameObject;
   }
}
