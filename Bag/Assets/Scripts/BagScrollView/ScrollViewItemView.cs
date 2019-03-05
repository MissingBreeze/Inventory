using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewItemView : ScrollViewListItem
{

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    public override void LoadData(IViewData viewdata)
    {
        base.LoadData(viewdata);
        GoodsDataDto goodsDataDto = viewdata as GoodsDataDto;
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
}
