using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using static UnityEditor.EditorGUILayout;
using static UnityEditor.EditorGUIUtility;

namespace LeTai.TrueShadow.Editor
{
[CanEditMultipleObjects]
[CustomEditor(typeof(ProjectSettings))]
public class ProjectSettingsEditor : UnityEditor.Editor
{
    EditorProperty  useGlobalAngleByDefault;
    EditorProperty  globalAngle;
    EditorProperty  showQuickPresetsButtons;
    EditorProperty  quickPresets;
    ReorderableList reorderableList;

    void OnEnable()
    {
        useGlobalAngleByDefault = new EditorProperty(serializedObject, nameof(ProjectSettings.UseGlobalAngleByDefault));
        globalAngle             = new EditorProperty(serializedObject, nameof(ProjectSettings.GlobalAngle));
        showQuickPresetsButtons = new EditorProperty(serializedObject, nameof(ProjectSettings.ShowQuickPresetsButtons));
        quickPresets            = new EditorProperty(serializedObject, nameof(ProjectSettings.QuickPresets));

        reorderableList = new ReorderableList(serializedObject, quickPresets.serializedProperty, true, true, true, true) {
            drawHeaderCallback  = DrawPresetsHListHeader,
            drawElementCallback = DrawPresetListItems,
            elementHeight = singleLineHeight * 6
                          + standardVerticalSpacing * 7,
        };
    }

    void DrawPresetsHListHeader(Rect rect)
    {
        EditorGUI.PrefixLabel(rect, new GUIContent(quickPresets.serializedProperty.displayName));
    }

    void DrawPresetListItems(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = reorderableList.serializedProperty.GetArrayElementAtIndex(index);

        var childRect = new Rect(rect) { height = singleLineHeight };
        EditorGUI.LabelField(childRect, element.FindPropertyRelative(nameof(QuickPreset.name)).stringValue);
        childRect.y += singleLineHeight + standardVerticalSpacing;

        var oldLabelWidth = labelWidth;
        labelWidth = Mathf.Min(labelWidth, pixelsPerPoint * 60);
        foreach (var childProp in element)
        {
            EditorGUI.PropertyField(childRect, (SerializedProperty)childProp, true);
            childRect.y += singleLineHeight + standardVerticalSpacing;
        }

        labelWidth = oldLabelWidth;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        useGlobalAngleByDefault.Draw();
        globalAngle.Draw();
        Space();
        showQuickPresetsButtons.Draw();

        using (new GUILayout.HorizontalScope())
        {
            Space(pixelsPerPoint * 8, false);
            using (new GUILayout.VerticalScope(GUILayout.MaxWidth(pixelsPerPoint * 400)))
                reorderableList.DoLayoutList();
            Space(pixelsPerPoint * 8, false);
        }

        serializedObject.ApplyModifiedProperties();
    }

    [SettingsProvider]
    public static SettingsProvider CreatSettingsProvider()
    {
        return AssetSettingsProvider.CreateProviderFromResourcePath(
            "Project/True Shadow",
            ProjectSettings.RESOURCE_PATH,
            SettingsProvider.GetSearchKeywordsFromPath(ProjectSettings.RESOURCE_PATH)
        );
    }
}
}
