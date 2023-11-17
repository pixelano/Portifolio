using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GerenciadorDeIA : MonoBehaviour
{
    public GameObject grupo_;
    public meus_olhos_ olhos;
    public servivo servo;
    private GerenciadorDeRedeNeural grn ;
    private NavMeshAgent agente;
    public logicaSeguir inteligencia;

    private Vector3 originalDestination;

    public bool seguirAmigo;
    public GerenciadorDeIA alvoAmigo;

    bool flag_ = true;
    public string debug_;
    public bool emCombate = false;
    public GameObject combateAlvo;
    
    

    [System.Serializable]
    public struct meus_olhos_ {
        public bool vendoAlvo;
        public float distanciaMax;
      //  public RaycastHit cast_jogador,cast_comida;
      //  public GameObject oAlvo,alvocomida;
    }

    [System.Serializable]
    public struct logicaSeguir {

        public float tempoParaEsquecer, tempoAtual;
    }

    public void seguiramigo(GerenciadorDeIA aux)
    {
        seguirAmigo = true;
     
        alvoAmigo = aux;
    }
    private void Start()
    {
        grn = GetComponent<GerenciadorDeRedeNeural>();
        servo = GetComponent<servivo>();
        grupo_ = GetComponentInParent<grupo>().gameObject;
       
        agente = GetComponent<NavMeshAgent>();

        originalDestination = transform.position;
        MoveToTarget(gameObject);

    }
    public struct olhar {
        public RaycastHit rch;
        public GameObject go;

    }


    private olhar olharEmVolta(bool vivo,bool aliado)
    {
        olhar aux = new olhar();
        RaycastHit[] rh = Physics.BoxCastAll(transform.position + transform.forward, Vector3.one, transform.forward, Quaternion.identity, olhos.distanciaMax);
        List<RaycastHit> rh_ = new List<RaycastHit>();
        rh_.AddRange(rh);
        rh_.RemoveAll(x => x.collider.gameObject == gameObject);
        rh_.RemoveAll(x => x.collider == null);
        rh_.RemoveAll(x => x.collider.GetComponent<servivo>() == false);

        if (aliado)
        {
            rh_.RemoveAll(x => x.collider.GetComponent<servivo>().especie != servo.especie);
        }
        else
        {
            rh_.RemoveAll(x => x.collider.GetComponent<servivo>().especie == servo.especie);
        }

        if (vivo)
        {
            rh_.RemoveAll(x => !x.collider.GetComponent<servivo>().acessarVida().estavivo());
        }
        else
        {
            rh_.RemoveAll(x => x.collider.GetComponent<servivo>().acessarVida().estavivo());

        }
        float distancia_ = olhos.distanciaMax + 1;
        GameObject alvo = null;

        foreach (RaycastHit auxg in rh_)
        {
            float aux__ = Vector3.Distance(transform.position, auxg.collider.transform.position);
            if (aux__ < distancia_)
            {
                distancia_ = aux__;
                alvo = auxg.collider.gameObject;
            }
        }

        if (alvo != null)
        {

            Physics.Raycast(transform.position + transform.forward, alvo.transform.position - transform.position, out aux.rch, olhos.distanciaMax);

            if (aux.rch.collider != null)
            {
                if (aux.rch.collider.gameObject == alvo)
                {
                    aux.go = alvo;
                    if (vivo == true && aliado == false)
                    {
                       
                        olhos.vendoAlvo = true;
                    }

                }
                else
                {
                    if (vivo == true && aliado == false)
                    {
                        olhos.vendoAlvo = false;
                    }
                }

            }
                

            }
           
        
       

        return aux;
        
    }
    void seguirAmigo_(GerenciadorDeIA alvo)
    {
        if(alvo.olhos.vendoAlvo == false || alvo == null)
        {
            seguirAmigo = false;
        }
        else
        {
            MoveToTarget(alvo.gameObject);
          //  agente.SetDestination(alvo.transform.position);
          //  originalDestination = alvo.transform.position;
        }
      
    }
    void MoveToTarget( GameObject alvo)
    {
        //  agente.isStopped = false;
        if (servo.flags.atacando == false)
        {
            agente.SetDestination(alvo.transform.position);
            originalDestination = alvo.transform.position;
        }
    }
 
    void patrulhar()
    {
        if (agente.remainingDistance < agente.stoppingDistance)
        {
            float distancia = grupo_.GetComponent<grupo>().distancia;
            float x = Random.Range(-distancia, distancia);
            float y = Random.Range(-distancia, distancia);
            agente.isStopped = false;
            agente.SetDestination(new Vector3(x, 0, y) + grupo_.transform.position);

        }
        else
        {
        }
    }
    private bool esperaSeguir()
    {
        inteligencia.tempoAtual += Time.deltaTime;

        if(inteligencia.tempoAtual > inteligencia.tempoParaEsquecer)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    public bool paratudo;

    [System.Serializable]
    public struct entradasSeres_Rede
    {
        public string especie;
        public float nivel;
        public float pontos_de_vida,pontosArmadura;
        public float localização_x, localização_z;
        public void setar(servivo aux)
        {
            especie = aux.especie;
            nivel = aux.nivel;
            pontos_de_vida = aux.acessarVida().retonavida();
            localização_x = aux.transform.position.x;
            localização_z = aux.transform.position.z;
            pontosArmadura = aux.acessarVida().armadura;

        }
        public void zerar()
        {
            especie ="";
            nivel=0;
            pontos_de_vida=0;
            localização_x=0; 
            localização_z=0;
        }
    }
    public entradasSeres_Rede inimigoAlvo, eumesmo;

    private void attFlags()
    {
       

    }
    public bool atk;
    public float ultimoatk;
    private void atacar()
    {
        if (atk == false)
        {
            ultimoatk = Time.time;
            atk = true;
            
            servo.usarHabilidade(0, 0);
            
        }
    }
    private void retirarAtk() {

        servo.fecharHabilidade(0);
    }
    private void comerComida(GameObject aux)
    {
        servivo aux_ =  aux.GetComponent<servivo>();
        servo.comerAlvo(aux_);
        aux_.destruirS();
    }
    private Vector3 distanciaRaio(Vector3 aux)
    {
        RaycastHit rh;
        Physics.Raycast(transform.position, aux, out rh);

        return rh.collider != null ? rh.collider.transform.position:transform.position;

    }

    void Update()
    {
        if (servo.minhaVida.estavivo())
        {
            attFlags();
            olhar inimigo = olharEmVolta(true,false);
            if (paratudo == false)
            {
               

                if (emCombate==false)
                {
                    if (inimigo.go != null)
                    {
                        debug_ = " alvo por olhar";
                        emCombate = true;
                        combateAlvo = inimigo.go;
                      
                    }
                    else
                    {

                        if (seguirAmigo)
                        {

                            seguirAmigo_(alvoAmigo);
                            debug_ = ("um amigo sabe o caminho");

                        }
                        else
                        {
                           
                            if (servo.flags.atacado != null)
                            {
                                debug_ = " alvo por retruca0";
                                emCombate =  servo.flags.atacado.GetComponent<servivo>().acessarVida().estavivo() ?
                                                                   true : false ;

                                combateAlvo = emCombate ? servo.flags.atacado.gameObject : null;
                            }
                            else {
                                olhar comid = olharEmVolta(false, false);

                                if (comid.go != null)
                                {
                                    debug_ = "indo comer";
                                    MoveToTarget(comid.go);
                                    if (agente.remainingDistance < agente.stoppingDistance)
                                    {
                                        comerComida(comid.go);
                                    }




                                }
                                else
                                {
                                    patrulhar();
                                    inteligencia.tempoAtual = 0;
                                    debug_ = ("patrulhando");

                                } 
                            }
                        }
                        

                    }
                }
                else
                {
                  
                //   debug_ = "perseguida";
                    flag_ = true;
                    MoveToTarget(combateAlvo);


                    if (agente.remainingDistance < agente.stoppingDistance)
                    {
                        transform.LookAt(combateAlvo.transform);

                        agente.isStopped = true;
                   //     debug_ = " atacando";
                        atacar();
                    }
                    else
                    {
                   //     debug_ = " perceguindo   ";
                        agente.isStopped = false;
                    }
                    if(combateAlvo.GetComponent<servivo>().acessarVida().estavivo() == false)
                    {
                        emCombate = false;
                    }

                    /*
                    if (flag_ && olhos.oAlvo != null)
                    {

                        if (esperaSeguir())
                        {
                            flag_ = false;
                            MoveToTarget(gameObject);

                        }
                        if (olhos.oAlvo.transform.position != originalDestination)
                        {

                            MoveToTarget(olhos.oAlvo);
                        }
                        if (agente.remainingDistance < agente.stoppingDistance)
                        {
                            agente.isStopped = true;
                        }
                        else
                        {
                            agente.isStopped = false;
                        }
                        debug_ = ("persistindo");

                    }*/
                    
                    }
               
                if (atk)
                {
                    if (Mathf.Abs(Time.time - ultimoatk) > 4)
                    {
                        atk = false;
                        retirarAtk();
                    }
                }
                
            }
            else
            {
                if (olhos.vendoAlvo)
                {
                    inimigoAlvo.setar(inimigo.go.GetComponent<servivo>());
                }
                else
                {
                    inimigoAlvo.zerar();
                }
                eumesmo.setar(servo);

                List<float> entrada = new List<float>();

                Debug.Log("ae");
                entrada.Add(eumesmo.nivel - inimigoAlvo.nivel);
                entrada.Add(eumesmo.pontos_de_vida- inimigoAlvo.pontos_de_vida);
                entrada.Add(eumesmo.pontosArmadura - inimigoAlvo.pontosArmadura);

                entrada.Add(eumesmo.localização_x- inimigoAlvo.localização_x);
                entrada.Add(eumesmo.localização_z - inimigoAlvo.localização_z);

                entrada.Add(Vector3.Distance(distanciaRaio(transform.forward), transform.position));
                entrada.Add(Vector3.Distance(distanciaRaio(-transform.forward), transform.position));
                entrada.Add(Vector3.Distance(distanciaRaio(transform.right), transform.position));
                entrada.Add(Vector3.Distance(distanciaRaio(-transform.right), transform.position));

                grn.definirEntrada(entrada);

                switch (grn.saida.IndexOf(grn.saida.Find(x => x == 1)))
                    {
                    case 0:
                        debug_ = " zero";
                        break;
                    case 1:
                        debug_ = " um ";
                        break;
                    case 2:
                        debug_ = " 3";
                        break;
                    case 3:
                        debug_ = " 4 ";
                        break;
                    default:
                        debug_ = " sua m ae";
                        break;


                }
            }


        }
        else
        {
            servo.DesativarTudo();
          if(servo.acessarVida().decompondo())
            {
                servo.destruirS();
            }
        }
    }
}
