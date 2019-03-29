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

    void Awake()
    {
        text = transform.Find("Text").GetComponent<Text>();
        maskImg = transform.Find("maskImg").GetComponent<Image>().gameObject;
        maskImg.SetActive(false);
    }
	
	void Update ()
    {

	}

    public override void LoadData(IViewData viewdata)
    {
        base.LoadData(viewdata);
        goodsDataDto = viewdata as GoodsDataDto;
        
        text.text = goodsDataDto.name;
        transform.GetComponent<Button>().onClick.AddListener(()=> 
        {
            ChangeState(!IsSelect);
        });
    }

    public override void OnStateChange(bool state)
    {
        base.OnStateChange(state);
        if (!state)
        {
            text.text = "取消";
        }
        else
        {
            text.text = "被选中了";
        }
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
