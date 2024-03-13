using System;
using UnityEngine;

namespace CustomAttributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public class ButtonAttribute : PropertyAttribute
    {
        public readonly string MethodName;

        public ButtonAttribute(string methodName)
        {
            this.MethodName = methodName;
        }
    }
}