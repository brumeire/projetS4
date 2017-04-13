using UnityEngine;
using System.Collections;

namespace EasyEditor
{
    /// <summary>
    /// Example class showing how a ui element can be hidden based on the value of another field.
    /// </summary>
    public class SetRendererOrder : MonoBehaviour {

    	public enum RenderOrderLabel
    	{
    		Shadow = 1998,
    		Terrain = 1999,
    		MainCharacter = 2001
    	}

        [Image]
        public string easyEditorImage = "Assets/EasyEditor/Examples/icon.png";
        [Space(20f)]

    	public bool useLabel = true;
        [Visibility("useLabel", true)]
    	[SerializeField] private RenderOrderLabel renderOrderLabel = RenderOrderLabel.MainCharacter;
        [Visibility("useLabel", false)]
    	[SerializeField] private int renderOrderValue = 2000;

        void Awake()
        {
            int renderOrder = 0;

            if (useLabel)
            {
                renderOrder = (int)renderOrderLabel;
            }
            else
            {
                renderOrder = renderOrderValue;
            }

            GetComponent<Renderer>().material.renderQueue = renderOrder;
        }
    }
}
