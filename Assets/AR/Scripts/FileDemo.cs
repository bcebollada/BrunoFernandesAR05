using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileDemo : MonoBehaviour
{
    public string fileName = "C:\\Users\\var05bruno\\Downloads\\FileTest\\DemoFile.txt";

    private void Start()
    {
        string contents = "This is a file";

        File.WriteAllText(fileName, contents);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
