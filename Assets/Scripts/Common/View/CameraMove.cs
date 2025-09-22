using UnityEngine;

namespace Assets.Scripts.Common.View
{
    public class CameraMove : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float offsetY;

        private void Update()
        {
            transform.position = new Vector3(target.position.x, target.position.y + offsetY, transform.position.z);
        }
    }
}
