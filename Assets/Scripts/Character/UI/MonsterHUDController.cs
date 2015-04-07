using UnityEngine;
using UnityEngine.UI;

public class MonsterHUDController : MonoBehaviour
{
    public Transform HUDLocation;
    public GameObject HUDPrefab;

    public float HideTime;

    public float HpValue { get; set; }

    private Slider hpSlider;

    public void Show(bool flag)
    {
        HUDLocation.gameObject.SetActive(flag);

        if (flag)
        {
            hpSlider.value = HpValue;

            CancelInvoke("Hide");
            Invoke("Hide", HideTime);
        }
    }

    private void Hide()
    {
        HUDLocation.gameObject.SetActive(false);
    }

    private void Awake()
    {
        HUDLocation = transform.Find("HUDLocation");
        var hud = Instantiate(HUDPrefab, HUDLocation.position, HUDLocation.rotation) as GameObject;
        hud.transform.parent = HUDLocation;

        hpSlider = hud.transform.Find("HPSlider").GetComponent<Slider>();

        Show(false);
    }
}
