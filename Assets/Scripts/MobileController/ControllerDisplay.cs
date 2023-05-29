using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerDisplay : MonoBehaviour
{
    [SerializeField]
    private GameObject background = null;
    [SerializeField]
    private GameObject leftJoystick = null;
    [SerializeField]
    private GameObject rightJoystick = null;
    [SerializeField]
    private GameObject rightButton = null;
    [SerializeField]
    private GameObject leftButton = null;

    public void Start()
    {        
        if(Application.platform == RuntimePlatform.Android)
        {
            background.SetActive(true);
            leftJoystick.SetActive(true);
            rightJoystick.SetActive(true);
            leftButton.SetActive(true);
            rightButton.SetActive(true);
        }
    }
}
