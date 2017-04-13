//
// Copyright (c) 2016 Easy Editor 
// All Rights Reserved 
//  
//

using UnityEngine;
using UnityEditor;
using UEObject = UnityEngine.Object;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace EasyEditor
{
    /// <summary>
    /// Core renderer of EasyEditor. After parsing a monobehaviour/scriptableobject and its editor script, 
    /// it generates a list of sub renderer for each fields, methods
    /// and renders it in the inspector.
    /// </summary>
	public class ScriptObjectRenderer : FullObjecRenderer
	{
        private EasyEditorBase editorScript;

        public void Initialize(SerializedObject serializedObject, EasyEditorBase editorScript)
        {
            this.serializedObject = serializedObject;
            this.editorScript = editorScript;

            RetrieveGroupList();
            InitializeRenderersList();
        }

        protected override void RetrieveGroupList()
        {
            GroupsAttribute groupsAttribute = AttributeHelper.GetAttribute<GroupsAttribute>(editorScript.GetType());
            if (groupsAttribute != null)
            {
                groups = new Groups(groupsAttribute.groups);
            }
            else
            {
                groups = new Groups(new string[] { "" });
            }
        }

        /// <summary>
        /// Initializes the renderers list with monobehaviour/scriptableobjects fields and method, 
        /// then with method from the editor script delegate.
        /// </summary>
        override protected void InitializeRenderersList()
        {
            base.InitializeRenderersList();

            renderers = new List<InspectorItemRenderer>();
            
            renderers.AddRange(RendererFinder.GetListOfFields(_serializedObject.targetObject));
            renderers.AddRange(RendererFinder.GetListOfMethods(_serializedObject.targetObject));
            renderers.AddRange(RendererFinder.GetListOfMethods(editorScript));
            
            InspectorItemRendererOrderComparer comparer = new InspectorItemRendererOrderComparer(groups, renderers);
            renderers.Sort(comparer);
        }

		public override void Render (Action preRender = null)
		{
            if (editorScript == null)
            {
                Debug.LogError("You need to set the easyeditor script this renderer is rendering for.");
            }

            EditorGUILayout.BeginVertical();
            
            DrawScriptHeader();
            
            base.Render(preRender);
            
            EditorGUILayout.EndVertical();
		}

        //// <summary>
        /// Gets the message renderer for <c>InspectorItemRenderer</c> with the attribute MessageAttribute.
        /// If the renderer belongs to the editor script, then the method specified in MessageAttribute
        /// is looked for in the editor script.
        /// </summary>
        /// <returns>The message renderer created to render MessageAttribute in the inspector.</returns>
        /// <param name="renderer">An item renderer.</param>
        protected override MessageRenderer GetMessageRenderer(InspectorItemRenderer renderer)
        {
            MessageRenderer result = null;

            MessageAttribute messageAttribute = AttributeHelper.GetAttribute<MessageAttribute>(renderer.entityInfo);
            if(messageAttribute != null)
            {
                object caller = serializedObject.targetObject;

                if(renderer.entityInfo.isMethod)
                {
                    if(typeof(EasyEditorBase).IsAssignableFrom(renderer.entityInfo.methodInfo.DeclaringType))
                    {
                        caller = editorScript;
                    }
                }

                result = new MessageRenderer(renderer, caller, serializedObject.targetObject, renderers.ToArray());
            }   

            return result;
        }

        private void DrawScriptHeader()
        {
            UEObject script = null;

            if (_serializedObject.targetObject is MonoBehaviour)
            {
                script = MonoScript.FromMonoBehaviour((MonoBehaviour)_serializedObject.targetObject);
            }
            else 
            if (_serializedObject.targetObject is ScriptableObject)
            {
                script = MonoScript.FromScriptableObject((ScriptableObject)_serializedObject.targetObject);
            }


            EditorGUILayout.ObjectField("Script", script, typeof(MonoScript), false);
            GUILayout.Space(5f);
        }
	}
}