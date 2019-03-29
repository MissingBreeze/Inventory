using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using WYF.BaseUIElement.ScrollView.Data;

[Serializable]
public class GoodsDataDto: GoodsDto, IViewData
{
    /// <summary>
    /// 数量
    /// </summary>
    [SerializeField]
    public int count;

    public GoodsDataDto() { }

    public GoodsDataDto(string id, string name, int count, string describe, bool isAdd)
    {
        this.id = id;
        this.name = name;
        this.count = count;
        this.describe = describe;
        this.isAdd = isAdd;
    }
}

