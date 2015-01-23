using UnityEngine;

/// <summary>
/// Xml file based path layer mapping.
/// </summary>
public class XMLPathLayerMapping : AbstractPathLayerMapping
{
    #region Fields

    public TextAsset Text;

    #endregion

    #region Properties

    public bool IsValid
    {
        get
        {
            return Text != null && !string.IsNullOrEmpty(Text.text);
        }
    }

    #endregion

    #region AbstractPathLayerMapping

    public override bool Parse()
    {
        return true;
    }

    #endregion

    #region Mono

    // Use this for initialization
    void Start()
    {
        if (!IsValid)
        {
            Logger.LogError("Text should not be null or empty, please drag xml generated from Window Configurator.");
        }
    }

    #endregion
}
