using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MenuUpgradeMaster : MonoBehaviour
{
    public RotateCircleMen rcm;
    [SerializeField]
    public List<SkillUpgrade> _skiils;
    [SerializeField]

    public SkillUpgrade dog_skill;
    public float PointsUpgrade;
    public TextMeshProUGUI txpro;




    float m=0;
    public void NoUpgrade()
    {
        m = Min_upgrade;
    }
    public float Max_MultPoint, Time_multPoint,Min_upgrade;

    public void UpgradeSkill()
    {
        if(m < Max_MultPoint)
        {
            m += Time.deltaTime * Time_multPoint;
        }
        if(rcm._direitaesquerda== false)
        {
            // cachorro
            _upgradeSkill(dog_skill,m);

        }
        else
        {
            SkillUpgrade temp = _skiils.Find(x => x.bb == rcm.botoes[rcm.index]);
            _upgradeSkill(temp,m);
        }

        txpro.text = PointsUpgrade.ToString();
    }

    private void _upgradeSkill(SkillUpgrade temp,float m)
    {
        float cust =  m;
        Debug.Log(cust + " custo");
        if (PointsUpgrade > cust)
        {
            temp._Exp += cust;


            if (temp._Exp >= temp._ExpMax)
            {
                temp.lvl++;
                temp._Exp = 0;
                temp.bb.LvlSkill = temp.lvl * 3;
                temp.bb.ExpSkill = 0;
                PointsUpgrade -= temp._ExpMax;
                return;
            }
            temp.bb.ExpSkill = temp._Exp / temp._ExpMax;
            PointsUpgrade -= cust;
        }
    }
    private void Start()
    {
        txpro.text = PointsUpgrade.ToString();
    }
}
[System.Serializable]
public class SkillUpgrade
{
    public ButtonBehavior bb;
   // [HideInInspector]
    private int _lvl;
    public int lvl {

        get => _lvl >0 ? _lvl:1;
        set
        {
            _lvl = value;
            _ExpMax = _lvl * 100;
        }
    }
  //  [HideInInspector]
    public float _Exp;
    [HideInInspector]
    public float _ExpMax = 100;
}
