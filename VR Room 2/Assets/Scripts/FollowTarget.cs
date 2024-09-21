using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    Vector3 offset;
    public Transform target;
    Vector3 transformRotation;

    [Range(-1, 3)]
    public float scale = 1;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
        //we need to keep track of the original follower roation
        transformRotation = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        //need to consider the follower and target rotations. So combine their rotation together.
        Vector3 targetRotation = target.rotation.eulerAngles;
        targetRotation += transformRotation;
        Quaternion theTargetRotation = Quaternion.Euler(targetRotation);

        //make the follower maintain the same distance from the target while following the target
        transform.position = target.position + target.rotation * offset * scale;

        //make the follower look at the same direction as the target (aka orginal follower rotation + target rotation)
        transform.rotation = theTargetRotation;
    }
}
