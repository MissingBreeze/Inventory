using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WYF.Utils
{
    /// <summary>
    /// 单例类
    /// </summary>
    public class SingletonUtils<T> where T:new ()
    {
        private static T instance { get; set; }

        private static readonly object obj = new object();

        public static T Instance
        {
            get
            {
                if(null == instance)
                {
                    lock (obj)
                    {
                        if(null == instance)
                            instance = new T();
                    }
                }
                return instance;
            }
        }
    }
}
