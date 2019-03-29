using System.Collections.Generic;
using UnityEngine;
using WYF.Utils;

/// <summary>
/// 物品管理类
/// </summary>
public static class GoodsDataManage
{
    private static Dictionary<string, GoodsDto> _dataDic;

    public static Dictionary<string, GoodsDto> dataDic
    {
        get
        {
            if (_dataDic == null)
            {
                GetSystemData();
            }
            return _dataDic;
        }
        private set{}
    }
    
    /// <summary>
    /// 从文件中读取系统设定好的物品信息，并缓存在字典内，避免多次重复读取文件
    /// </summary>
    private static void GetSystemData()
    {
        string json = FileOperateUtils.Instance.ReadFile("SystemGoodData.json", "");
        List<GoodsDto> dataList = JsonUtility.FromJson<Serialization<GoodsDto>>(json).ToList();
        if (dataList != null && dataList.Count > 0)
        {
            _dataDic = new Dictionary<string, GoodsDto>();
            for (int i = 0; i < dataList.Count; i++)
            {
                _dataDic.Add(dataList[i].id, dataList[i]);
            }
        }
    }

}
