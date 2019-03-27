using System;
using System.Collections;
using System.Collections.Generic;
using WYF.Event;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using WYF.BaseUIElement.ScrollView;
using WYF.BaseUIElement.ScrollView.Data;
using WYF.DataDto;
using WYF.Utils;

public class BagManage : MonoBehaviour
{

    public int itemCount = 36;

    public Button add;

    private ScrollViewManage scrollViewManage;

    WYF.BaseUIElement.ToggleGroup.ToggleGroupManage toggleGroup;

    private List<GoodsDataDto> goods;

    private void OnDisable()
    {
        EventManager.Instance.RemoveEventListener<ScrollItemEventData>(ScrollItemEventData.DRAG, DragHandler);
    }

    void Start ()
    {
        GoodsDataDto goodsDataDto = new GoodsDataDto("0", "小血瓶", "10");
        GoodsDataDto goodsDataDto1 = new GoodsDataDto("1", "小蓝瓶", "5");
        GoodsDataDto goodsDataDto2 = new GoodsDataDto("2", "木剑", "1");
        GoodsDataDto goodsDataDto3 = new GoodsDataDto("3", "木甲", "1");
        GoodsDataDto goodsDataDto4 = new GoodsDataDto("4", "传送石", "7");
        goods = new List<GoodsDataDto>();
        goods.Add(goodsDataDto);
        goods.Add(goodsDataDto1);
        goods.Add(goodsDataDto2);
        goods.Add(goodsDataDto3);
        goods.Add(goodsDataDto4);
        string json = JsonUtility.ToJson(new Serialization<GoodsDataDto>(goods));
        FileOperateUtils.Instance.WriteFile("PlayerBag.json", "", json);
        return;
        scrollViewManage = transform.Find("Scroll View").GetComponent<ScrollViewManage>();
        List<GoodsDataDto> dataList = new List<GoodsDataDto>();
        //for (int i = 0; i < itemCount; i++)
        //{
        //    GoodsDataDto goodsDataDto = new GoodsDataDto();
        //    goodsDataDto.name = "item" + i;
        //    dataList.Add(goodsDataDto);
        //}
        scrollViewManage.UpAllData(dataList);
        scrollViewManage.RegisterSliderEvent(SliderTypeEnum.Up, Additem);
        add.onClick.AddListener(Additem);

        //toggleGroup = transform.Find("ToggleGroup").GetComponent<WYF.BaseUIElement.ToggleGroup.ToggleGroup>();
        EventManager.Instance.AddEventListener<ScrollItemEventData>(ScrollItemEventData.DRAG, DragHandler);
        EventManager.Instance.AddEventListener<ScrollItemEventData>(ScrollItemEventData.DRAGEND, DragEndHandler);
    }

    #region 滑动列表相关

    private void DragEndHandler(ScrollItemEventData e)
    {
        isDragScrollItem = false;
        List<GameObject> goList = MouseRaycastUtils.Instance.MouseRaycast("ScrollItem");
        Destroy(item.gameObject);
        if (goList != null && goList.Count != 0)
        {
            ScrollViewItemView scrollViewItemView = goList[0].GetComponent<ScrollViewItemView>();
            if (scrollViewItemView)
            {
                scrollViewManage.ExchangeItem<GoodsDataDto>(index, scrollViewItemView.Index);
            }
        }
    }

    private void DragHandler(ScrollItemEventData e)
    {
        index = e.index;
        item = Instantiate(e.itemGo, scrollViewManage.transform).transform;
        item.GetComponent<Image>().raycastTarget = false;
        RectTransform rt = item.GetComponent<RectTransform>();
        rt.pivot = new Vector2(0,1);
        rt.anchorMin = new Vector2(0,1);
        rt.anchorMax = new Vector2(0, 1);
        rt.sizeDelta = new Vector2(e.itemGo.transform.GetComponent<RectTransform>().rect.width, e.itemGo.transform.GetComponent<RectTransform>().rect.height);
        isDragScrollItem = true;
    }

    private Transform item;

    private int index;

    private void DragItem()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.GetComponent<RectTransform>(), Input.mousePosition, null, out position);
        item.localPosition = new Vector3(position.x - item.GetComponent<RectTransform>().sizeDelta.x / 2, position.y, 0);
        //List<GameObject> goList = MouseRaycastUtils.Instance.MouseRaycast("ScrollItem");
        //if(goList != null && goList.Count > 0)
        //{
        //    ScrollViewItemView scrollViewItemView = goList[0].GetComponent<ScrollViewItemView>();
        //    if (scrollViewItemView)
        //    {
        //        scrollViewItemView.ShowMaskImg();
        //    }
        //}
    }

    #endregion

    private void Additem()
    {
        List<GoodsDataDto> dataList = new List<GoodsDataDto>();
        for (int i = 0; i < itemCount; i++)
        {
            GoodsDataDto goodsDataDto = new GoodsDataDto();
            goodsDataDto.name = "item" + i;
            dataList.Add(goodsDataDto);
        }
        scrollViewManage.LoadData(dataList);
    }

    private bool isDragScrollItem = false;

    void Update ()
    {
        if (isDragScrollItem)
        {
            DragItem();
        }

    }
}
