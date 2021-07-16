using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;



    [CustomEditor(typeof(DamageController))]
    public class DamageEditor : Editor
    {
        static BoxBoundsHandle s_BoxBoundsHandle = new BoxBoundsHandle();
        static Color s_EnabledColor = Color.green + Color.grey;

        SerializedProperty m_DamageProp;
        SerializedProperty m_OffsetProp;
        SerializedProperty m_SizeProp;
        SerializedProperty m_HittableLayersProp;
        SerializedProperty m_CanHitTriggersProp;
        SerializedProperty m_DisableDamageAfterHitProp;
        SerializedProperty m_OnDamageableHitProp;
        SerializedProperty m_OnNonDamageableHitProp;


    void OnEnable ()
        {
            m_DamageProp = serializedObject.FindProperty ("_damage");
            m_OffsetProp = serializedObject.FindProperty("_offset");
            m_SizeProp = serializedObject.FindProperty("_size");
            m_HittableLayersProp = serializedObject.FindProperty("_hittableLayers");
            m_CanHitTriggersProp = serializedObject.FindProperty("_canHitTriggers");
            m_DisableDamageAfterHitProp = serializedObject.FindProperty("_disableDamageAfterHit");
            m_OnDamageableHitProp = serializedObject.FindProperty("_onDamageableHit");
            m_OnNonDamageableHitProp = serializedObject.FindProperty("_onNonDamageableHit");
    }

        public override void OnInspectorGUI ()
        {
            serializedObject.Update ();

            EditorGUILayout.PropertyField(m_DamageProp);
            EditorGUILayout.PropertyField(m_OffsetProp);
            EditorGUILayout.PropertyField(m_SizeProp);
            EditorGUILayout.PropertyField(m_HittableLayersProp);
            EditorGUILayout.PropertyField(m_CanHitTriggersProp);
            EditorGUILayout.PropertyField(m_DisableDamageAfterHitProp);
            EditorGUILayout.PropertyField(m_OnDamageableHitProp);
            EditorGUILayout.PropertyField(m_OnNonDamageableHitProp);


        serializedObject.ApplyModifiedProperties ();
        }

        void OnSceneGUI ()
        {
            DamageController damager = (DamageController)target;

            if (!damager.enabled)
                return;

            Matrix4x4 handleMatrix = damager.transform.localToWorldMatrix;
            handleMatrix.SetRow(0, Vector4.Scale(handleMatrix.GetRow(0), new Vector4(1f, 1f, 0f, 1f)));
            handleMatrix.SetRow(1, Vector4.Scale(handleMatrix.GetRow(1), new Vector4(1f, 1f, 0f, 1f)));
            handleMatrix.SetRow(2, new Vector4(0f, 0f, 1f, damager.transform.position.z));
            using (new Handles.DrawingScope(handleMatrix))
            {
                s_BoxBoundsHandle.center = damager.Offset;
                s_BoxBoundsHandle.size = damager.Size;

                s_BoxBoundsHandle.SetColor(s_EnabledColor);
                EditorGUI.BeginChangeCheck();
                s_BoxBoundsHandle.DrawHandle();
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(damager, "Modify Damager");

                    damager.Size = s_BoxBoundsHandle.size;
                    damager.Offset = s_BoxBoundsHandle.center;
                }
            }
        }
    }
