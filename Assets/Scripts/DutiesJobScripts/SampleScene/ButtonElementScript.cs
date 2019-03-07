using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ButtonElementScript : MonoBehaviour
{
    private Text beforeText, rearText;
    private Button selfBtn;
    private int onlyNumber;
    private GameThreadScript ctrl;
    void Awake()
    {
        beforeText = transform.Find("BeforeText").GetComponent<Text>();
        rearText = transform.Find("RearText").GetComponent<Text>();
        selfBtn = GetComponent<Button>();
    }

    public int GetOnlyNumber()
    {
        return onlyNumber;
    }

    public void SetSmallData(string _articleName, float _articleValue, int _articleNumber)
    {
        beforeText.text = _articleName;
        rearText.text = _articleValue.ToString(CultureInfo.InvariantCulture);
        onlyNumber = _articleNumber;
    }

    public void SetData(string _articleName, float _articleValue, int _articleNumber, GameThreadScript _instance)
    {
        beforeText.text = _articleName;
        rearText.text = _articleValue.ToString(CultureInfo.InvariantCulture);
        onlyNumber = _articleNumber;
        if (ctrl == null)
            ctrl = _instance;

        selfBtn.onClick.AddListener(ClickEvent);
    }

    void ClickEvent()
    {
        if (ctrl)
        {
            ctrl.UILoadData(onlyNumber);
        }
    }
}