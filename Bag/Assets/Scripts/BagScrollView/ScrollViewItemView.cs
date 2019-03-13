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

public class ScrollViewItemView : ScrollViewListItem,IBeginDragHandler,IEndDragHandler, IDragHandler
{
    private GoodsDataDto goodsDataDto;


    void Start ()
    {
		
	}
	
	void Update ()
    {

	}

    public override void LoadData(IViewData viewdata)
    {
        base.LoadData(viewdata);
        goodsDataDto = viewdata as GoodsDataDto;
        Text text = transform.Find("Text").GetComponent<Text>();
        text.text = goodsDataDto.name;
        transform.GetComponent<Button>().onClick.AddListener(()=> 
        {
            ChangeState(!IsSelect);
            text.text = "被选中了";
        });

        
    }

    public override void OnStateChange(bool state)
    {
        base.OnStateChange(state);
        if (!state)
        {
            Text text = transform.Find("Text").GetComponent<Text>();
            text.text = "取消";
        }

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.LogError("开始");
        ScrollItemEventData scrollItemEventData = new ScrollItemEventData(ScrollItemEventData.DRAG, Index, gameObject, goodsDataDto);
        EventManager.Instance.SendEventData(ScrollItemEventData.DRAG, scrollItemEventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.LogError("结束");
    }

    public void OnDrag(PointerEventData eventData)
    {
        

    }
}
