using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagManage : MonoBehaviour {

    public int itemCount = 36;

    public Button add;

    private ScrollViewManage scrollViewManage;


    void Start ()
    {
        scrollViewManage = transform.Find("Scroll View").GetComponent<ScrollViewManage>();
        List<GoodsDataDto> dataList = new List<GoodsDataDto>();
        for (int i = 0; i < itemCount; i++)
        {
            GoodsDataDto goodsDataDto = new GoodsDataDto();
            goodsDataDto.name = "item" + i;
            dataList.Add(goodsDataDto);
        }
        scrollViewManage.UpAllData(dataList);
        scrollViewManage.RegisterSliderEvent(SliderTypeEnum.Up, Additem);
        

        add.onClick.AddListener(Additem);
    }

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

    void Update ()
    {
		
	}
}
