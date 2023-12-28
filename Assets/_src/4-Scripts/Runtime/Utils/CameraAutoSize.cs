using UnityEngine;

namespace TapSwap.Utils
{
    public class CameraAutoSize : MonoBehaviour
    {
        private void Awake()
        {
            Camera.main.orthographicSize += Camera.main.pixelWidth / (float) Camera.main.pixelHeight;
        }
    }
}

