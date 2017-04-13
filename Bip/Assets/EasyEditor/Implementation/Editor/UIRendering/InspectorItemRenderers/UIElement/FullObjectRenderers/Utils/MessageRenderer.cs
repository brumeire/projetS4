//
// Copyright (c) 2016 Easy Editor 
// All Rights Reserved 
//  
//

using UnityEngine;
using UEObject = UnityEngine.Object;
using UnityEditor;
using System.Collections;
using System;
using System.Reflection;

namespace EasyEditor
{
    public class MessageRenderer
    {
        string text = "";
        UnityEditor.MessageType messageType;
        string method = "";
        string id = "";
        object value;

        InspectorItemRenderer[] otherRenderers;
        object caller;
        object classFieldBelongTo;

        public void Render()
        {
            bool renderMessage = true;

            if (!string.IsNullOrEmpty(method))
            {
                MethodInfo methodInfo = caller.GetType().GetMethod(method, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                if(methodInfo != null)
                {
                    if(methodInfo.ReturnType == typeof(bool))
                    {
                        renderMessage = (bool) methodInfo.Invoke(caller, null);
                    }
                    else
                    {
                        Debug.LogError("The method specified in the attribute Message have to return a bool.");
                    }
                }
                else
                {
                    Debug.LogError("The method specified in the attribute Message does not exist.");
                }
            }
            else if(!string.IsNullOrEmpty(id) && value != null)
            {
                InspectorItemRenderer conditionalRenderer = LookForRenderer(id);
                if(conditionalRenderer != null && conditionalRenderer.entityInfo.isField)
                {
                    if(!value.Equals(conditionalRenderer.entityInfo.fieldInfo.GetValue(classFieldBelongTo)))
                    {
                        renderMessage = false;
                    }
                }
                else
                {
                    Debug.LogWarning("The identifier " + id + " was not found in the list of renderers, or this renderer " +
                                     "was not initialized from a field. Ensure that the id parameter of the attribute Visibility refers to the id of a field " +
                                     "(name of the field if you did not specify explicitly the id of the field in [Inspector(id = \"...\").");
                }
            }

            if (renderMessage)
            {
                if(!string.IsNullOrEmpty(text))
                {
                    EditorGUILayout.HelpBox(text, messageType, true);
                }
            }
        }

        public MessageRenderer(InspectorItemRenderer inspectorItemRenderer, object caller, object classFieldBelongTo, InspectorItemRenderer[] otherRenderers = null)
        {
            MessageAttribute messageAttribute = AttributeHelper.GetAttribute<MessageAttribute>(inspectorItemRenderer.entityInfo);
            this.text = messageAttribute.text;
            this.method = messageAttribute.method;
            this.id = messageAttribute.id;
            this.value = messageAttribute.value;
            this.caller = caller;
            this.classFieldBelongTo = classFieldBelongTo;

            switch (messageAttribute.messageType)
            {
                case MessageType.Info:
                    this.messageType = UnityEditor.MessageType.Info;
                    break;
                case MessageType.Warning:
                    this.messageType = UnityEditor.MessageType.Warning;
                    break;
                case MessageType.Error:
                    this.messageType = UnityEditor.MessageType.Error;
                    break;
            }

            this.otherRenderers = otherRenderers;
        }

        /// <summary>
        /// Looks for renderer in the renderers list based on an id. The default id is the field or the method name of a renderer.
        /// But this id can be modified with Inspector attribute.
        /// </summary>
        /// <returns>The for renderer.</returns>
        /// <param name="rendererId">Renderer identifier.</param>
        private InspectorItemRenderer LookForRenderer(string rendererId)
        {
            InspectorItemRenderer result = null;
            foreach (InspectorItemRenderer renderer in otherRenderers)
            {
                if (renderer.GetIdentifier() == rendererId)
                {
                    result = renderer;
                    break;
                }
            }
            
            return result;
        }
    }
}