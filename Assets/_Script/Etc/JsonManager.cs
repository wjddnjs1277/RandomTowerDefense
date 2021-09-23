using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

[System.Serializable]
class DataInfo
{
    public WaveInfo[] waveData;
}

[System.Serializable]
class WaveInfo
{
    public string objName;
    public int count;
    public float spawnTime;
    public float speed;
    public float hp;
    public float defense;
}

public class JsonManager : MonoBehaviour
{
    public string filePath;
    [SerializeField] DataInfo dataInfo;

    void Start()
    {
        string streamingAssetPath = Application.streamingAssetsPath;
        string fileName = "wavedata.json";
        filePath = Path.Combine(streamingAssetPath, fileName);

        string jsonString = File.ReadAllText(filePath);

        dataInfo = JsonUtility.FromJson<DataInfo>(jsonString);
        Debug.Log(jsonString);

        //foreach (var Wave in dataInfo.waveData)
        //{
        //    GameObject prefab = Resources.Load<GameObject>(Wave.objName);

        //    Vector3 position = new Vector3(Wave.x, Wave.y, Wave.z);
        //    GameObject clone = Instantiate(prefab, position, Quaternion.identity);
        //    clone.name = Wave.name;
        //}

    }
}