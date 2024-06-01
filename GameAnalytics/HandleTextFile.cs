using System;
using UnityEngine;
using System.IO;
using System.IO.IsolatedStorage;

public class HandleTextFile: MonoBehaviour
{
    private void Start()
    {
        ReadString();
    }

    public string WriteString(string data)
    {
        string path = Application.persistentDataPath + "/NewFile1.txt";
        StreamWriter writer = new StreamWriter(path, false);
        writer.Flush();
        writer.WriteLine(data);
        writer.Close();
        StreamReader reader = new StreamReader(path);
        //Print the text from the file
        Debug.Log(reader.ReadToEnd());
        reader.Close();
        return path;
    }
    public void ReadString()
    {
        string path = Application.persistentDataPath + "/NewFile1.txt";
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }
}