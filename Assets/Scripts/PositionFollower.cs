/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 22/06/2024
 * Date Modified: 22/06/2024
 * Description: Position Following Script for WeaponSway
 */

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
