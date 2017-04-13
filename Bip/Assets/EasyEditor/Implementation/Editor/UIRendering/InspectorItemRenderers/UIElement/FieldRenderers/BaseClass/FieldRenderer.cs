//
// Copyright (c) 2016 Easy Editor 
// All Rights Reserved 
//  
//

using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace EasyEditor
{
    /// <summary>
    /// Field renderer is the base class to render fields that Unity renders by default in the inspector.
    /// </summary>
    public class FieldRenderer : InspectorItemRenderer
    {
        /// <summary>
        /// The label of the field.
        /// </summary>
        protected string label = "";
        /// <summary>
        /// The serialized property, which is initialized in the function <c>Render</c>.
        /// </summary>
        protected SerializedProperty serializedProperty = null;
		
		public override void InitializeFromEntityInfo(EntityInfo entityInfo)
		{
			base.InitializeFromEntityInfo (entityInfo);
			Init(ObjectNames.NicifyVariableName(this.entityInfo.fieldInfo.Name));
		}

        public override string GetLabel()
        {
            return label;
        }

        private void Init(string aLabel)
        {
            this.label = aLabel;
        }

        public override void Render(Action preRender = null)
        {
            base.Render(preRender);

            if (serializedProperty == null)
            {
                FindSerializedProperty();
            }
        }

        /// <summary>
        /// Finds the serialized property starting from the root object (the Monobehaviour or the ScriptableObject) and
        /// going down through every composed fields (custom serializable class and struct).
        /// </summary>
        private void FindSerializedProperty()
        {
            serializedProperty = FieldInfoHelper.GetSerializedPropertyFromPath(entityInfo.propertyPath, _serializedObject);

            if (serializedProperty == null)
            {
                string path = FieldInfoHelper.GetFieldInfoPath(entityInfo.fieldInfo, _serializedObject.targetObject.GetType());
                if (!string.IsNullOrEmpty(path))
                {
                    string[] pathTable = path.Split('.');
                    if (pathTable.Length > 0)
                    {
                        serializedProperty = _serializedObject.FindProperty(pathTable [0]);
                        for (int i = 1; i < pathTable.Length; i++)
                        {
                            serializedProperty = serializedProperty.FindPropertyRelative(pathTable [i]);
                        }
                    }
                }
                else
                {
                    Debug.LogWarning("The field info " + entityInfo.fieldInfo.Name + " you initialized this renderer with cannot be fount in the children properties of the target.");
                }
            }
        }
    }
}