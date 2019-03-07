using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class GameThreadScript : MonoBehaviour
{

    public ArticleCreateScriptable article;

    public Transform operatingTransform;

    public Transform editorPanel;
    // elementBtn 是预制体，包含着排序元素等属性。
    // addBtn 是单独的添加按钮。
    public Button elementBtn, addBtn;

    public InputField articleInput, valueInput;

    public Button editorModefyBtn, editorDeleteBtnl;

    // 定位地标
    private int currentOnlyNumber;

    private List<ButtonElementScript> spawnBtnElementList = new List<ButtonElementScript>();

    // Use this for initialization
    void Start()
    {
        InitStandardElement();
        
    }

    void InitStandardElement()
    {
        int count = article.articleArray.Count;
        for (int i = 0; i < count; i++)
        {
            var temp = Instantiate(elementBtn, operatingTransform);
            var articleData = article.articleArray[i];
            var tempScript = temp.GetComponent<ButtonElementScript>();
            tempScript.SetData(articleData.articleName, articleData.articleValue,
                articleData.articleNumber, this);

            spawnBtnElementList.Add(tempScript);
        }

        addBtn.onClick.AddListener(CreateArticleUIClick);
        editorModefyBtn.onClick.AddListener(ModifyArticleData);
        editorDeleteBtnl.onClick.AddListener(DeleteArticleData);
    }

    /// <summary>
    /// 创建新的数据UI
    /// </summary>
    void CreateArticleUIClick()
    {
        var temp = article.CreateArticle();
        currentOnlyNumber = temp.articleNumber;
        articleInput.text = temp.articleName;
        valueInput.text = temp.articleValue.ToString(CultureInfo.InvariantCulture);

        var instanTemp = Instantiate(elementBtn, operatingTransform);
        instanTemp.GetComponent<ButtonElementScript>().SetData(temp.articleName, temp.articleValue,
            temp.articleNumber, this);

        spawnBtnElementList.Add(instanTemp.GetComponent<ButtonElementScript>());
        SetActiveState(editorPanel, true);
    }

    void SetActiveState(Transform _stateTransform, bool _isActive)
    {
        _stateTransform.gameObject.SetActive(_isActive);
    }

    private ButtonElementScript GetBtnElement()
    {
        foreach (var v in spawnBtnElementList)
        {
            if (v.GetOnlyNumber() == currentOnlyNumber)
            {
                return v;
            }
        }

        return null;
    }

    /// <summary>
    /// 请求修改数据UI信息
    /// </summary>
    void ModifyArticleData()
    {
        try
        {
            if (GetBtnElement() == null)
            {
                // 修改失败
                PromptPanelScript.Instance.ShowPanelSecond(0.6f,"程序出现问题");
                return;
            }

            float.Parse(valueInput.text);
        }
        catch (Exception e)
        {
            // 失败
            PromptPanelScript.Instance.ShowPanelSecond(0.6f, "数值必须为纯数字");
            return;
        }

        ArticleCreateScriptable.ArticleValueStruct temp = new ArticleCreateScriptable.ArticleValueStruct()
        {
            articleNumber = currentOnlyNumber,
            articleName = articleInput.text,
            articleValue = float.Parse(valueInput.text)
        };

        if (article.SetDataArticle(temp))
        {
            GetBtnElement().SetSmallData(articleInput.text, float.Parse(valueInput.text), currentOnlyNumber);

            // 修改成功
            PromptPanelScript.Instance.ShowPanelSecond(0.6f, "修改成功");
        }

    }

    /// <summary>
    /// 删除数据UI信息
    /// </summary>
    void DeleteArticleData()
    {
        if (GetBtnElement() == null)
        {
            // 删除失败
            PromptPanelScript.Instance.ShowPanelSecond(0.6f, "删除失败");
            return;
        }

        article.DeleteArticle(currentOnlyNumber);

        var temp = GetBtnElement();

        spawnBtnElementList.Remove(temp);

        Destroy(temp.gameObject);
        PromptPanelScript.Instance.ShowPanelSecond(0.6f, "删除成功");
        SetActiveState(editorPanel, false);
        currentOnlyNumber = -1;
    }

    /// <summary>
    /// 读取数据UI信息。
    /// </summary>
    /// <param name="_number"></param>
    public void UILoadData(int _number)
    {
        ArticleCreateScriptable.ArticleValueStruct temp = null;
        foreach (var v in article.articleArray)
        {
            if (v.articleNumber == _number)
            {
                temp = v;
                break;
            }
        }

        if (temp == null)
        {
            return;
        }

        currentOnlyNumber = _number;
        articleInput.text = temp.articleName;
        valueInput.text = temp.articleValue.ToString(CultureInfo.InvariantCulture);

        SetActiveState(editorPanel, true);
    }


}
