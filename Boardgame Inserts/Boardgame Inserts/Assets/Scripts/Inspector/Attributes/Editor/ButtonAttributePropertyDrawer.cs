using CustomAttributes;
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ButtonAttribute))]
public class ButtonAttributePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ButtonAttribute component = this.attribute as ButtonAttribute;
        if (GUILayout.Button(component.MethodName))
        {
            Type type = property.serializedObject.targetObject.GetType();
            MethodInfo method = type.GetMethod(component.MethodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            method.Invoke(property.serializedObject.targetObject, null);
        }
    }
}
