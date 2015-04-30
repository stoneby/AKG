using UnityEngine;

public class SettingController : MonoBehaviour
{
    public void OnExit()
    {
        SSSceneManager.Instance.PopUp("DemoPopup", new DemoPopupData("Are you sure to quit?", DemoPopupType.YES_NO),
            (ctrl) =>
            {
                var popup = (DemoPopup) ctrl;
                popup.onYesButtonTap += OnYesButtonTap;
            },
            (ctrl) =>
            {
                var popup = (DemoPopup) ctrl;
                popup.onYesButtonTap -= OnYesButtonTap;
            });
    }

    private void OnYesButtonTap()
    {
        SSSceneManager.Instance.Screen("Menu");
    }
}
