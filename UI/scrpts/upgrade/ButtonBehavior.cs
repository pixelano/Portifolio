using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonBehavior : MonoBehaviour
{
    public Animator anim;


    public float max_velocity, Min_velocity;
    [Range(0,1f)]
    public float Speed_velocity;
    public float velocity;

    public Light _light; 
    public float Max_lumin, Min_lumin;
    [Range(0, 1f)]
    public float speed_Lumin;
    private float limin;

    //

     
    public float ExpSkill { get => _ExpSkill;
        set
        {
            _ExpSkill = value;
            SkillUpdateExp();
        }
    }
 
    public int LvlSkill
    {
        get => _LvlSkill;
        set
        {
            _LvlSkill = value;
            SkillUpdateLvl();
        }
    }

    private float _ExpSkill;
    private int _LvlSkill;

    public Renderer r;
    public Renderer renderRodela;
    private void SkillUpdateExp()
    {
        r.material.SetFloat("_CullOff", ExpSkill);
        renderRodela.material.SetFloat("_Rotate", ExpSkill * 270);
    }
    private void SkillUpdateLvl()
    {
        r.material.SetFloat("_PerlingScale", _LvlSkill);
       
        renderRodela.material.SetFloat("_VeronelScalling", _LvlSkill/3 );
    }

    IEnumerator maxSpeed()
    {

        while( true)
        {
            velocity = Mathf.Lerp(velocity, max_velocity, Speed_velocity) + 0.01f;
            
            yield return null;
            anim.speed = velocity;

            if (velocity >= max_velocity)
                StopCoroutine(SpeedControl);
        }
    }
    IEnumerator minSpeed()
    {

        while (true)
        {
            velocity = Mathf.Lerp(velocity, Min_velocity, Speed_velocity)- 0.01f;

            yield return null;
            anim.speed = velocity;

            if (velocity <= Min_velocity)
                StopCoroutine(SpeedControl);
        }
    }
    IEnumerator max_luminControl()
    {

        while (true)
        {
            limin = Mathf.Lerp(limin,Max_lumin , speed_Lumin)+0.01f;
            _light.intensity = limin;
            yield return null;
            if(limin >= Max_lumin)
                StopCoroutine(LuminControl);
        }
    }
    IEnumerator min_luminControl()
    {

        while (true)
        {
            limin = Mathf.Lerp(limin, Min_lumin, speed_Lumin) - 0.01f;
            _light.intensity = limin;
            yield return null;
            if (limin <=  Min_lumin)
                StopCoroutine(LuminControl);
        }
    }
    Coroutine SpeedControl,LuminControl;
    public void OnPointerExit()
    {
    
        if(SpeedControl == null)
        {
            SpeedControl = StartCoroutine(minSpeed());
        }
        else
        {
            StopCoroutine(SpeedControl);
            SpeedControl = StartCoroutine(minSpeed());
        }
    
        if (LuminControl == null)
        {
            LuminControl = StartCoroutine(min_luminControl());
        }
        else
        {
            StopCoroutine(LuminControl);
            LuminControl = StartCoroutine(min_luminControl());
        }
    }
  
    public void OnPointerEnter()
    {
        if (SpeedControl == null)
        {
            SpeedControl = StartCoroutine(maxSpeed());
        }
        else
        {
            StopCoroutine(SpeedControl);
            SpeedControl = StartCoroutine(maxSpeed());
        }
      
         if (LuminControl == null)
        {
            LuminControl = StartCoroutine(max_luminControl());
        }
        else
        {
            StopCoroutine(LuminControl);
            LuminControl = StartCoroutine(max_luminControl());
        }

    }



}
