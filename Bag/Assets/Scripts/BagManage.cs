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

    private void OnDisable()
    {
        EventManager.Instance.RemoveEventListener<ScrollItemEventData>(ScrollItemEventData.DRAG, DragHandler);
    }

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

        //toggleGroup = transform.Find("ToggleGroup").GetComponent<WYF.BaseUIElement.ToggleGroup.ToggleGroup>();
        EventManager.Instance.AddEventListener<ScrollItemEventData>(ScrollItemEventData.DRAG, DragHandler);

    }

    private void DragHandler(ScrollItemEventData e)
    {
        Debug.LogError("收到Begin");

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
        if (Input.GetMouseButtonUp(0))
        {
//            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
//            eventDataCurrentPosition.position = new Vector2
//                (
//#if UNITY_EDITOR
//            Input.mousePosition.x, Input.mousePosition.y
//#elif UNITY_ANDROID || UNITY_IOS
//           Input.touchCount > 0 ? Input.GetTouch(0).position.x : 0, Input.touchCount > 0 ? Input.GetTouch(0).position.y : 0
//#endif
//            );
//            List<RaycastResult> results = new List<RaycastResult>();
//            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
//            for (int i = 0; i < results.Count; i++)
//            {
//                if (results[i].gameObject.GetComponent<ScrollViewItemView>())
//                {
//                    GoodsDataDto goodsDataDto = results[i].gameObject.GetComponent<ScrollViewItemView>().viewData as GoodsDataDto;
//                    Debug.LogError(goodsDataDto.name);
//                }
//            }

        }
        if (Input.GetMouseButtonDown(0))
        {
            Debug.LogError(MouseRaycastUtils.Instance.MouseRaycast()[0].name);
        }
    }
}
