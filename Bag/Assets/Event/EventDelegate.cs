//--------------------------------------------------------------------------------
// <copyright file="EventDelegate.cs" company="Qishon">
//     Copyright (c) 2016, Xiamen Qishon Technology Co., Ltd. All rights reserved.
// </copyright>
//--------------------------------------------------------------------------------

namespace WYF.Event
{
    /// <summary>
    /// 事件回调委托
    /// </summary>
    /// <param name="eventData">回调的事件数据</param>
    public delegate void EventDelegate<T>(T e) where T : EventData;
}
