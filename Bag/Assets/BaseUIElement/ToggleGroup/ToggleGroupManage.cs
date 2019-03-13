using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace WYF.BaseUIElement.ToggleGroup
{
    public class ToggleGroupManage : MonoBehaviour
    {
        private List<Toggle> toggleList = new List<Toggle>();

        private void Awake()
        {
            foreach (Transform item in transform)
            {
                if (item.GetComponent<Toggle>() != null)
                {
                    toggleList.Add(item.GetComponent<Toggle>());
                }
            }
        }

        /// <summary>
        /// 当前选中的Toggle
        /// </summary>
        public string fileValue
        {
            get
            {
                for (int i = 0; i < toggleList.Count; i++)
                {
                    if (toggleList[i].isOn)
                    {
                        return toggleList[i].name;
                    }
                }
                return string.Empty;
            }
            set
            {
                for (int i = 0; i < toggleList.Count; i++)
                {
                    if (toggleList[i].name == value)
                    {
                        toggleList[i].isOn = true;
                    }
                }
            }
        }

        /// <summary>
        /// 添加Toggle值改变事件监听
        /// </summary>
        /// <param name="action"></param>
        public void AddValueChangeEvent(UnityAction<bool> action)
        {
            for (int i = 0; i < toggleList.Count; i++)
            {
                toggleList[i].onValueChanged.AddListener(action);
            }
        }
    }
}

