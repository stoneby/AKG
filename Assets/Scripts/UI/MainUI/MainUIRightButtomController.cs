using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainUIRightButtomController : MonoBehaviour
{
    public const float MoveDuration = 0.5f;

    public RectTransform RightButtonParent;
    public List<MainUIRightButtomButtonManager> RightButtonManagerList = new List<MainUIRightButtomButtonManager>();
    public RectTransform ButtomButtonParent;
    public List<MainUIRightButtomButtonManager> ButtomButtonManagerList = new List<MainUIRightButtomButtonManager>();

    public Button m_OpenCloseButton;

    private const string ButtonPrefabPath = "UI/MainUI/MainUIRightButtomButton";
    private GameObject buttonPrefab;

    private Dictionary<int, Sprite> SpriteIndexDic = new Dictionary<int, Sprite>();

    private const int buttonCellLength = 110;

    private List<int> RightButtonIndexList = new List<int>();
    private List<int> ButtomButtonIndexList = new List<int>();

    public Sprite MailSprite;
    public Sprite FriendSprite;
    public Sprite FamousSprite;
    public Sprite HeroSprite;
    public Sprite EquipmentSprite;
    public Sprite SkillSprite;
    public Sprite TalentSprite;
    public Sprite RuneSprite;

    #region Initialize

    void InitUI()
    {
        AddButtons();
        RankButtons();
        InitButtonsPosition();
    }

    void AddButtons()
    {
        RightButtonIndexList.ForEach(item =>
        {
            var temp = Instantiate(buttonPrefab).GetComponent<RectTransform>();
            temp.parent = RightButtonParent;
            temp.transform.localPosition = Vector3.zero;
            temp.transform.localScale = Vector3.one;
            var manager = temp.GetComponent<MainUIRightButtomButtonManager>();
            manager.m_Controller = this;
            manager.m_index = item;
            manager.m_Image.sprite = SpriteIndexDic[item];
            manager.m_button_name = manager.m_Image.sprite.name;
            RightButtonManagerList.Add(manager);
        });

        ButtomButtonIndexList.ForEach(item =>
        {
            var temp = Instantiate(buttonPrefab).GetComponent<RectTransform>();
            temp.parent = ButtomButtonParent;
            temp.transform.localPosition = Vector3.zero;
            temp.transform.localScale = Vector3.one;
            var manager = temp.GetComponent<MainUIRightButtomButtonManager>();
            manager.m_Controller = this;
            manager.m_index = item;
            manager.m_Image.sprite = SpriteIndexDic[item];
            manager.m_button_name = manager.m_Image.sprite.name;
            ButtomButtonManagerList.Add(manager);
        });
    }

    void RankButtons()
    {
        RightButtonManagerList.Sort();
        ButtomButtonManagerList.Sort();
    }

    void InitButtonsPosition(bool isPlayAnimation = false)
    {
        SetButtonsPosition(true, false);
    }

    #endregion

    #region OpenCloseController

    private enum OpenCloseState
    {
        Opened,
        Closed,
        Moving
    }
    private OpenCloseState m_openCloseState = OpenCloseState.Opened;

    void OnOpenCloseClick()
    {
        if (m_openCloseState != OpenCloseState.Moving)
        {
            if (m_openCloseState == OpenCloseState.Opened)
            {
                SetButtonsPosition(false, true);
            }
            else
            {
                SetButtonsPosition(true, true);
            }

            m_openCloseState = OpenCloseState.Moving;
        }
    }

    void SetButtonsPosition(bool isOpen, bool isPlayAnimation)
    {
        if (isOpen)
        {
            for (int i = 0; i < RightButtonManagerList.Count; i++)
            {
                if (isPlayAnimation)
                {
                    iTween.MoveTo(RightButtonManagerList[i].gameObject, iTween.Hash(
                        "position", new Vector3(0, buttonCellLength * (RightButtonManagerList.Count - i), 0),
                        "time", MoveDuration,
                        "easeType", "easeOutBack",
                        "islocal", true,
                        "oncomplete", "OnOpenComplete",
                        "oncompletetarget", gameObject));
                }
                else
                {
                    RightButtonManagerList[i].transform.localPosition = new Vector3(0, buttonCellLength * (RightButtonManagerList.Count - i), 0);
                }
            }

            for (int i = 0; i < ButtomButtonManagerList.Count; i++)
            {
                if (isPlayAnimation)
                {
                    iTween.MoveTo(ButtomButtonManagerList[i].gameObject, iTween.Hash(
                        "position", new Vector3(-buttonCellLength * (ButtomButtonManagerList.Count - i), 0, 0),
                        "time", MoveDuration,
                        "easeType", "easeOutBack",
                        "islocal", true,
                        "oncomplete", "OnOpenComplete",
                        "oncompletetarget", gameObject));
                }
                else
                {
                    ButtomButtonManagerList[i].transform.localPosition = new Vector3(-buttonCellLength * (ButtomButtonManagerList.Count - i), 0, 0);
                }
            }
        }
        else
        {
            if (isPlayAnimation)
            {
                RightButtonManagerList.ForEach(item =>
                {
                    iTween.MoveTo(item.gameObject, iTween.Hash(
                        "position", Vector3.zero,
                        "time", MoveDuration,
                        "easeType", "linear",
                        "islocal", true,
                        "oncomplete", "OnCloseComplete",
                        "oncompletetarget", gameObject));
                });

                ButtomButtonManagerList.ForEach(item =>
                {
                    iTween.MoveTo(item.gameObject, iTween.Hash(
                        "position", Vector3.zero,
                        "time", MoveDuration,
                        "easeType", "linear",
                        "islocal", true,
                        "oncomplete", "OnCloseComplete",
                        "oncompletetarget", gameObject));
                });
            }
            else
            {
                RightButtonManagerList.ForEach(item =>
                {
                    item.transform.localPosition = Vector3.zero;
                });

                ButtomButtonManagerList.ForEach(item =>
                {
                    item.transform.localPosition = Vector3.zero;
                });
            }
        }
    }

    public void OnOpenComplete()
    {
        m_openCloseState = OpenCloseState.Opened;
    }

    public void OnCloseComplete()
    {
        m_openCloseState = OpenCloseState.Closed;
    }

    #endregion

    public void OnButtonClick(int index)
    {
        //Add function enterance here.
        switch (index)
        {

        }
    }

    void OnDestroy()
    {
        m_OpenCloseButton.onClick.RemoveListener(OnOpenCloseClick);
    }

    void Start()
    {
        LoadConfigXml();
        InitUI();
    }

    void Awake()
    {
        buttonPrefab = Resources.Load<GameObject>(ButtonPrefabPath);

        SpriteIndexDic.Add(10, MailSprite);
        SpriteIndexDic.Add(20, FriendSprite);
        SpriteIndexDic.Add(30, FamousSprite);
        SpriteIndexDic.Add(40, HeroSprite);
        SpriteIndexDic.Add(50, EquipmentSprite);
        SpriteIndexDic.Add(60, SkillSprite);
        SpriteIndexDic.Add(70, TalentSprite);
        SpriteIndexDic.Add(80, RuneSprite);

        m_OpenCloseButton.onClick.AddListener(OnOpenCloseClick);
    }

    //Configs here, convert this to diagram in the future.
    void LoadConfigXml()
    {
        RightButtonIndexList = new List<int>() { 10, 20, 30 };
        ButtomButtonIndexList = new List<int>() { 40, 50, 60, 70, 80 };
    }
}
