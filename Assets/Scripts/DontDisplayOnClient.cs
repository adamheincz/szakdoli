using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDisplayOnClient : MonoBehaviour
{
    private void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            gameObject.SetActive(false);
        }
    }
}
