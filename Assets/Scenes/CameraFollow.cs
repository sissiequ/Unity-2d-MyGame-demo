using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;     // 玩家
    public float smoothSpeed = 5f;
    public Vector3 offset;       // 相机偏移

    void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(
            target.position.x + offset.x,
            offset.y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}
