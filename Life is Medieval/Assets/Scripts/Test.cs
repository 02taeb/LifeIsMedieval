using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    /// <summary>
    /// Can be used for testing other scripts. 
    /// <br></br>
    /// Just instantiate your script in method and fill out desired functionality.
    /// <br></br>
    /// Can be bound to OnClick() event on button for easy testing.
    /// </summary>
    public void ForTesting()
    {
        GameController gameController = GameObject.Find("GameController").GetComponent<GameController>();
        FileReader fr = new FileReader();

        gameController.SetTest();
        fr.WriteToFile("SaveGame", gameController.SaveTest());
        gameController.ResetTest();
        gameController.LoadTest(fr.ReadFromFile("SaveGame"));
        gameController.DebugValues();
    }
}
