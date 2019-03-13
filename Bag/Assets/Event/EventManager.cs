using System.Collections.Generic;
using UnityEngine;
using WYF.Utils;

namespace WYF.Event
{
    /// <summary>
    /// 事件管理类
    /// </summary>
    public class EventManager: SingletonUtils<EventManager>
    {
        /// <summary>
        /// 委托字典
        /// </summary>
        private Dictionary<string, System.Delegate> dictionary = new Dictionary<string, System.Delegate>();

        /// <summary>
        /// 添加事件监听
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="eventKey">事件key</param>
        /// <param name="eventDelegate">回调方法</param>
        public void AddEventListener<T>(string eventKey, EventDelegate<T> eventDelegate) where T : EventData
        {
            if (!dictionary.ContainsKey(eventKey))
            {
                dictionary.Add(eventKey, null);
            }
            EventDelegate<T> listeners = (EventDelegate<T>)dictionary[eventKey];
            listeners += eventDelegate;
            dictionary[eventKey] = listeners;
        }

        /// <summary>
        /// 移除事件监听
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="eventKey">事件key</param>
        /// <param name="eventDelegate">回调方法</param>
        public void RemoveEventListener<T>(string eventKey, EventDelegate<T> eventDelegate) where T: EventData
        {
            if (!dictionary.ContainsKey(eventKey))
            {
                Debug.LogError("未添加" + eventKey + "事件监听");
                return;
            }
            EventDelegate<T> listeners = (EventDelegate<T>)dictionary[eventKey];
            listeners -= eventDelegate;
            dictionary[eventKey] = listeners;
            if(dictionary[eventKey] == null)
            {
                dictionary.Remove(eventKey);
            }
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="eventKey">事件key</param>
        /// <param name="eventData">事件数据</param>
        public void SendEventData<T>(string eventKey, T eventData) where T: EventData
        {
            if (!dictionary.ContainsKey(eventKey))
            {
                Debug.LogError("未添加" + eventKey + "事件监听");
                return;
            }
            ((EventDelegate<T>)dictionary[eventKey])(eventData);
        }
    }
}
