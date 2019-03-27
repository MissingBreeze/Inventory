using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using WYF.BaseUIElement.ScrollView.Data;

[Serializable]
public class GoodsDataDto: IViewData
{
    /// <summary>
    /// 物品id
    /// </summary>
    [SerializeField]
    public string id;

    /// <summary>
    /// 名称
    /// </summary>
    [SerializeField]
    public string name;

    /// <summary>
    /// 数量
    /// </summary>
    [SerializeField]
    public string count;

    public GoodsDataDto() { }

    public GoodsDataDto(string id, string name, string count)
    {
        this.id = id;
        this.name = name;
        this.count = count;
    }
}

