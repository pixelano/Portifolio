using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RotateCircleMen : MonoBehaviour
{
 
    private int _index = 1;
   
    public Animator anim;
   

    private void UpRotate()
    {
        Debug.Log(anim.GetCurrentAnimatorStateInfo(0).normalizedTime / anim.GetCurrentAnimatorStateInfo(0).length);
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime / anim.GetCurrentAnimatorStateInfo(0).length < 0.98f)
            return;

     
      if(_index >= 6)
        {
         
            Debug.Log(_index + "-" + ("1"));
            anim.Play(_index + "-" + ("1"));
            _index = 1;
            return;
        }
        Debug.Log(_index + "-" + ((_index + 1).ToString()));
            
        anim.Play(_index +"-"+((_index + 1).ToString()));

        _index++;
       


    }
    private void DownRotate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime / anim.GetCurrentAnimatorStateInfo(0).length < 0.98f)
            return;
        
        if (_index <= 1)
        {
            anim.Play(_index + "-" + ("6"));
            _index = 6;
            return;
        }

        anim.Play(_index + "-" + ((_index - 1).ToString()));

        _index--;
    }

    public List<ButtonBehavior> botoes;
    public ButtonBehavior dog;
    public bool _direitaesquerda = true;
    bool direitaEsquerda
    {
        get => _direitaesquerda;
        set
        {
            if (_direitaesquerda != value)
            {
                _direitaesquerda = value;
                SideShang(_direitaesquerda);
            }
           
        }
    }
    public int index=0;

    private void Start()
    {
        botoes[0].OnPointerEnter();
    }
    public void UpDown(bool a)
    {
        if (direitaEsquerda)
        {
            botoes[index].OnPointerExit();

            if (a)
            {
                UpRotate();
            }
            else
            {
                DownRotate();
            }
            index += a ? 1 : -1;
            index = index > 5 ? 0 : index;
            index = index < 0 ? 5 : index;
           
            botoes[index].OnPointerEnter();

           
        }
    }
    public void LefRig(bool a)
    {
        direitaEsquerda = a;
    }
    public void SideShang( bool a)
    {
        if (a)
        {
            dog.OnPointerExit();
            botoes[index].OnPointerEnter();
        }
        else
        {
            dog.OnPointerEnter();
            botoes[index].OnPointerExit();
        }
    }


}
