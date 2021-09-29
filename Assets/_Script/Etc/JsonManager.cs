using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

// JavaScript Object Notation
// �ٸ��� ��ũ�� ���� �±� ���� �̿��� �������� ������ ����Ѵ�.
// �ַ� HTML���� ����ϸ� �����Ϳ� �ǹ̸� �ο��ϴ� meta�����͸� ������ �� �ִ�.
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
        // obj�� Json���Ϸ� ��ȯ
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
        string jsonData = string.Empty;                                     //string ��������
        string path = string.Concat(defaultPath, "/", fileName + ".json");

        using (FileStream stream = new FileStream(path, FileMode.Open))
        {
            byte[] data = new byte[stream.Length];          // �о���� ������ ����
            stream.Read(data, 0, data.Length);              // ������ 0�� ��ġ���� length��ŭ �о� data�� ����

            jsonData = Encoding.UTF8.GetString(data);        // byte[]�� UTF-8(�ѱ���)�� ���ڵ�
        }

        return JsonUtility.FromJson<T>(jsonData);           // �޾ƿ� json�����͸� T(���׸�)�������� �ٲ� ��ȯ
    }

    public static List<T> LoadJsonList<T>(string fileName)
    {
        return LoadJsonList<T>(Application.dataPath, fileName);
    }
    public static List<T> LoadJsonList<T>(string defaultPath, string fileName)
    {
        string jsonData = string.Empty;                                     //string ��������
        string path = string.Concat(defaultPath, "/", fileName + ".json");

        using (FileStream stream = new FileStream(path, FileMode.Open))
        {
            byte[] data = new byte[stream.Length];          // �о���� ������ ����
            stream.Read(data, 0, data.Length);              // ������ 0�� ��ġ���� length��ŭ �о� data�� ����

            jsonData = Encoding.UTF8.GetString(data);        // byte[]�� UTF-8(�ѱ���)�� ���ڵ�
        }

        ConvertList<T> list = JsonUtility.FromJson<ConvertList<T>>(jsonData);           // �޾ƿ� json�����͸� T(���׸�)�������� �ٲ� ��ȯ

        return list.list;
    }
}