using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class ChapterCollections : MonoBehaviour
{
    public List<ChapterController> m_ChapterControllerList;
    public RectTransform m_RectTransform;
    public HorizontalLayoutGroup m_Grid;
    public ContentSizeFitter m_Fitter;

    public float MovingDistance
    {
        get { return m_ChapterControllerList.First().GetComponent<RectTransform>().rect.width * 3; }
    }

    public static bool IsChapterOpen = false;

    public void UpdateFitter()
    {
        m_Fitter.SetLayoutHorizontal();
    }

    void Awake()
    {
        if (m_RectTransform == null)
        {
            m_RectTransform = GetComponent<RectTransform>();
        }

        if (m_Grid == null)
        {
            m_Grid = GetComponent<HorizontalLayoutGroup>();
        }

        if (m_Fitter == null)
        {
            m_Fitter = GetComponent<ContentSizeFitter>();
        }
    }
}
