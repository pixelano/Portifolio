using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.IO;
using System;
public class GerenciadorDeRedeNeural : MonoBehaviour
{
    public List<float> entrada = new List<float>();
    public List<float>  saida ;
    public List<instanciaMatriz> matriz = new List<instanciaMatriz>();
    public config_Rede configuracoes = new config_Rede();

    [System.Serializable]
    public struct instanciaMatriz {
        [SerializeField]
        public Neural_2_0 rede;
        public int tamanho;
        

    
    }
    public void instanciarRede()
    {
        for (int x = 0; x < matriz.Count; x++)
        {
            if (x == 0)
            {
                instanciaMatriz temp = matriz[x];
                temp.rede = new Neural_2_0( entrada.Count, temp.tamanho, configuracoes);
                matriz[x] = temp;
            }
            else
            {
                instanciaMatriz temp = matriz[x];
                temp.rede = new Neural_2_0(matriz[x - 1].tamanho, temp.tamanho, configuracoes);
                matriz[x] = temp;
            }
        }
    }
    public void processarEntrada(List<float> ent_, List<float> said_)
    {
        List<float> aux = new List<float>();
        aux.AddRange(ent_);
        foreach(instanciaMatriz mt in matriz)
        {
           aux = mt.rede.processar(aux);
        }
       
        said_.Clear();
        said_.AddRange(Neural_2_0.ativacao(aux));
    }
    private void Start()
    {
        entrada.Add(0);

        instanciarRede();
     
    }
    float tt,tt_;

    private void serializando()
    {
        serializarRede srr = new serializarRede();
      
        foreach (instanciaMatriz aux in matriz) {
            srr.neuroniosSalvos.Add(aux.rede.multiplicador); 
        }

        serializar(nomeSerial+".xml", srr);
    }
    public void carregarSRL()
    {
        serializarRede srr = new serializarRede();
        srr = desSerializar(nomeSerial + ".xml");
      for(int x = 0; x < srr.neuroniosSalvos.Count; x++)
        {
            matriz[x].rede.multiplicador = srr.neuroniosSalvos[x];
        }
        Debug.Log(matriz[0].rede.multiplicador[0][0]);
    }
    public void definirEntrada(List<float> aux)
    {

        entrada.Clear();
            entrada.AddRange(aux);
        
       
    }
    public float clock=1;
    private void Update()
    {
      
            if (tt_ + clock< Time.time)
            {
                tt_ = Time.time;

                processarEntrada(entrada, saida);

            }

           


        if (serialize == true)
        {
            serialize = false;
            Debug.Log(matriz[0].rede.multiplicador[0][0]);
            serializando();
        }
        if (carregar == true)
        {
            carregar = false;

            carregarSRL();
        }
    }
    public string nomeSerial;
    public bool serialize,carregar;
    private void serializar(string nomeArquivo,serializarRede ser)
    {
        using (StreamWriter stream = new StreamWriter(Path.Combine(@"C:\Users\filip\OneDrive\Área de Trabalho\Hunters of the Wild\Assets\prefabs\mobs\rede neural save", nomeArquivo)))
        {
            XmlSerializer serializador = new XmlSerializer(typeof(serializarRede));
            serializador.Serialize(stream, ser);
        }
    }
    private serializarRede desSerializar(string nomeArquivo)
    {
        serializarRede aux = null;
        using (StreamReader stream = new StreamReader(Path.Combine(@"C:\Users\filip\OneDrive\Área de Trabalho\Hunters of the Wild\Assets\prefabs\mobs\rede neural save", nomeArquivo)))
        {
            XmlSerializer serializador = new XmlSerializer(typeof(serializarRede));
            aux = (serializarRede)serializador.Deserialize(stream);
        }
        return aux;
    }


}

[System.Serializable()]
public class serializarRede : ISerializable
{

    public List<List<List<float>>> neuroniosSalvos = new List<List<List<float>>>();
    
   


    public serializarRede() { }

   
    public serializarRede(SerializationInfo info, StreamingContext ctxt)
    {
        
        neuroniosSalvos = (List<List<List<float>>>)info.GetValue("rede", typeof(string));
       
      
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {

            info.AddValue("rede", neuroniosSalvos);
           
      
    } 
}