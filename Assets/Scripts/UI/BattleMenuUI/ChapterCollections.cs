using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class ChapterCollections : MonoBehaviour
{
    public List<ChapterController> ChapterControllerList;
    public RectTransform m_RectTransform;
    public HorizontalLayoutGroup m_Grid;

    public float MovingDistance
    {
        get { return ChapterControllerList.First().GetComponent<RectTransform>().rect.width * 3; }
    }

    public static bool IsChapterOpen = false;

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
    }
}
