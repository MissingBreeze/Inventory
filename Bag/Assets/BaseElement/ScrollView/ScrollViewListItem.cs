using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollViewListItem : BaseScrollListItem
{
    /// <summary>
    /// 当前子项索引
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// 是否选中
    /// </summary>
    public bool IsSelect { get; set; }

    /// <summary>
    /// 子项数据
    /// </summary>
    public IViewData viewData { get; set; }

    public Action<int,bool> stateChange;

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    public virtual void LoadData(IViewData viewdata)
    {
        viewData = viewdata;
    }

    /// <summary>
    /// 修改子项状态
    /// </summary>
    /// <param name="state"></param>
    public void ChangeState(bool state)
    {
        stateChange(Index, state);
    }


    public virtual void OnStateChange(bool state)
    {
    }

    public sealed override void SetState(bool state)
    {
        IsSelect = state;
        OnStateChange(state);
    }
}
