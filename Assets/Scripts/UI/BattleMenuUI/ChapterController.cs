using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class ChapterController : MonoBehaviour
{
    public int ChapterID;
    public const float MoveDuration = 0.5f;

    public Button ChapterButton;
    public ScrollRect TollsRect;
    public ChapterCollections m_ChapterCollections;


    void OnChapterClick()
    {
        if (!ChapterCollections.IsChapterOpen)
        {
            ChapterCollections.IsChapterOpen = true;

            m_ChapterCollections.m_Grid.enabled = false;
            TollsRect.gameObject.SetActive(true);

            m_ChapterCollections.ChapterControllerList.Where(item => item != this).ToList().ForEach(item =>
            {
                item.MoveToOpen();
            });
        }
        else
        {
            ChapterCollections.IsChapterOpen = false;

            TollsRect.gameObject.SetActive(false);

            m_ChapterCollections.ChapterControllerList.Where(item => item != this).ToList().ForEach(item =>
            {
                item.MoveToClose();
            });
        }
    }

    public void MoveToOpen()
    {
        iTween.MoveTo(gameObject, iTween.Hash(
            "position", new Vector3(GetComponent<RectTransform>().rect.width * (ChapterID + 0.5f) + m_ChapterCollections.MovingDistance, transform.localPosition.y, transform.localPosition.z),
            "time", MoveDuration,
            "easeType", "easeOutBack",
            "islocal", true,
            "oncomplete", "OnOpenComplete",
            "oncompletetarget", gameObject));
    }

    public void OnOpenComplete()
    {

    }

    public void MoveToClose()
    {
        iTween.MoveTo(gameObject, iTween.Hash(
            "position", new Vector3(GetComponent<RectTransform>().rect.width * (ChapterID + 0.5f), transform.localPosition.y, transform.localPosition.z),
            "time", MoveDuration,
            "easeType", "linear",
            "islocal", true,
            "oncomplete", "OnCloseComplete",
            "oncompletetarget", gameObject));
    }

    public void OnCloseComplete()
    {
        m_ChapterCollections.m_Grid.enabled = true;
    }

    void OnDestroy()
    {
        ChapterButton.onClick.RemoveListener(OnChapterClick);
    }

    void Awake()
    {
        if (m_ChapterCollections == null)
        {
            m_ChapterCollections = Utility.FindParent(transform, "Content").GetComponent<ChapterCollections>();
        }

        if (ChapterButton == null)
        {
            ChapterButton = GetComponent<Button>();
        }
        ChapterButton.onClick.AddListener(OnChapterClick);

        if (TollsRect == null)
        {
            TollsRect = Utility.FindChild(transform, "Scroll View").GetComponent<ScrollRect>();
        }
    }
}
