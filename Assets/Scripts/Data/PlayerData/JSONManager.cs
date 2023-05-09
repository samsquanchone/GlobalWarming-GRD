using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//This class is part of sams saving overhaul, utilising data for more efficent loading 
public static class JSONManager
{
   public static string directory = "/SaveData/";
   public static string fileExt = ".txt";

   public static void CreateDirectorys()
   {
       string dir = Application.persistentDataPath + directory;
       Directory.CreateDirectory(dir);
   }

   public static void SaveGraphJSON(SaveGraphData obj, string fileName)
   {
       string dir = Application.persistentDataPath + directory;
       string file = fileName + fileExt;

       if(Directory.Exists(dir))
       {
           

           string json = JsonUtility.ToJson(obj);
           File.WriteAllText(dir + file, json);
       }
   }

   public static SaveGraphData LoadGraphData(string fileName)
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

   public static void SavePlayerJSON(SavePlayerData obj, string fileName)
   {
       string dir = Application.persistentDataPath + directory;
       string file = fileName + fileExt;

       if(Directory.Exists(dir))
       {
           string json = JsonUtility.ToJson(obj);
           File.WriteAllText(dir + file, json);
       }
   }

   public static SavePlayerData LoadPlayerData(string fileName)
   {
        string file = fileName + fileExt;
        string fullPath = Application.persistentDataPath + directory + file;
        SavePlayerData so = new SavePlayerData();

        if(File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            so = JsonUtility.FromJson<SavePlayerData>(json);
        }

        else 
        {
            Debug.Log("Save file does not exist");
        }

        return so;

   }

   public static void SaveNationJSON(SaveNationData obj, string fileName)
   {
       string dir = Application.persistentDataPath + directory;
       string file = fileName + "NationData" + fileExt;

       if(Directory.Exists(dir))
       {
           string json = JsonUtility.ToJson(obj);
           File.WriteAllText(dir + file, json);
       }
   }

   public static SaveNationData LoadNationData(string fileName)
   {
        string file = fileName + "NationData" + fileExt;
        string fullPath = Application.persistentDataPath + directory + file;
        SaveNationData so = new SaveNationData();

        if(File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            so = JsonUtility.FromJson<SaveNationData>(json);
        }

        else 
        {
            Debug.Log("Save file does not exist");
        }

        return so;

   }

   public static void SaveTileJSON(SaveTileData obj, string fileName)
   {
       string dir = Application.persistentDataPath + directory;
       string file = fileName + "TileData" + fileExt;

       if(Directory.Exists(dir))
       {
           string json = JsonUtility.ToJson(obj);
           File.WriteAllText(dir + file, json);
       }
   }

   public static SaveTileData LoadTileData(string fileName)
   {
        string file = fileName + "TileData" + fileExt;
        string fullPath = Application.persistentDataPath + directory + file;
        SaveTileData so = new SaveTileData();

        if(File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            so = JsonUtility.FromJson<SaveTileData>(json);
        }

        else 
        {
            Debug.Log("Save file does not exist");
        }

        return so;

   }

    public static void SaveTimeJSON(SaveTimeData obj, string fileName)
   {
       string dir = Application.persistentDataPath + directory;
       string file = fileName +  fileExt;

       if(Directory.Exists(dir))
       {
           string json = JsonUtility.ToJson(obj);
           File.WriteAllText(dir + file, json);
       }
   }

   public static SaveTimeData LoadTimeData(string fileName)
   {
        string file = fileName  + fileExt;
        string fullPath = Application.persistentDataPath + directory + file;
        SaveTimeData so = new SaveTimeData();

        if(File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            so = JsonUtility.FromJson<SaveTimeData>(json);
        }

        else 
        {
            Debug.Log("Save file does not exist");
        }

        return so;

   }

   public static void SaveNavJSON(SaveableNavData obj, string fileName)
   {
       string dir = Application.persistentDataPath + directory;
       string file = fileName +  fileExt;

       if(Directory.Exists(dir))
       {
           string json = JsonUtility.ToJson(obj);
           File.WriteAllText(dir + file, json);
       }
   }

   public static SaveableNavData LoadNavData(string fileName)
   {
        string file = fileName  + fileExt;
        string fullPath = Application.persistentDataPath + directory + file;
        SaveableNavData so = new SaveableNavData();

        if(File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            so = JsonUtility.FromJson<SaveableNavData>(json);
        }

        else 
        {
            Debug.Log("Save file does not exist");
        }

        return so;

   }

   

}
