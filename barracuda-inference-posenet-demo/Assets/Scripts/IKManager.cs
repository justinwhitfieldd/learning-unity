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
