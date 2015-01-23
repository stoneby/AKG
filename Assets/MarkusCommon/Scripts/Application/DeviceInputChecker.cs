using UnityEngine;

public class DeviceInputChecker : MonoBehaviour
{
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android && (Input.GetKeyDown(KeyCode.Escape)))
        {
            Application.Quit();
        }
    }
}
