using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class TollCollections : MonoBehaviour
{
    public List<TollController> TollControllerList;
    public RectTransform m_RectTransform;
    public HorizontalLayoutGroup m_Grid;

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
