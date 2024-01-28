using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKManager : MonoBehaviour
{
    //root of armature
    public ArmJoint m_root;
    //end effector
    public ArmJoint m_end;
    public GameObject m_target;
    public InferenceController inferenceController; // Assign this in the Unity Editor

    public float m_rate = 5.0f;
    public int m_steps = 24;
    public float m_threshold = 0.05f;

    float CalculateSlope(ArmJoint _joint)
    {
        float deltaTheta = 0.01f;
        float distance1 = GetDistance(m_end.transform.position,m_target.transform.position);

        _joint.Rotate(deltaTheta);

        float distance2 = GetDistance(m_end.transform.position,m_target.transform.position);

        _joint.Rotate(-deltaTheta);

        return(distance2-distance1) / deltaTheta;

    }
    void Update()
    {
         // Get the 2D position of the right wrist from the InferenceController
        Vector2 rightWristPosition2D = inferenceController.GetRightWristPosition2D();

        // Convert 2D position to 3D, setting Z position to be the same as the arm's root Z position
        Vector3 rightWristPosition3D = new Vector3(rightWristPosition2D.x, rightWristPosition2D.y, m_root.transform.position.z);

        // Use rightWristPosition3D as the target position for the IK calculations
        m_target.transform.position = rightWristPosition3D;
        for(int i=0; i< m_steps; ++i)
        {
            if(GetDistance(m_end.transform.position,m_target.transform.position) > m_threshold)
            {
                ArmJoint current = m_root;
                while(current != null)
                {
                    float slope = CalculateSlope(current);
                    current.Rotate(-slope * m_rate);
                    current = current.GetChild();
                }
            }
        }
    }
    float GetDistance(Vector3 _point1, Vector3 _point2)
    {
        return Vector3.Distance(_point1,_point2);
    }
}
