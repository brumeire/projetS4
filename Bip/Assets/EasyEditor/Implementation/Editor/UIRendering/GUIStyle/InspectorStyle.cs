//
// Copyright (c) 2016 Easy Editor 
// All Rights Reserved 
//  
//

using UnityEngine;
using System.Collections;

namespace EasyEditor
{
    /// <summary>
    /// InspectorStyle is a class providing the different styles used to render in the inspector.
    /// </summary>
	public class InspectorStyle {

        public GUIStyle groupHeaderStyle { get ; private set; }
        public GUIStyle groupDescriptionStyle { get ; private set; }

	    private static InspectorStyle defaultStyle;

        public static InspectorStyle DefaultStyle
        {
		    get {
                if (defaultStyle == null)
                {
                    defaultStyle = new InspectorStyle();
			    }

                return defaultStyle;
		    }
	    }
	
	    public InspectorStyle() 
        {
		    groupHeaderStyle = new GUIStyle((GUIStyle) "OL title");
            groupDescriptionStyle = GetTitleDescriptionStyle();;
	    }

        static private GUIStyle GetTitleDescriptionStyle()
        {
            GUIStyle titleDescriptionStyle = new GUIStyle((GUIStyle) "GroupBox");
            titleDescriptionStyle.alignment = TextAnchor.UpperLeft;
            titleDescriptionStyle.padding = new RectOffset(3, 3, 3, 3);
            titleDescriptionStyle.margin = new RectOffset(0, 0, 3, 3);
            titleDescriptionStyle.fontSize = 10;
            titleDescriptionStyle.stretchWidth = true;
            titleDescriptionStyle.wordWrap = true;
            return titleDescriptionStyle;
        }
	}
}