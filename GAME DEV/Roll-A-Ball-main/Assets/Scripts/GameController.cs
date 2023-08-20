using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlType { Normal, WorldTilt } 
public class GameController : MonoBehaviour
{
    public static GameController instance;
    public ControlType controlType;

    // Start is called before the first frame update
    void Start()
    {
        if (instance ==null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    //Toggles for control type
    public void ToggleWorldTilt(bool _tilt)
    {
        if (_tilt)
            controlType = ControlType.WorldTilt;
        else
            controlType = ControlType.Normal;
    }
}
