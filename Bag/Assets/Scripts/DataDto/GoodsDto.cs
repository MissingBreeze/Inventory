using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class GoodsDto
{
    /// <summary>
    /// 物品id
    /// </summary>
    [SerializeField]
    public string id;

    /// <summary>
    /// 物品名称
    /// </summary>
    [SerializeField]
    public string name;

    /// <summary>
    /// 物品说明
    /// </summary>
    [SerializeField]
    public string describe;

    /// <summary>
    /// 是否可以叠加
    /// </summary>
    [SerializeField]
    public bool isAdd;

    [SerializeField]
    public string tag;

    public GoodsDto() { }

    public GoodsDto(string id, string name, string describe, bool isAdd)
    {
        this.id = id;
        this.name = name;
        this.describe = describe;
        this.isAdd = isAdd;
    }
}

