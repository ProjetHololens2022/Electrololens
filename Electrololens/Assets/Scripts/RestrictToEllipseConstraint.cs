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
        const float long_diameter = 0.28f;
        const float small_diameter = 0.18f;
        Vector3 localDir = transform.Position-ellipse.position; //Angle 0
        localDir = Quaternion.Euler(0, ellipse.eulerAngles.y, 0) * localDir; //Angle X
        float equation = Mathf.Pow(localDir.x,2.0f)/Mathf.Pow(long_diameter,2.0f) + Mathf.Pow(localDir.z,2.0f)/Mathf.Pow(small_diameter,2.0f);
        if(equation > 1.0f){
            localDir = Vector3.Normalize(localDir);
            localDir = Quaternion.Euler(0, -2.0f*ellipse.eulerAngles.y, 0) * localDir; //Angle -X
            localDir.x *= long_diameter;
            localDir.z *= small_diameter;
            localDir = Quaternion.Euler(0, ellipse.eulerAngles.y, 0) * localDir; //Angle 0
            transform.Position = ellipse.position + localDir;
        }
    }
}
