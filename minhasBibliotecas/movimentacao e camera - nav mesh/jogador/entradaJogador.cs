using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class entradaJogador : MonoBehaviour
{

    public movimentacao movimento;
    public float rotationSpeed = 5f;
    private Vector3 direcao;
   public Rigidbody rb;

    public Camera cam_;

    [SerializeField]
    public coisasDaCamera cameramain;
    [SerializeField]
    public coisasCorrida corrida;

    public GameObject bolinha;


    [System.Serializable]
    public struct coisasDaCamera {

        public List<GameObject> cameras_pontos;// pontosZero,direita_c,esquerda_c,front_c;
       
        public float velocidade_recompor;


    }
    [System.Serializable]
    public struct coisasCorrida {

        public List<float> evolução_ ,tempo_;
        public int indice_,indice_max;
        public float aux_tempo_,velocidade_atual;

        public float tamanhoPulo;

    }


    private bool andando_, travar_camera = true;
 
    private Dictionary<KeyCode, Vector3> andar = new Dictionary<KeyCode, Vector3>()
    {
        { KeyCode.W, Vector3.forward },
        { KeyCode.A, Vector3.left },
        { KeyCode.S, Vector3.back },
        { KeyCode.D, Vector3.right }
    };

    public Vector3 direcaoTemp, direcaoTemp2, local_temp;
    private int camera_temp;
    public bool aux___;
    private void andar_()
    {
        foreach (var kvp in andar)
        {
            if (Input.GetKey(kvp.Key))
            {
                direcao += transform.TransformDirection(kvp.Value);
            }
        }
        travar_camera = direcao == Vector3.zero ? false : true;

        if (direcao == transform.TransformDirection(Vector3.left) ||            direcao == transform.TransformDirection(Vector3.right) ||            direcao == transform.TransformDirection(Vector3.back)   )
        {
            aux___ = false;


            if (direcao == transform.TransformDirection(Vector3.left) )
            {
                camera_temp = 0;
                if (direcaoTemp == Vector3.zero)
                {
                    direcaoTemp =(Vector3.left);
                    local_temp = transform.TransformDirection(direcaoTemp);
                }

              
            }
            if (direcao == transform.TransformDirection(Vector3.right))
            {
                camera_temp =2;
                if (direcaoTemp == Vector3.zero)
                {
                    direcaoTemp = (Vector3.right);
                    local_temp = transform.TransformDirection(direcaoTemp);
                }
              
            }
            if (direcao == transform.TransformDirection(Vector3.back))
            {
                camera_temp = 3;
                if (direcaoTemp == Vector3.zero)
                {
                    direcaoTemp = (Vector3.back);
                    local_temp = transform.TransformDirection(direcaoTemp);
                }

            }

            direcaoTemp2 = direcaoTemp;
            direcao =(local_temp);
        }
        else
        {
          if(direcaoTemp2 != Vector3.zero &&  direcao == Vector3.zero)
            {
                // direcaoTemp2 = Vector3.zero;
            }

            if (direcao == transform.TransformDirection(Vector3.forward))
            {
                if (aux___ == false)
                {
                    if (direcaoTemp2 == (Vector3.left))
                    {

                        if (local_temp == Vector3.zero)
                        {
                            local_temp = transform.TransformDirection(Vector3.right);
                            aux___ = true;
                        }
                    }
                    else if (direcaoTemp2 == (Vector3.right))
                    {
                        if (local_temp == Vector3.zero)
                        {
                            local_temp = transform.TransformDirection(Vector3.left);
                            aux___ = true;
                        }
                    }
                }

                direcao = local_temp != Vector3.zero ? local_temp : direcao;
            }
            else
            {
                if (aux___ == true)
                {
                    local_temp = transform.TransformDirection(Vector3.forward);
                }
            }

            
                if(direcao == Vector3.zero)
            {

                 direcaoTemp = Vector3.zero;
                 local_temp = Vector3.zero;

                if(aux___ == true)
                {
                    direcaoTemp2 = Vector3.zero;
                }
               
            }







            camera_temp = 1;
        }


        
        direcao *= 0.5f;

        movimento.andeAte(direcao + transform.position);

        if(travar_camera == true)
        {
            
            reposicionar_camera(camera_temp);

        }
        else
        {

        }
        pulo(direcao);
        direcao = Vector3.zero;

      
        correr_();
       
    }
  
    private void pulo( Vector3 aux)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movimento.desativar();
            bolinha.GetComponent<ParentConstraint>().constraintActive = false;
           
            bolinha.GetComponent<Rigidbody>().AddForce((Vector3.up + (direcao*2)) * corrida.tamanhoPulo, ForceMode.Impulse);
            bolinha.GetComponent<pulo>().noAr = true;
        }
    }
    private void correr_()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            corrida.indice_max = corrida.indice_max == (corrida.evolução_.Count - 1 )? 1 : corrida.indice_max +1; 
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            corrida.aux_tempo_ = 0;
            corrida.indice_ = corrida.indice_ == 0 ? 0 : corrida.indice_ -1;
        }

            if (Input.GetKey(KeyCode.LeftShift))
        {
            if (corrida.indice_  < corrida.indice_max)
            {
                corrida.aux_tempo_ += Time.deltaTime;

                if (corrida.tempo_[corrida.indice_] < corrida.aux_tempo_)
                {
                    if (corrida.indice_ < corrida.indice_max)
                    {


                        corrida.indice_++;
                        corrida.aux_tempo_ = 0;
                        corrida.velocidade_atual = corrida.evolução_[corrida.indice_];


                    }

                }
            }
            else
            {
                if (corrida.indice_ > corrida.indice_max)
                {
                    corrida.indice_ = corrida.indice_max;
                    corrida.velocidade_atual = corrida.evolução_[corrida.indice_];
                }

            }

        }
        else
        {
            if (corrida.indice_ > 0)
            {
                corrida.aux_tempo_ += Time.deltaTime;
                if (corrida.tempo_[corrida.indice_]/3 < corrida.aux_tempo_)
                {
                   
                        corrida.indice_--;
                        corrida.aux_tempo_ = 0;
                        corrida.velocidade_atual = corrida.evolução_[corrida.indice_];

                 

                }
            }
            else
            {
                corrida.aux_tempo_ = 0;
                corrida.velocidade_atual = corrida.evolução_[0];
            }
        }

        movimento.alterarVelocidade(corrida.velocidade_atual);
    }
    private void mov_camera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        if (travar_camera)
        {
           
            

            transform.Rotate(Vector3.up, mouseX * rotationSpeed);
        }
        else
        {
            cam_.transform.RotateAround(transform.position,Vector3.up , mouseX * rotationSpeed);
        }
    }
    public float veloc;
    public int vf = 0;
    private int valorUM;
    private void reposicionar_camera(int refe)
    {
        float nn = valorUM / cameramain.velocidade_recompor;
        int aux_ = refe == vf ? vf : refe > vf ? 1 : -1;

        if (refe != 3)
        {
            aux_ = aux_ != refe ? aux_ + vf < 0 ? 3 : refe + vf > 3 ? 0 : aux_ + vf : aux_;
        }
        else
        {
            aux_ = 3;
        }
        if (Vector3.Distance(cameramain.cameras_pontos[aux_].transform.position, cam_.transform.position) < 1)
        {
            cam_.transform.position =(cameramain.cameras_pontos[aux_].transform.position );
            vf = aux_;
            valorUM = 0;
        }
        else
        {
            
            cam_.transform.position = Vector3.Lerp(cam_.transform.position, cameramain.cameras_pontos[aux_].transform.position, nn);
        }

        valorUM = (valorUM + 1) % ((int)cameramain.velocidade_recompor + 1);
    }

    void Start()
    {
        // cam_ = GetComponentInChildren<Camera>();
      //  movimento = GetComponent<movimentacao>();
       // rb = GetComponent<Rigidbody>();
    }
 

  

    private void Update()
    {
        
        mov_camera();
        andar_();
      
        }

    }
