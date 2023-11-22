using UnityEditor;
using UnityEngine;

namespace CompanyStats
{
    [CreateAssetMenu(fileName = "InstantEffectPermanent", menuName = "Effects/InstantEffects/Instant Effect Permanent Definition")]
    public class InstantEffectPermanentDefinition : InstantEffectPermanent
    {
        
    }

    [CustomEditor(typeof(InstantEffectPermanentDefinition))]
    public class InstantEffectPermanentDefinitionEditor : Editor
    {
        private SerializedProperty nameProperty;
        private SerializedProperty descriptionProperty;

        private SerializedProperty statAffectedInTargetProperty;
        private SerializedProperty statPartAffectedProperty;
        private SerializedProperty isStatIncrementedProperty;
        private SerializedProperty isValueInPercentageProperty;

        private SerializedProperty valueInPercentageProperty;
        private SerializedProperty valueProperty;

        private void OnEnable()
        {
            nameProperty = serializedObject.FindProperty("effectName");
            descriptionProperty = serializedObject.FindProperty("description");

            statAffectedInTargetProperty = serializedObject.FindProperty("statAffected");
            statPartAffectedProperty = serializedObject.FindProperty("statPart");
            isStatIncrementedProperty = serializedObject.FindProperty("isStatIncremented");
            isValueInPercentageProperty = serializedObject.FindProperty("isValueInPercentage");

            valueInPercentageProperty = serializedObject.FindProperty("valueInPercentage");
            valueProperty = serializedObject.FindProperty("value");

        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(nameProperty);
            EditorGUILayout.PropertyField(descriptionProperty);

            EditorGUILayout.PropertyField(statAffectedInTargetProperty);
            EditorGUILayout.PropertyField(statPartAffectedProperty);
            EditorGUILayout.PropertyField(isStatIncrementedProperty);

            EditorGUILayout.PropertyField(isValueInPercentageProperty);

            if (isValueInPercentageProperty.boolValue)
            {
                EditorGUILayout.PropertyField(valueInPercentageProperty);
            }
            else
            {
                EditorGUILayout.PropertyField(valueProperty);
            }



            serializedObject.ApplyModifiedProperties();
        }
    }
}

