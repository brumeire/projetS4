using UnityEngine;

namespace EasyEditor
{
    public enum Compatibility
    {
        iOS             = 0x001,
        Android         = 0x002,
        WindowsPhone    = 0x004,
        PS4             = 0x008,
        Wii             = 0x010,
        XBoxOne         = 0x020,
        WindowsDesktop  = 0x040,
        Linux           = 0x080,
        MacOS           = 0x100
    }

    public class MaskEnumExample : MonoBehaviour
    {
        [Image]
        public string easyEditorImage = "Assets/EasyEditor/Examples/icon.png";
        [Space(20f)]

    	[EnumFlag]
        [SerializeField] Compatibility compatibleDevice;

    	[Inspector]
        public void TestCompatibilityWithMacOS()
        {
            int compatible = (int)compatibleDevice & (int)Compatibility.MacOS;
            Debug.Log((compatible == 0) ? "Not compatible" : "Compatible");
        }
    }
}