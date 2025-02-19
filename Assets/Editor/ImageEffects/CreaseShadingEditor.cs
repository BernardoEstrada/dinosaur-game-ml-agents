using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects {
    [CustomEditor(typeof(CreaseShading))]
    internal class CreaseShadingEditor : Editor {
        private readonly AnimBool m_showSoftnessWarning = new();
        private readonly AnimBool m_showSpreadWarning = new();
        private SerializedProperty m_intensity;
        private SerializedProperty m_softness;
        private SerializedProperty m_spread;
        private SerializedObject serObj;

        private bool softnessWarningValue => m_softness.intValue > 4;
        private bool spreadWarningValue => m_spread.floatValue > 4;

        private void OnEnable() {
            serObj = new SerializedObject(target);

            m_intensity = serObj.FindProperty("intensity");
            m_softness = serObj.FindProperty("softness");
            m_spread = serObj.FindProperty("spread");

            m_showSoftnessWarning.valueChanged.AddListener(Repaint);
            m_showSpreadWarning.valueChanged.AddListener(Repaint);

            m_showSoftnessWarning.value = softnessWarningValue;
            m_showSpreadWarning.value = spreadWarningValue;
        }

        public override void OnInspectorGUI() {
            serObj.Update();

            EditorGUILayout.Slider(m_intensity, -5.0f, 5.0f, new GUIContent("Intensity"));

            EditorGUILayout.IntSlider(m_softness, 0, 15, new GUIContent("Softness"));
            m_showSoftnessWarning.target = softnessWarningValue;
            if (EditorGUILayout.BeginFadeGroup(m_showSoftnessWarning.faded))
                EditorGUILayout.HelpBox("High Softness value might reduce performance.", MessageType.Warning, false);
            EditorGUILayout.EndFadeGroup();

            EditorGUILayout.Slider(m_spread, 0.0f, 50.0f, new GUIContent("Spread"));
            m_showSpreadWarning.target = spreadWarningValue;
            if (EditorGUILayout.BeginFadeGroup(m_showSpreadWarning.faded))
                EditorGUILayout.HelpBox("High Spread value might introduce visual artifacts.", MessageType.Warning,
                    false);
            EditorGUILayout.EndFadeGroup();

            serObj.ApplyModifiedProperties();
        }
    }
}