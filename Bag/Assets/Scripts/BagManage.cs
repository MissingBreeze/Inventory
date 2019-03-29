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

    public Button add1;
    public Button add2;
    public Button save;

    private ScrollViewManage scrollViewManage;

    WYF.BaseUIElement.ToggleGroup.ToggleGroupManage toggleGroup;

    private Image caption;

    private Text captionTxt;

    private List<GoodsDataDto> dataList;

    private void OnDisable()
    {
        EventManager.Instance.RemoveEventListener<ScrollItemEventData>(ScrollItemEventData.DRAG, DragHandler);
    }

    void Start ()
    {
        //List<GoodsDto> data = new List<GoodsDto> { new GoodsDto("1", "1", "1", false) };
        //FileOperateUtils.Instance.WriteFile("SystemGoodData.json", "", JsonUtility.ToJson(new Serialization<GoodsDto>(data)));
        //return;

        caption = transform.Find("Scroll View/caption").GetComponent<Image>();
        captionTxt = caption.transform.Find("captionTxt").GetComponent<Text>();
        caption.gameObject.SetActive(false);

        dataList = new List<GoodsDataDto>();
        string dataStr = FileOperateUtils.Instance.ReadFile("PlayerBag.json", "");
        if(!string.IsNullOrEmpty(dataStr))
            dataList = JsonUtility.FromJson<Serialization<GoodsDataDto>>(dataStr).ToList();

        scrollViewManage = transform.Find("Scroll View").GetComponent<ScrollViewManage>();
        scrollViewManage.UpAllData(dataList);
        //scrollViewManage.RegisterSliderEvent(SliderTypeEnum.Up, Additem);

        add1.onClick.AddListener(Additem1);
        add2.onClick.AddListener(Additem2);
        save.onClick.AddListener(SaveBagData);
        //toggleGroup = transform.Find("ToggleGroup").GetComponent<WYF.BaseUIElement.ToggleGroup.ToggleGroup>();
        EventManager.Instance.AddEventListener<ScrollItemEventData>(ScrollItemEventData.DRAG, DragHandler);
        EventManager.Instance.AddEventListener<ScrollItemEventData>(ScrollItemEventData.DRAGEND, DragEndHandler);
        EventManager.Instance.AddEventListener<ScrollItemEventData>(ScrollItemEventData.SHOW, ShowCaptionHandler);
        EventManager.Instance.AddEventListener<ScrollItemEventData>(ScrollItemEventData.CLOSE, CloseCaptionHandler);
    }

    #region 显示物品说明相关

    private void CloseCaptionHandler(ScrollItemEventData e)
    {
        caption.gameObject.SetActive(false);
    }

    private void ShowCaptionHandler(ScrollItemEventData e)
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.GetComponent<RectTransform>(), Input.mousePosition, null, out position);
        caption.GetComponent<RectTransform>().localPosition = new Vector3(position.x, position.y, 0);
        captionTxt.text = GetShowTxt(e.id);
        if (isDrag) return;
        caption.gameObject.SetActive(true);
    }

    private string GetShowTxt(string id)
    {
        for (int i = 0; i < dataList.Count; i++)
        {
            if(dataList[i].id == id)
            {
                string str = dataList[i].name + "\nX" + dataList[i].count + "\n" + dataList[i].describe;
                return str;
            }
        }
        return string.Empty;
    }

    #endregion

    #region 滑动列表相关

    private bool isDrag = false;

    private void DragEndHandler(ScrollItemEventData e)
    {
        isDrag = false;
        caption.gameObject.SetActive(true);
        isDragScrollItem = false;
        List<GameObject> goList = MouseRaycastUtils.Instance.MouseRaycast("ScrollItem");
        Destroy(item.gameObject);
        if (goList != null && goList.Count != 0)
        {
            ScrollViewItemView scrollViewItemView = goList[0].GetComponent<ScrollViewItemView>();
            if (scrollViewItemView)
            {
                GoodsDataDto goodsDataDto = scrollViewItemView.viewData as GoodsDataDto;
                if (e.id == goodsDataDto.id && goodsDataDto.isAdd)
                {

                }
                scrollViewManage.ExchangeItem<GoodsDataDto>(e.index, scrollViewItemView.Index, dataList);
            }
        }
    }

    private void DragHandler(ScrollItemEventData e)
    {
        isDrag = true;
        caption.gameObject.SetActive(false);
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

    private void DragItem()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.GetComponent<RectTransform>(), Input.mousePosition, null, out position);
        item.localPosition = new Vector3(position.x - item.GetComponent<RectTransform>().sizeDelta.x / 2, position.y, 0);
    }

    #endregion

    private void Additem1()
    {
        List<GoodsDataDto> data= new List<GoodsDataDto>();
        int index = UnityEngine.Random.Range(0, 2);
        GoodsDto goodsDto = GoodsDataManage.dataDic[index.ToString()];
        GoodsDataDto goodsDataDto = new GoodsDataDto(goodsDto.id, goodsDto.name, 1, goodsDto.describe, goodsDto.isAdd);
        dataList.Add(goodsDataDto);
        data.Add(goodsDataDto);
        scrollViewManage.LoadData(data);
    }

    private void Additem2()
    {
        List<GoodsDataDto> data = new List<GoodsDataDto>();
        int index = UnityEngine.Random.Range(2, 4);
        GoodsDto goodsDto = GoodsDataManage.dataDic[index.ToString()];
        GoodsDataDto goodsDataDto = new GoodsDataDto(goodsDto.id, goodsDto.name, 1, goodsDto.describe, goodsDto.isAdd);
        dataList.Add(goodsDataDto);
        data.Add(goodsDataDto);
        scrollViewManage.LoadData(data);
    }

    private void SaveBagData()
    {
        string json = JsonUtility.ToJson(new Serialization<GoodsDataDto>(dataList));
        FileOperateUtils.Instance.WriteFile("PlayerBag.json", "", json);
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
