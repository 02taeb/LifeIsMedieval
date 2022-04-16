using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileReader : MonoBehaviour
{
    /// <summary>
    /// Takes a fileName and adds ".txt" then reads all lines from file.
    /// <br></br>
    /// Throws FileNotFoundException and Exception.
    /// </summary>
    /// <param name="fileName">Name of file to read from.</param>
    /// <returns>List&lt;string&gt; with lines read.</returns>
    public List<string> ReadFromFile(string fileName)
    {
        fileName += ".txt";
        List<string> strings = new List<string>();

        try
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    strings.Add(line);
                }
            }
        }
        catch (FileNotFoundException ex)
        {
            throw ex;
        }
        catch (Exception e)
        {
            throw e;
        }

        return strings;
    }

    public void WriteToFile(string fileName, List<string> stringsToWrite)
    {
        fileName += ".txt";
        using (StreamWriter sw = new StreamWriter(fileName))
        {
            foreach (string line in stringsToWrite)
            {
                sw.WriteLine(line);
            }
        }
    }
}
