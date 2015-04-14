using UnityEngine;

public class SettingController : MonoBehaviour
{
    public void OnExit()
    {
        SceneManager.Instance.Load("Menu");
    }
}
