using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private float distanceMin = 0.1f;

    // Update is called once per frame
    void Update()
    {
        // 1. ターゲットとの距離を求める
        var distance = Vector3.Distance(transform.position, target.position);
        // 2. ターゲットとの距離が一定以上なら、ターゲットに向かって移動する
        if (distance > distanceMin)
        {
            // 3. ターゲットの方向を求める
            var direction = (target.position - transform.position).normalized;
            // 4. ターゲットの方向に移動する
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
