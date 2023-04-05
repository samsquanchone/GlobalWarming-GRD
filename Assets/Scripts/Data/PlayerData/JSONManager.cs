using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public static class JSONManager
{
   public static string directory = "/SaveData/";
   public static string fileExt = ".txt";

   public static void CreateDirectorys()
   {
       string dir = Application.persistentDataPath + directory;
       Directory.CreateDirectory(dir);
   }

   public static void SaveJSON(SaveGraphData obj, string fileName)
   {
       string dir = Application.persistentDataPath + directory;
       string file = fileName + fileExt;

       if(Directory.Exists(dir))
       {
           

           string json = JsonUtility.ToJson(obj);
           File.WriteAllText(dir + file, json);
       }
   }

   public static SaveGraphData Load(string fileName)
   {
        string file = fileName + fileExt;
        string fullPath = Application.persistentDataPath + directory + file;
        SaveGraphData so = new SaveGraphData();

        if(File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            so = JsonUtility.FromJson<SaveGraphData>(json);
        }

        else 
        {
            Debug.Log("Save file does not exist");
        }

        return so;

   }

}
