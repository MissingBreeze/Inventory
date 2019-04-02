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

public class ScrollViewItemView : ScrollViewListItem,IBeginDragHandler,IEndDragHandler, IDragHandler,IPointerEnterHandler,IPointerExitHandler
{
    private GoodsDataDto goodsDataDto;

    private Text text;

    private GameObject maskImg;

    /// <summary>
    /// 是否允许选中
    /// </summary>
    private bool canSelect = false;

    void Awake()
    {
        text = transform.Find("Text").GetComponent<Text>();
        maskImg = transform.Find("maskImg").GetComponent<Image>().gameObject;
        maskImg.SetActive(false);
        EventManager.Instance.AddEventListener<ScrollItemEventData>(ScrollItemEventData.OPEN, OpenSelectHandler);
        EventManager.Instance.AddEventListener<ScrollItemEventData>(ScrollItemEventData.SHUT, ShutSelectHandler);
    }

    private void ShutSelectHandler(ScrollItemEventData e)
    {
        canSelect = false;
        ChangeState(false);
    }

    private void OpenSelectHandler(ScrollItemEventData e)
    {
        canSelect = true;
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveEventListener<ScrollItemEventData>(ScrollItemEventData.OPEN, OpenSelectHandler);
        EventManager.Instance.RemoveEventListener<ScrollItemEventData>(ScrollItemEventData.SHUT, ShutSelectHandler);
    }

    void Update ()
    {
	}

    /// <summary>
    /// 物品可叠加时添加数量
    /// </summary>
    public void AddCount(int count)
    {
        goodsDataDto.count += count;
        text.text = goodsDataDto.name + "\n" + goodsDataDto.count;
    }

    public override void LoadData(IViewData viewdata)
    {
        base.LoadData(viewdata);
        goodsDataDto = viewdata as GoodsDataDto;
        
        text.text = goodsDataDto.name + "\n" + goodsDataDto.count;
        transform.GetComponent<Button>().onClick.AddListener(()=> 
        {
            if(canSelect)
                ChangeState(!IsSelect);
        });
    }

    public override void OnStateChange(bool state)
    {
        base.OnStateChange(state);
        maskImg.SetActive(state);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ScrollItemEventData scrollItemEventData = new ScrollItemEventData(ScrollItemEventData.DRAG, Index, gameObject, goodsDataDto);
        EventManager.Instance.SendEventData(ScrollItemEventData.DRAG, scrollItemEventData);
        maskImg.SetActive(true);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EventManager.Instance.SendEventData(ScrollItemEventData.DRAGEND, new ScrollItemEventData(ScrollItemEventData.DRAGEND, Index, goodsDataDto));
        maskImg.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventManager.Instance.SendEventData(ScrollItemEventData.SHOW, new ScrollItemEventData(ScrollItemEventData.SHOW, goodsDataDto.id));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EventManager.Instance.SendEventData(ScrollItemEventData.CLOSE, new ScrollItemEventData(ScrollItemEventData.CLOSE));
    }
}
