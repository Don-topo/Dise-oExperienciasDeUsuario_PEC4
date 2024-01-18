using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class FileManager
{
    private static readonly string gameInfoPath = Application.persistentDataPath + "/GameInfo.txt";

    public static GameInfo LoadGameConfig()
    {
        if (CheckIfExistSavedData())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(gameInfoPath, FileMode.Open);
            GameInfo gameInfo = formatter.Deserialize(stream) as GameInfo;
            if (gameInfo == null) gameInfo = new GameInfo(100f, 100f, 1, 100f, true, true, 18, 80f);
            stream.Close();
            return gameInfo;
        }
        else
        {
            return new GameInfo(100f, 100f, 1, 100f, true, true, 18, 80f);
        }
    }

    public static void SaveGameInfo(GameInfo gameInfo)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(gameInfoPath, FileMode.Create);
        formatter.Serialize(stream, gameInfo);
        stream.Close();
    }

    public static bool CheckIfExistSavedData()
    {
        return File.Exists(gameInfoPath);    
    }
}
