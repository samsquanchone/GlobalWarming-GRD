using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SamSquanchLibrary.Functions;

public class WindowGraph : MonoBehaviour
{
   [SerializeField] private Sprite dotSprite;
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
       //List<int> valueList = new List<int>() {5};
       List<int> valueList = new List<int>() {5, 90, 80, 60, 70, 55};
       IGraphVisual graphVisual = new LineGraphVisual(graphContainer, dotSprite, Color.green, new Color(1, 1, 1, .5f));
       ShowGraph(valueList, graphVisual, -1, (int _i) => "Year: " + (_i + 10), (float _f) => Mathf.RoundToInt(_f) + " Tons" );
      
       /////////TESTING PURPOSES WILL CREATE RANDOM CHART VALUES EVERY .5 OF A SECOND TO SHOW DYNAMIC GRAPH AT WORK////////////
       FunctionPeriodic.Create(() => {
            valueList.Clear();
            for(int i = 0; i < UnityEngine.Random.Range(5, 25); i++)
            {
                valueList.Add(UnityEngine.Random.Range(0,500));
            }

            ShowGraph(valueList, graphVisual, -1, (int _i) => "Year: " + (_i + 10), (float _f) => Mathf.RoundToInt(_f) + "Tons");
       }, .5f);  
    }


   private void ShowGraph(List<int> valueList, IGraphVisual graphVisual, int maxVisibleValueAmount = -1, Func<int, string> getAxisLabelX = null, Func<float, string> getAxisLabelY = null)
   {
      if(getAxisLabelX == null)
      {
        getAxisLabelX = delegate (int _i) {return _i.ToString(); }; //Default behavior delegated to set x label
      }
      if(getAxisLabelY == null)
      {
        getAxisLabelY = delegate (float _f) {return Mathf.RoundToInt(_f).ToString(); }; //Default behavior delegated to set Y label
      }
      

      //Set visible count to value list size if 0
      if(maxVisibleValueAmount <= 0)
      {
        maxVisibleValueAmount = valueList.Count;
      }
      //Clear previous list of values before displaying new ones
      foreach (GameObject gameObject in gameObjectList)
      {
        Destroy(gameObject);
      }

      gameObjectList.Clear();

      float graphHeight = graphContainer.sizeDelta.y;
      float graphWidth = graphContainer.sizeDelta.x;
      
      float yMaximum = valueList[0]; //Would be used for what data you want to show value for e.g. carbon captured
      float yMinimum = valueList[0]; //Setting minimum and max values based off first element of values list

      //Set Y graph values dynamically 
      for (int i = Mathf.Max(valueList.Count - maxVisibleValueAmount, 0); i < valueList.Count; i++)
      {
        int value = valueList[i];
        if(value > yMaximum)
        {
            yMaximum = value;
        }

        if(value < yMinimum)
        {
            yMinimum = value;
        }
      }

      float yDifference = yMaximum - yMinimum;
      if(yDifference <= 0)
      {
        yDifference = 5f;
      }
      yMaximum = yMaximum + (yDifference * 0.2f); //Set max size with 20 percent leway
      yMinimum = yMinimum - (yDifference * 0.2f); //Set min size with 20 percent leway

      float xSize = graphWidth / (maxVisibleValueAmount + 1); //Would be used for time frame e.g. year or month
      int xIndex = 0;
      
      //LineGraphVisual lineGraphVisual = new LineGraphVisualC
      //BarChartVisual barChartVisual = new BarChartVisual(graphContainer, Color.green, .8f);
      
      //GameObject lastdotGameObject = null;

      //Setting up X axis e.g. time period
      for(int i = Mathf.Max(valueList.Count - maxVisibleValueAmount, 0); i < valueList.Count; i++)
      {
        float xPosition = xSize + xIndex * xSize;
        float yPosition = ((valueList[i] - yMinimum) / (yMaximum - yMinimum)) * graphHeight; //Nomralise value based off graph container
          
          gameObjectList.AddRange(graphVisual.AddGraphVisual(new Vector2(xPosition, yPosition), xSize));
       // gameObjectList.AddRange(barChartVisual.AddGraphVisual(new Vector2(xPosition, yPosition), xSize));
        

        //Create circle for graph

        
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

        xIndex++;
        


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

   
private interface IGraphVisual 
   {
      List<GameObject> AddGraphVisual(Vector2 graphPosition, float graphPositionWidth);
   }

private class BarChartVisual : IGraphVisual
{
   private RectTransform graphContainer;
   private Color barColor;
   float barWidthMultiplier;

   //contstructor for bar chart class
   public BarChartVisual(RectTransform graphContainer, Color barColor, float barWidthMultiplier) 
   {
      //Set up propterties of bar chart, based on param values passed in 
      this.graphContainer = graphContainer;
      this.barColor = barColor;
      this.barWidthMultiplier = barWidthMultiplier;
   }

   public List<GameObject> AddGraphVisual(Vector2 graphPosition, float graphPositionWidth)
   {
      GameObject barGameObject = CreateBar(graphPosition, graphPositionWidth); //Set up bar positions with gap offset
      return new List<GameObject>() { barGameObject };
   }
   private GameObject CreateBar(Vector2 graphPosition, float barWidth)
   { 
      GameObject gameObject = new GameObject("bar", typeof(Image));
      gameObject.transform.SetParent(graphContainer, false);
      gameObject.GetComponent<Image>().color = barColor;
      RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
      rectTransform.anchoredPosition =new Vector2(graphPosition.x, 0f);
      rectTransform.sizeDelta = new Vector2(barWidth * barWidthMultiplier, graphPosition.y);
      rectTransform.anchorMin = new Vector2(0, 0);
      rectTransform.anchorMax = new Vector2(0, 0);
      rectTransform.pivot = new Vector2(.5f, 0f);
     
     

      return gameObject;
   }

   }
   

   private class LineGraphVisual : IGraphVisual
   {
      private RectTransform graphContainer;
      private Sprite dotSprite;
      private GameObject lastdotGameObject;
      private Color dotColor;
      private Color dotConnectionColor;
     
      //Set up required references for this class in the constructor
      public LineGraphVisual(RectTransform graphContainer, Sprite dotSprite, Color dotColor, Color dotConnectionColor)
      {
        this.graphContainer = graphContainer;
        this.dotSprite = dotSprite;
        lastdotGameObject = null;
        this.dotColor = dotColor;
        this.dotConnectionColor = dotConnectionColor;
      }

      public List<GameObject> AddGraphVisual(Vector2 graphPosition, float graphPositionWidth)
      {
        List<GameObject> gameObjectList = new List<GameObject>();
        GameObject dotGameObject = CreateDot(graphPosition);
        gameObjectList.Add(dotGameObject); //Create new list of values to display

        //Check if circle has been placed previously and draw line to new point
        if(lastdotGameObject != null)
        {
            GameObject dotConnectionGameObject = CreateDotConnection(lastdotGameObject.GetComponent<RectTransform>().anchoredPosition, dotGameObject.GetComponent<RectTransform>().anchoredPosition);
            gameObjectList.Add(dotConnectionGameObject);
        }

        lastdotGameObject = dotGameObject;
        return gameObjectList;
      }

      private GameObject CreateDot(Vector2 anchoredPosition)
      {
        GameObject gameObject = new GameObject("dot", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = dotSprite;
        gameObject.GetComponent<Image>().color = dotColor;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(5, 5);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);

        return gameObject;
      }

      private GameObject CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
      {
         GameObject gameObject = new GameObject("dotConnection", typeof(Image));
         gameObject.transform.SetParent(graphContainer, false);
         gameObject.GetComponent<Image>().color = dotConnectionColor;
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
}
