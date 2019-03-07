using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "articleBehaviour",menuName = "CrateScriptableObject/ArticleScriptable")]
public class ArticleCreateScriptable : ScriptableObject
{
    [System.Serializable]
    public class ArticleValueStruct
    {
        public int articleNumber;
        public string articleName;
        public float articleValue;
    }

    public List<ArticleValueStruct> articleArray;
    public ArticleValueStruct[] GetArticleValue()
    {
        return articleArray.ToArray();
    }

    public ArticleValueStruct GetDataArticle(int _number)
    {
        foreach (var v in articleArray)
        {
            if (v.articleNumber == _number)
            {
                return v;
            }
        }

        return null;
    }


    /// <summary>
    /// 修改数据
    /// </summary>
    /// <param name="_article"></param>
    /// <returns></returns>
    public bool SetDataArticle(ArticleValueStruct _article)
    {
        bool isExist = false;
        int i = 0;
        for (;i < articleArray.Count; i++)
        {
            if (articleArray[i].articleNumber == _article.articleNumber)
            {
                isExist = true;
                break;
            }
        }

        if (isExist)
        {
            articleArray[i] = _article;

            return true;
        }

        return false;

    }
    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="_number"></param>
    public void DeleteArticle(int _number)
    {
        bool isExist = false;
        int i = 0;

        for (; i < articleArray.Count; i++)
        {
            if (articleArray[i].articleNumber == _number)
            {
                isExist = true;
                break;
            }
        }

        if (isExist)
        {
            articleArray.RemoveAt(i);
        }
        
    }

    /// <summary>
    /// 创建数据
    /// </summary>
    /// <returns></returns>
    public ArticleValueStruct CreateArticle()
    {
        ArticleValueStruct temp = new ArticleValueStruct
        {
            articleNumber = articleArray.Count,
            articleName = "未命名" + articleArray.Count,
            articleValue = 0
        };

        articleArray.Add(temp);

        return temp;
    }

}
