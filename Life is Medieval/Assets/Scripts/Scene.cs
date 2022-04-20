using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour
{
    public Sprite sprite;
    public Scene prevScene, nextScene;
    public string sceneName;
    public SceneConfiguration sc = new SceneConfiguration();

    public class SceneConfiguration
    {
        public bool lBtns;
        public bool rBtns;
        public bool lText;
        public bool rText;
        public bool lGraphic;
        public bool rGraphic;
        public bool lPanel;
        public bool rPanel;
        public bool fGraphic;

        public bool CheckSetup()
        {
            return (Left() && Right()) ^ fGraphic;
        }

        private bool Left()
        {
            if (lPanel && !fGraphic)
                return lBtns ^ lText ^ lGraphic;

            return false;
        }

        private bool Right()
        {
            if (rPanel && !fGraphic)
                return rBtns ^ rText ^ rGraphic;

            return false;
        }
    }
}
