using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewManage : MonoBehaviour
{
    ///*
    //    初始化加载36个数据格子
    //    加载数据
    //    下拉翻页（判断是否有实际数据）
    //    整理数据
    //    子项多选删除      
    //*/

    #region 变量

    private Transform content;

    /// <summary>
    /// 子项
    /// </summary>
    [HideInInspector]
    public List<ScrollViewListItem> itemList;

    /// <summary>
    /// 子项数据
    /// </summary>
    [HideInInspector]
    public List<IViewData> itemData;

    [Header("最大选中数量，0为无限制")]
    public int selectCount;

    private GameObject prefab;

    /// <summary>
    /// 列表滑动事件回调
    /// </summary>
    private Dictionary<SliderTypeEnum, Action> sliderAction;

    /// <summary>
    /// 垂直和水平滑动条初始值
    /// </summary>
    private float horiBarSize, vertiBarSize;

    private ScrollRect scrollRect;

    #endregion


    private void Awake()
    {
        content = transform.Find("Viewport/Content");
        prefab = transform.Find("Viewport/Content/item").gameObject;
        prefab.SetActive(false);
        //Destroy(transform.Find("Viewport/Content/item"));
        itemList = new List<ScrollViewListItem>();
        itemData = new List<IViewData>();
        sliderAction = new Dictionary<SliderTypeEnum, Action>();
        scrollRect = transform.GetComponent<ScrollRect>();
        scrollRect.onValueChanged.AddListener(ScrollBarSliderHandler);
        horiBarSize = scrollRect.horizontalScrollbar.size;
        vertiBarSize = scrollRect.verticalScrollbar.size;
    }

    /// <summary>
    /// 添加子项
    /// </summary>
    /// <param name="itemData"></param>
    /// <returns></returns>
    private bool AddItem(IViewData itemData)
    {
        if (prefab.GetComponent<ScrollViewListItem>() == null)
        {
            Debug.LogError("请检查子项是否有继承类ScrollViewListItem");
            return false;
        }
        ScrollViewListItem item = new ScrollViewListItem();
        item = Instantiate(prefab, content).GetComponent<ScrollViewListItem>();
        item.LoadData(itemData);
        item.Index = itemList.Count;
        item.stateChange = ItemStateChange;
        item.gameObject.SetActive(true);
        itemList.Add(item);
        return true;
    }

    /// <summary>
    /// 重新加载数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    public void UpAllData<T>(List<T> t) where T : IViewData
    { 
        ClearAllItemView();
        LoadData(t);
    }

    /// <summary>
    /// 增加数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    public void LoadData<T>(List<T> t) where T : IViewData
    {
        foreach (var item in t)
        {
            AddItem(item);
        }
    }

    /// <summary>
    /// 删除全部子项及子项数据
    /// </summary>
    public void ClearAllItemView()
    {
        if(itemList != null)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                Destroy(itemList[i].gameObject);
            }
            itemList = new List<ScrollViewListItem>();
            itemData = new List<IViewData>();
        }
    }

    /// <summary>
    /// 交换两个子项
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="index1"></param>
    /// <param name="index2"></param>
    /// <param name="dataList">不为空则交换前端数据</param>
    public void ExchangeItem<T>(int index1, int index2, List<T> dataList = null) where T : IViewData
    {
        int tempIndex = itemList[index1].transform.GetSiblingIndex();
        itemList[index1].transform.SetSiblingIndex(itemList[index2].transform.GetSiblingIndex());
        itemList[index1].Index = index2;
        itemList[index2].transform.SetSiblingIndex(tempIndex);
        itemList[index2].Index = index1;

        IViewData tempData = itemData[index1];
        itemData[index1] = itemData[index2];
        itemData[index2] = tempData;

        if (dataList != null && dataList.Count > 0)
        {
            T temp = dataList[index1];
            dataList[index1] = dataList[index2];
            dataList[index2] = temp;
        }
    }

    /// <summary>
    /// 批量移除子项
    /// </summary>
    /// <param name="idList">子项索引列表</param>
    public void RemoveItem(List<int> idList)
    {
        if(idList != null && idList.Count > 0)
        {
            for (int i = 0; i < idList.Count; i++)
            {
                RemoveItem(idList[i]);
            }
        }
    }

    /// <summary>
    /// 移除单个子项
    /// </summary>
    /// <param name="id">子项索引</param>
    public void RemoveItem(int id)
    {
        Destroy(itemList[id]);
        itemData.RemoveAt(id);
    }

    #region 子项选中相关

    private List<int> selectIdList = new List<int>();

    private void ItemStateChange(int id, bool state)
    {
        SetItemState(id, state);
    }

    /// <summary>
    /// 设置单个子项选中状态
    /// </summary>
    /// <param name="index"></param>
    /// <param name="state"></param>
    public void SetItemState(int index, bool state)
    {
        if (selectIdList.Contains(index))
            return;
        if(selectCount!= 0 && selectCount == selectIdList.Count)
        {
            itemList[selectIdList[0]].SetState(false);
            selectIdList.RemoveAt(0);
        }
        selectIdList.Add(index);
        itemList[index].SetState(state);
    }

    /// <summary>
    /// 设置单个子项选中状态
    /// </summary>
    /// <param name="index"></param>
    /// <param name="state"></param>
    public void SetItemState(List<int> index, bool state)
    {
        for (int i = 0; i < index.Count; i++)
        {
            SetItemState(index[i], state);
        }
    }

    /// <summary>
    /// 获取选中的子项id
    /// </summary>
    /// <returns></returns>
    public List<int> GetSelectId()
    {
        return selectIdList;
    }

    /// <summary>
    /// 获取选中的子项
    /// </summary>
    /// <returns></returns>
    public List<ScrollViewListItem> GetSelectItem()
    {
        List<ScrollViewListItem> itemList = new List<ScrollViewListItem>();
        for (int i = 0; i < selectIdList.Count; i++)
        {
            itemList.Add(itemList[selectIdList[i]]);
        }
        return itemList;
    }

    #endregion

    #region 列表滑动

    /// <summary>
    /// 注册列表滑动事件
    /// </summary>
    /// <param name="sliderType">滑动类型</param>
    /// <param name="action">回调事件</param>
    public void RegisterSliderEvent(SliderTypeEnum sliderType, Action action)
    {
        if (!sliderAction.ContainsKey(sliderType))
        {
            sliderAction.Add(sliderType, action);
        }
        else
        {
            sliderAction[sliderType] += action;
        }
    }

    /// <summary>
    /// 移除列表滑动事件
    /// </summary>
    /// <param name="sliderType">滑动类型</param>
    /// <param name="action">回调事件</param>
    public void RemoveSliderEvent(SliderTypeEnum sliderType, Action action)
    {
        if (sliderAction.ContainsKey(sliderType))
        {
            sliderAction[sliderType] -= action;
            if (sliderAction[sliderType] == null)
                sliderAction.Remove(sliderType);
        }
    }

    /// <summary>
    /// 列表滑动事件回调
    /// </summary>
    /// <param name="arg0"></param>
    private void ScrollBarSliderHandler(Vector2 arg0)
    {
        VertBarSliderHandler();
        HoriBarSliderHandler();
    }

    /// <summary>
    /// 垂直滑动条值改变事件
    /// </summary>
    private void VertBarSliderHandler()
    {
        if (scrollRect.verticalScrollbar == null)
            return;
        if (scrollRect.verticalScrollbar.size > vertiBarSize)
        {
            vertiBarSize = scrollRect.verticalScrollbar.size;
        }
        if (scrollRect.verticalScrollbar.value == 0 && sliderAction.ContainsKey(SliderTypeEnum.Up))
        {
            float dir = vertiBarSize - scrollRect.verticalScrollbar.size;
            dir = dir < 0 ? -dir : dir;
            if(dir/ scrollRect.verticalScrollbar.size > 0.1f)
            {
                sliderAction[SliderTypeEnum.Up]();
                vertiBarSize = 0;
            }
        }
        if (scrollRect.verticalScrollbar.value == 1 && sliderAction.ContainsKey(SliderTypeEnum.Down))
        {
            float dir = vertiBarSize - scrollRect.verticalScrollbar.size;
            dir = dir < 0 ? -dir : dir;
            if (dir / scrollRect.verticalScrollbar.size > 0.1f)
            {
                sliderAction[SliderTypeEnum.Down]();
                vertiBarSize = 0;
            }
        }
    }

    /// <summary>
    /// 水平滑动条值改变事件
    /// </summary>
    private void HoriBarSliderHandler()
    {
        if (scrollRect.horizontalScrollbar == null)
            return;
        if (scrollRect.horizontalScrollbar.size > horiBarSize)
        {
            horiBarSize = scrollRect.horizontalScrollbar.size;
        }
        if (scrollRect.horizontalScrollbar.value == 1 && sliderAction.ContainsKey(SliderTypeEnum.Left))
        {
            float dir = horiBarSize - scrollRect.horizontalScrollbar.size;
            dir = dir < 0 ? -dir : dir;
            if (dir / scrollRect.horizontalScrollbar.size > 0.1f)
            {
                sliderAction[SliderTypeEnum.Left]();
                horiBarSize = 0;
            }
        }
        if (scrollRect.horizontalScrollbar.value == 0 && sliderAction.ContainsKey(SliderTypeEnum.Right))
        {
            float dir = horiBarSize - scrollRect.horizontalScrollbar.size;
            dir = dir < 0 ? -dir : dir;
            if (dir / scrollRect.horizontalScrollbar.size > 0.1f)
            {
                sliderAction[SliderTypeEnum.Right]();
                horiBarSize = 0;
            }
        }
    }

    #endregion
}
