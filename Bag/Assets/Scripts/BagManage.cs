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

    private Image caption;

    private Text captionTxt;

    private void OnDisable()
    {
        EventManager.Instance.RemoveEventListener<ScrollItemEventData>(ScrollItemEventData.DRAG, DragHandler);
    }

    void Start ()
    {
        caption = transform.Find("Scroll View/caption").GetComponent<Image>();
        captionTxt = caption.transform.Find("captionTxt").GetComponent<Text>();
        caption.gameObject.SetActive(false);

        string dataStr = FileOperateUtils.Instance.ReadFile("PlayerBag.json", "");

        List<GoodsDataDto> dataList = JsonUtility.FromJson<Serialization<GoodsDataDto>>(dataStr).ToList();

        scrollViewManage = transform.Find("Scroll View").GetComponent<ScrollViewManage>();
        //List<GoodsDataDto> dataList = new List<GoodsDataDto>();
        //for (int i = 0; i < itemCount; i++)
        //{
        //    GoodsDataDto goodsDataDto = new GoodsDataDto();
        //    goodsDataDto.name = "item" + i;
        //    dataList.Add(goodsDataDto);
        //}
        scrollViewManage.UpAllData(dataList);
        //scrollViewManage.RegisterSliderEvent(SliderTypeEnum.Up, Additem);
        add.onClick.AddListener(Additem);

        //toggleGroup = transform.Find("ToggleGroup").GetComponent<WYF.BaseUIElement.ToggleGroup.ToggleGroup>();
        EventManager.Instance.AddEventListener<ScrollItemEventData>(ScrollItemEventData.DRAG, DragHandler);
        EventManager.Instance.AddEventListener<ScrollItemEventData>(ScrollItemEventData.DRAGEND, DragEndHandler);
        EventManager.Instance.AddEventListener<ScrollItemEventData>(ScrollItemEventData.SHOW, ShowCaptionHandler);
        EventManager.Instance.AddEventListener<ScrollItemEventData>(ScrollItemEventData.CLOSE, CloseCaptionHandler);
    }

    private void CloseCaptionHandler(ScrollItemEventData e)
    {
        caption.gameObject.SetActive(false);
    }

    private void ShowCaptionHandler(ScrollItemEventData e)
    {
        caption.gameObject.SetActive(true);
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.GetComponent<RectTransform>(), Input.mousePosition, null, out position);
        caption.GetComponent<RectTransform>().localPosition = new Vector3(position.x, position.y, 0);
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
