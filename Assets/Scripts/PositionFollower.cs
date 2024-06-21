using UnityEngine;

public class PositionFollower : MonoBehaviour
{
    public Transform TargetTransform;
    public Vector3 Offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = TargetTransform.position + Offset;
        transform.rotation = TargetTransform.rotation;
    }
}
