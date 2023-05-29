using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDisplayOnServer : MonoBehaviour
{
    private void Awake()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            gameObject.SetActive(false);
        } else
        {
            gameObject.SetActive(true);
        }
    }
}
