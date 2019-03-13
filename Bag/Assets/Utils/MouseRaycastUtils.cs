using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WYF.Utils
{
    /// <summary>
    /// 鼠标射线检测工具类
    /// </summary>
    public class MouseRaycastUtils: SingletonUtils<MouseRaycastUtils>
    {
        /// <summary>
        /// 获取指定类型的射线检测的UI结果
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>结果</returns>
        public List<GameObject> MouseRaycast<T>()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2
                (
#if UNITY_EDITOR
            Input.mousePosition.x, Input.mousePosition.y
#elif UNITY_ANDROID || UNITY_IOS
           Input.touchCount > 0 ? Input.GetTouch(0).position.x : 0, Input.touchCount > 0 ? Input.GetTouch(0).position.y : 0
#endif
            );
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            List<GameObject> gameobjects = new List<GameObject>();
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject.GetComponent<T>() != null)
                    gameobjects.Add(results[i].gameObject);
            }
            return gameobjects;
        }

        /// <summary>
        /// 获取指定标签的射线检测的UI结果
        /// </summary>
        /// <typeparam name="tag">标签</typeparam>
        /// <returns>结果</returns>
        public List<GameObject> MouseRaycast(string tag)
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2
                (
#if UNITY_EDITOR
            Input.mousePosition.x, Input.mousePosition.y
#elif UNITY_ANDROID || UNITY_IOS
           Input.touchCount > 0 ? Input.GetTouch(0).position.x : 0, Input.touchCount > 0 ? Input.GetTouch(0).position.y : 0
#endif
            );
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            List<GameObject> gameobjects = new List<GameObject>();
            for (int i = 0; i < results.Count; i++)
            {
                if (!string.IsNullOrEmpty(results[i].gameObject.tag) && results[i].gameObject.tag == tag)
                    gameobjects.Add(results[i].gameObject);
            }
            return gameobjects;
        }

        /// <summary>
        /// 获取射线检测的UI结果
        /// </summary>
        /// <typeparam name="tag">标签</typeparam>
        /// <returns>结果</returns>
        public List<GameObject> MouseRaycast()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2
                (
#if UNITY_EDITOR
            Input.mousePosition.x, Input.mousePosition.y
#elif UNITY_ANDROID || UNITY_IOS
           Input.touchCount > 0 ? Input.GetTouch(0).position.x : 0, Input.touchCount > 0 ? Input.GetTouch(0).position.y : 0
#endif
            );
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            List<GameObject> gameobjects = new List<GameObject>();
            for (int i = 0; i < results.Count; i++)
            {
                gameobjects.Add(results[i].gameObject);
            }
            return gameobjects;
        }
    }
}
