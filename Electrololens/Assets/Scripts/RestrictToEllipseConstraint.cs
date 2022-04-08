using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

public class RestrictToEllipseConstraint : TransformConstraint
{

    public override TransformFlags ConstraintType => TransformFlags.Move;

    private Transform ellipse = null;

    public override void Initialize(MixedRealityTransform worldPose)
    {
        base.Initialize(worldPose);
        ellipse = transform.parent;
    }

    public override void ApplyConstraint(ref MixedRealityTransform transform)
    {
        if(ellipse == null){
            return;
        }
        float equation = Mathf.Pow(transform.Position.x-ellipse.position.x,2.0f)/Mathf.Pow(0.3f,2.0f) + Mathf.Pow(transform.Position.z-ellipse.position.z,2.0f)/Mathf.Pow(0.2f,2.0f);
        Debug.Log(equation);
        if(equation > 1.0f){
            Vector3 dir = transform.Position-ellipse.position;
            dir = Vector3.Normalize(dir);
            dir.x *= 0.3f;
            dir.z *= 0.2f;
            transform.Position = ellipse.position + dir;
        }
    }
}
