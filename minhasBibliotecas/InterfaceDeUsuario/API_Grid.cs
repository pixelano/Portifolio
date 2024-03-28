    using System.Collections;
    using System.Collections.Generic;
    using Unity.VisualScripting;
    using UnityEngine;

    public class API_Grid : MonoBehaviour
    {

    public bool itensEmpilhaveis=false;
    public bool SlotEspancivos = false;
    public int limiteDeStack = 64;


    public List<inventarioSlot> inventario;
          [System.Serializable]
        public class inventarioSlot{
        public int id_;
        public int id_stack;
        public int quantidade;
        public DragAndDrop obj_;
        public void attQ() {

        obj_.attQ(quantidade);
        }
        

    }
    public bool TeveAlteracao = false;  
    public inventarioSlot clonar( inventarioSlot a)
    {
        inventarioSlot x = new inventarioSlot();
        x.id_ = a.id_;
        x.id_stack = a.id_stack;
        x.quantidade = a.quantidade;
        x.obj_ = a.obj_;
        return x;

    }
    public inventarioSlot temEsteItem(DragAndDrop k){
        inventarioSlot tempb = null;

    int id_ = k.ID_item;int id_Stack = k.ID_pack;
                foreach(var a in inventario){
                    if(a.id_ == id_ && a.id_stack == id_Stack){
                        tempb = a;
                        break;
                    }
                }

        return tempb;
    }
    public inventarioSlot acheEsteItem(int x)
    {
        inventarioSlot tempb = null;

        foreach(var a in inventario)
        {
            if(a.id_stack == x)
            {
                tempb = a;
                break;
            }
        }


        return tempb;
    }
    public bool temEstaQuantidade(int id_, int quantidade)
    {
        inventarioSlot x = acheEsteItem(id_);

        if(x != null)
        {
            return x.quantidade >= quantidade;
        }
        return false;
    }

        public int adicionarEsteItem(DragAndDrop item,int inc_slot){
            int idPack = -1;
        if(itensEmpilhaveis == false){
                inventarioSlot temp_ =new inventarioSlot();
                temp_.id_stack = inc_slot;
            temp_.obj_ = item;
                temp_.id_ = item.ID_item;
            idPack = temp_.id_stack;

            int qq = item.origem.temEsteItem(item).quantidade;
                temp_.quantidade = qq;
                inventario.Add(temp_);
            temp_.attQ();
                
            
        }
        else{

        inventarioSlot temp = null;

            foreach(var a in inventario){
                if(a.id_stack == inc_slot )
                {
                    temp = a;
                    break;
                }
            }
            if(temp != null && temp.quantidade <= limiteDeStack){
                temp.quantidade += item.origem.temEsteItem(item).quantidade;
                temp.attQ();
            }
            else{
              inventarioSlot temp_ =new inventarioSlot();
                temp_.id_stack = inc_slot;
                temp_.id_ = item.ID_item;
                temp_.obj_ = item;
            idPack = temp_.id_stack;
        Debug.Log("__" + item.origem.temEsteItem(item).quantidade);
                temp_.quantidade =  item.origem.temEsteItem(item).quantidade;
                inventario.Add(temp_);
                temp_.attQ();

            }
        }
        
        return idPack;

        }
        public void removerEsteItem(DragAndDrop  item, bool transferencia){

        if(itensEmpilhaveis == false){

                inventarioSlot temp_ = null;
                        foreach (var a in inventario ){
                                if(a.id_stack == item.ID_pack){
                                    temp_ = a;
                                    break;
                                }
                        }
                if(temp_ != null){
                    inventario.Remove(temp_);
                }


        }else{

        inventarioSlot temp = null;

                        foreach(var a in inventario){
                            if(a.id_ == item.ID_item && a.id_stack == item.ID_pack){
                                temp = a;
                                break;
                            }
                        }
                    temp.quantidade -= item.origem.temEsteItem(item).quantidade;
         
                    if (temp.quantidade <= 0){
                        inventario.Remove(temp);
                      if(itensEmpilhaveis && transferencia){
                        Destroy(item.gameObject);
                      }
                    }
        }
        }
    public bool craft;
    public List<ScriptavelItemData> ItensDisponiveis;
public ScriptavelSalvarInventario Preset;
        public void Awake(){

       for(int x = 0;x < Preset.inventario.Count;x++)
        {
            inventarioSlot tem = new inventarioSlot();
            tem.id_ = Preset.inventario[x].id_;
            tem.id_stack = x;
            tem.quantidade = Preset.inventario[x].quantidade;
          
        }

            for (int x = 0; x < transform.childCount; x++)
            {
                SlotInventario temp = transform.GetChild(x).GetComponent<SlotInventario>();
                temp.gerenciadorDaGrid = this;
                temp.empilhavel = itensEmpilhaveis;
                temp.id_Slot = x;
            }

        
    }
        
    }
