using UnityEngine;

public class PageHandler : MonoBehaviour
{
    public UIGrid Grid;
    public float SpringStrength;

    public bool UsePanel;
    public bool UseGrid;
    public float CustomizeWidth;

    private UIPanel panel;
    private bool animating;

    public void OnPreButtonClicked()
    {
        if (animating)
        {
            return;
        }
        animating = true;

        var panelPosition = panel.cachedTransform.localPosition;
        var offsizePosition = (panelPosition.x >= 0) ? Vector3.zero : new Vector3(CustomizeWidth, 0);
        SpringPanel.Begin(panel.cachedGameObject, panel.cachedTransform.localPosition + offsizePosition, SpringStrength).onFinished += () =>
        {
            animating = false;
        };
    }

    public void OnNextButtonClicked()
    {
        if (animating)
        {
            return;
        }
        animating = true;

        var panelPosition = panel.cachedTransform.localPosition;
        var offsizePosition = (panelPosition.x <= -(int)((Grid.GetChildList().Count - 1) * Grid.cellWidth / CustomizeWidth) * CustomizeWidth)
            ? Vector3.zero
            : new Vector3(-CustomizeWidth, 0);
        SpringPanel.Begin(panel.cachedGameObject, panel.cachedTransform.localPosition + offsizePosition, SpringStrength).onFinished += () =>
        {
            animating = false;
        };
    }

    private void Awake()
    {
        panel = NGUITools.FindInParents<UIPanel>(Grid.gameObject);
        CustomizeWidth = (UsePanel) ? panel.width : ((UseGrid) ? Grid.cellWidth : CustomizeWidth);
    }
}
