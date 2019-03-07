using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptPanelScript : Singleton<PromptPanelScript>
{
    private Text selfText;

	// Use this for initialization
	void Start ()
	{
	    selfText = transform.Find("Text").GetComponent<Text>();
        gameObject.SetActive(false);
	}

    public void ShowPanelSecond(float _second,string _content)
    {
        selfText.text = _content;
        gameObject.SetActive(true);
        Invoke("ClosePanel", _second);
    }

    void ClosePanel()
    {
        selfText.text = "";
        gameObject.SetActive(false);
    }
	
}
