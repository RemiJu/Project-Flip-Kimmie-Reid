using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public static class SaveSystem
{
    public static void SavePhotos (List<PhotoInfo> photos)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/photos.pdata";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, photos);
        stream.Close();
    }

    public static List<PhotoInfo> LoadPhotos()
    {
        string path = Application.persistentDataPath + "/photos.pdata";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream (path, FileMode.Open);

            List<PhotoInfo> data = formatter.Deserialize(stream) as List<PhotoInfo>;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
