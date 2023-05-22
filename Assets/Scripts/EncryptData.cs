using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class EncryptData
{
    public static void EncryptScore(Sparks sparks)
    {
        BinaryFormatter encryptMethod = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.dat";
        Debug.Log("Created file at " + path);
        FileStream dataLocation = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(sparks);

        encryptMethod.Serialize(dataLocation, data);
        dataLocation.Close();
    }

    public static PlayerData LoadData(Sparks sparks)
    {
        string path = Application.persistentDataPath + "/player.dat";
        if(File.Exists(path)){
            BinaryFormatter encryptMethod = new BinaryFormatter();
            FileStream dataLocation = new FileStream(path, FileMode.Open);
            PlayerData data = encryptMethod.Deserialize(dataLocation) as PlayerData;
            dataLocation.Close();
            return data;
        } else {
            Debug.LogError("Folder: " + path + "not found");
            return null;
        }
    }
}
