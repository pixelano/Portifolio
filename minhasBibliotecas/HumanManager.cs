using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanManager : MonoBehaviour
{
    public Animator anime;
    public LayerMask layermask;
    [Range(0f,1f)]
    public float distoGround;
    private void Awake()
    {
        anime = GetComponent<Animator>();
    }
    private void OnAnimatorIK(int layerIndex)
    {
        if (anime)
        {
         
            anime.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
            anime.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);

            RaycastHit hit;
            Ray ray = new Ray(anime.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);
            if(Physics.Raycast(ray,out hit, distoGround + 1, layermask))
            {
              
                if (hit.transform.tag == "walkable")
                {
                   
                    Vector3 footposition = hit.point;
                    footposition.y += distoGround;
                    anime.SetIKPosition(AvatarIKGoal.LeftFoot, footposition);
                    anime.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward,hit.normal));
                }
            }

        }
    }
}
