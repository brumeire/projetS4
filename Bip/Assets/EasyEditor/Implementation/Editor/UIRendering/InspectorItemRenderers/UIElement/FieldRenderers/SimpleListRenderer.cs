//
// Copyright (c) 2016 Easy Editor 
// All Rights Reserved 
//  
//

using UnityEngine;
using UnityEditor;
using UEObject = UnityEngine.Object;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using EasyEditor.ReorderableList;

namespace EasyEditor
{
    /// <summary>
    /// Render a list in the inspector. The list UI allows to change element position, or remove them from the list in a very easy way.
    /// </summary>
    [RenderType(typeof(IList))]
    public class SimpleListRenderer : FieldRenderer
    {
        bool checkedIfReadOnly = false;
        bool isReadOnly = false;

        public override void CreateAsset(string path)
        {
            Utils.CreateAssetFrom<SimpleListRenderer>(this, "List_" + label, path);
        }

        public override void Render(Action preRender = null)
        {
            base.Render(preRender);

            if (!checkedIfReadOnly)
            {
                isReadOnly = (AttributeHelper.GetAttribute<ReadOnlyAttribute>(entityInfo.fieldInfo) != null);
                checkedIfReadOnly = true;
            }

            ReorderableListGUI.Title(label);
            if(isReadOnly)
            {
                ReorderableListGUI.ListField(serializedProperty, ReorderableListFlags.DisableReordering 
                                             | ReorderableListFlags.DisableContextMenu 
                                             | ReorderableListFlags.HideAddButton
                                             | ReorderableListFlags.HideRemoveButtons);
            }
            else
            {
                ReorderableListGUI.ListField(serializedProperty);
            }
        }
    }
}