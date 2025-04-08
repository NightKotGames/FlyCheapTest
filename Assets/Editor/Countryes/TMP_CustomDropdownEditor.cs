
using UnityEditor;
using UnityEngine;
using Countries.ForAnimate;

[CustomEditor(typeof(TMP_CustomDropdown))]

public class TMP_CustomDropdownEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TMP_CustomDropdown dropdown = (TMP_CustomDropdown)target;

        serializedObject.Update();

        // Указываем явно, какие свойства отображать
        SerializedProperty property = serializedObject.FindProperty("_selectorBackground");
        EditorGUILayout.PropertyField(property, new GUIContent("Selector Background"));

        property = serializedObject.FindProperty("_elementHeight");
        EditorGUILayout.PropertyField(property, new GUIContent("Element Height"));

        property = serializedObject.FindProperty("_borderOffset");
        EditorGUILayout.PropertyField(property, new GUIContent("Border Offset"));

        property = serializedObject.FindProperty("_animationDuration");
        EditorGUILayout.PropertyField(property, new GUIContent("Animation Duration"));

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Standard TMP_Dropdown Settings", EditorStyles.boldLabel);

        // Рендерим свойства TMP_Dropdown вручную
        SerializedProperty dropdownProperty = serializedObject.GetIterator();
        dropdownProperty.NextVisible(true); // Пропускаем "m_Script"

        while (dropdownProperty.NextVisible(false))
        {
            if (dropdownProperty.name != "_selectorBackground" &&
                dropdownProperty.name != "_elementHeight" &&
                dropdownProperty.name != "_borderOffset" &&
                dropdownProperty.name != "_animationDuration")
            {
                EditorGUILayout.PropertyField(dropdownProperty, true);
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
