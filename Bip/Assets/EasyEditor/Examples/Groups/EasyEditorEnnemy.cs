using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EasyEditor
{
    /// <summary>
    /// Example class introducing how fields can be organized into groups in Easy Editor. Please, note that groups
    /// need to be declared in the corresponding editor script (EasyEditorEnnemyEditor.cs) with the attribute
    /// [Groups("group 1", "group 2", ...)].
    /// </summary>
    public class EasyEditorEnnemy : MonoBehaviour
    {
        [Image]
        public string easyEditorImage = "Assets/EasyEditor/Examples/icon.png";

    	[Inspector(group = "Game Designer Settings")]
        public float height = 3f;
        public AnimationCurve attackAnimCurve;

    	[Inspector(group = "Basic Settings")]
    	[Comment("this option is not optimized for mobiles.")]
        public bool usePhysic = true;
        public Vector3 initialPosition;
    	
    	[Inspector(group = "Advanced Settings", groupDescription = "These settings can only be tuned by a programmer. Do not change any of these settings.")]
    	[Comment("Target cannot exceed a number of 10.")]
        public List<Bounds> listOfTarget;

    	[Inspector(group = "Game Designer Settings", order = 1)]
        public void GetIntoFuryState()
        {
            GetComponent<Animation>().Play();
        }
    }
}