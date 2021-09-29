using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

// JavaScript Object Notation
// 다목적 마크업 언어로 태그 등을 이용해 데이터의 구조를 기술한다.
// 주로 HTML에서 사용하며 데이터에 의미를 부여하는 meta데이터를 선언할 수 있다.
public static class JsonManager
{
    [System.Serializable]
    private class ConvertList<T>
    {
        public List<T> list;

        public ConvertList(List<T> list)
        {
            this.list = list;
        }
    }

    // ================= SAVE ==============================================================

    public static void SaveJsonList<T>(string fileName, List<T> list)
    {
        SaveJson(Application.dataPath, fileName, list);
    }
    public static void SaveJsonList<T>(string defaultPath, string fileName, List<T> list)
    {
        ConvertList<T> covert = new ConvertList<T>(list);
        SaveJson(defaultPath, fileName, covert);
    }

    public static void SaveJson<T>(string fileName, T obj)
    {
        SaveJson(Application.dataPath, fileName, obj);
    }
    public static void SaveJson<T>(string defaultPath, string fileName, T obj)
    {
        // obj를 Json파일로 변환
        string jsonData = JsonUtility.ToJson(obj, true);
        string path = string.Concat(defaultPath, "/", fileName + ".json");

        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            byte[] data = Encoding.UTF8.GetBytes(jsonData);
            fs.Write(data, 0, data.Length);
        }
    }

    // ================== LOAD ==============================================================

    public static T LoadJson<T>(string fileName)
    {
        return LoadJson<T>(Application.dataPath, fileName);
    }
    public static T LoadJson<T>(string defaultPath, string fileName)
    {
        string jsonData = string.Empty;                                     //string 변수생성
        string path = string.Concat(defaultPath, "/", fileName + ".json");

        using (FileStream stream = new FileStream(path, FileMode.Open))
        {
            byte[] data = new byte[stream.Length];          // 읽어들인 파일의 길이
            stream.Read(data, 0, data.Length);              // 파일을 0의 위치에서 length만큼 읽어 data에 대입

            jsonData = Encoding.UTF8.GetString(data);        // byte[]을 UTF-8(한국어)로 디코딩
        }

        return JsonUtility.FromJson<T>(jsonData);           // 받아온 json데이터를 T(제네릭)형식으로 바꿔 반환
    }

    public static List<T> LoadJsonList<T>(string fileName)
    {
        return LoadJsonList<T>(Application.dataPath, fileName);
    }
    public static List<T> LoadJsonList<T>(string defaultPath, string fileName)
    {
        string jsonData = string.Empty;                                     //string 변수생성
        string path = string.Concat(defaultPath, "/", fileName + ".json");

        using (FileStream stream = new FileStream(path, FileMode.Open))
        {
            byte[] data = new byte[stream.Length];          // 읽어들인 파일의 길이
            stream.Read(data, 0, data.Length);              // 파일을 0의 위치에서 length만큼 읽어 data에 대입

            jsonData = Encoding.UTF8.GetString(data);        // byte[]을 UTF-8(한국어)로 디코딩
        }

        ConvertList<T> list = JsonUtility.FromJson<ConvertList<T>>(jsonData);           // 받아온 json데이터를 T(제네릭)형식으로 바꿔 반환

        return list.list;
    }
}