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
        Vector3 localDir = transform.Position-ellipse.position; //Angle 0
        localDir = Quaternion.Euler(0, ellipse.eulerAngles.y, 0) * localDir; //Angle X
        //PROBLEME ICI
        float equation = Mathf.Pow(localDir.x,2.0f)/Mathf.Pow(0.3f,2.0f) + Mathf.Pow(localDir.z,2.0f)/Mathf.Pow(0.2f,2.0f);
        if(equation > 1.0f){
            localDir = Vector3.Normalize(localDir);
            localDir = Quaternion.Euler(0, -ellipse.eulerAngles.y, 0) * localDir; //Angle 0
            localDir = Quaternion.Euler(0, -ellipse.eulerAngles.y, 0) * localDir; //Angle 0
            localDir.x *= 0.3f;
            localDir.z *= 0.2f;
            localDir = Quaternion.Euler(0, ellipse.eulerAngles.y, 0) * localDir; //Angle X
            transform.Position = ellipse.position + localDir;
        }
    }
}
