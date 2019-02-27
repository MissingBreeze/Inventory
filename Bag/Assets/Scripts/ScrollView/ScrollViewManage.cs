using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollViewManage : MonoBehaviour
{
    ///*
    //    初始化加载36个数据格子
    //    加载数据
    //    下拉翻页（判断是否有实际数据）
    //    整理数据
    //    子项多选删除      
    //*/

    #region

    /// <summary>
    /// 对外公开的获取方式
    /// </summary>
    public ScrollViewManage Get { get; set; }



    #endregion

    #region 变量

    private Transform content;

    /// <summary>
    /// 每页格子数
    /// </summary>
    public int pageCount = 36;

    /// <summary>
    /// 子项
    /// </summary>
    private List<ScrollViewListItem> itemList;

    

    #endregion


    private void Awake()
    {
        Get = gameObject.GetComponent<ScrollViewManage>();
        content = transform.Find("Viewport/Content");
        itemList = new List<ScrollViewListItem>();
        for (int i = 0; i < pageCount-1; i++) // 生成子项
        {
            ScrollViewListItem item = Instantiate(transform.Find("Viewport/Content/item").gameObject, content).GetComponent<ScrollViewListItem>();
            if(item == null)
            {
                Debug.LogError("请检查子项是否有继承类ScrollViewListItem");
                return;
            }
            itemList.Add(item);
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }



}
