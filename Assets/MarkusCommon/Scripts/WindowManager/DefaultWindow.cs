
/// <summary>
/// Default window, which do nothing in OnOpen and OnClose.
/// </summary>
public class DefaultWindow : AbstractWindow
{
    #region AbstractWindow

    public override void OnOpen()
    {
    }

    public override void OnClose()
    {
    }

    #endregion

    #region Mono

    // Use this for initialization
    protected override void Start()
    {
    }

    #endregion
}
