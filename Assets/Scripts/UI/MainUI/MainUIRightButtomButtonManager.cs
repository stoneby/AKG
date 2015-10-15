using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainUIRightButtomButtonManager : MonoBehaviour, IComparable<MainUIRightButtomButtonManager>
{
    public int m_index;
    public string m_button_name;

    public RectTransform m_RectTransform;
    public Button m_Button;
    public Image m_Image;
    public MainUIRightButtomController m_Controller;

    private void OnButtonClick()
    {
        m_Controller.OnButtonClick(m_index);
    }

    /// <summary>
    /// for buttons compare, not used in this version.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public int CompareTo(MainUIRightButtomButtonManager other)
    {
        if (other == null)
        {
            return 1;
        }
        else
        {
            return m_index.CompareTo(other.m_index);
        }
    }

    void Awake()
    {
        m_Button.onClick.AddListener(OnButtonClick);
    }

    void OnDestroy()
    {
        m_Button.onClick.RemoveListener(OnButtonClick);
    }
}
