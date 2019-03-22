using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WYF.Event;
using UnityEngine;

namespace WYF.DataDto
{
    public class ScrollItemEventData: EventData
    {

        public static readonly string DRAG = "drag";

        public static readonly string DRAGEND = "dragend";

        /// <summary>
        /// 当前选择的子项物体
        /// </summary>
        public GameObject itemGo { get; set; }

        /// <summary>
        /// 子项数据
        /// </summary>
        public GoodsDataDto goodsDataDto { get; set; }

        /// <summary>
        /// 事件类型
        /// </summary>
        public string eventType { get; set; }

        /// <summary>
        /// 子项索引
        /// </summary>
        public int index { get; set; }

        public ScrollItemEventData(string eventType, int index, GameObject itemGo, GoodsDataDto goodsDataDto)
        {
            this.itemGo = itemGo;
            this.goodsDataDto = goodsDataDto;
            this.eventType = eventType;
            this.index = index;
        }

        public ScrollItemEventData(string eventType)
        {

        }
    }
}
