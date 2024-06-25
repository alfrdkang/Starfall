/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 23/06/2024
 * Date Modified: 23/06/2024
 * Description: Scripts that handles player view bobbing
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PositionFollower))]
public class ViewBobbing : MonoBehaviour
{
    [Header("Bobbing Settings")]
    [SerializeField] private float EffectIntensity;
    [SerializeField] private float EffectIntensityX;
    [SerializeField] private float EffectSpeed;

    private PositionFollower FollowerInstance;
    private Vector3 OriginalOffset;
    private float SinTime;

    // Start is called before the first frame update
    private void Awake()
    {
        FollowerInstance = GetComponent<PositionFollower>();
        OriginalOffset = FollowerInstance.Offset;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 inputVector = new Vector3(Input.GetAxis("Vertical"), 0f, Input.GetAxis("Horizontal"));

        if (inputVector.magnitude > 0f)
        {
            SinTime += Time.deltaTime * EffectSpeed;
        } else
        {
            SinTime = 0f;
        }

        float sinAmountY = -Mathf.Abs(EffectIntensity * Mathf.Sin(SinTime));
        Vector3 sinAmountX = FollowerInstance.transform.right * EffectIntensity * Mathf.Cos(SinTime) * EffectIntensityX;

        FollowerInstance.Offset = new Vector3
        {
            x = OriginalOffset.x,
            y = OriginalOffset.y + sinAmountY,
            z = OriginalOffset.z
        };

        FollowerInstance.Offset += sinAmountX;
    }
}
