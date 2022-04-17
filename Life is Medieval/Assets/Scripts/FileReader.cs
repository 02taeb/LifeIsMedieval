using System;
using System.Collections.Generic;
using System.IO;

public class FileReader
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
        string CurrentDirectory = Environment.CurrentDirectory;
        Environment.CurrentDirectory = Environment.CurrentDirectory += "\\Assets\\Text Files";
        
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

        Environment.CurrentDirectory = CurrentDirectory;
        return strings;
    }

    /// <summary>
    /// Takes in a fileName and adds ".txt" then writes all lines from stringsToWrite to file (will create file if not found).
    /// <br></br>
    /// File will be saved in "...\LifeIsMedieval\Life is Medieval\fileName.txt"
    /// </summary>
    /// <param name="fileName">Name of file to write to or to create.</param>
    /// <param name="stringsToWrite">List&lt;string&gt; to write to file</param>
    public void WriteToFile(string fileName, List<string> stringsToWrite)
    {
        string CurrentDirectory = Environment.CurrentDirectory;
        Environment.CurrentDirectory = Environment.CurrentDirectory += "\\Assets\\Text Files";

        fileName += ".txt";
        using (StreamWriter sw = new StreamWriter(fileName))
        {
            foreach (string line in stringsToWrite)
            {
                sw.WriteLine(line);
            }
        }

        Environment.CurrentDirectory = CurrentDirectory;
    }
}
