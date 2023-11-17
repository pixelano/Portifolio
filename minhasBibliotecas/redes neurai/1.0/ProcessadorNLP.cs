using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ProcessadorNLP : MonoBehaviour
{
    public int tamanhoEMBANDING = 64,tamanhoGrupo =1;
    //
    public List<TextAsset> textoCorpus;


    private _corpus corpus = new _corpus();
    private _processamento processamento = new _processamento();
   private _analisando analisando = new _analisando();
  
 
    private int indicePalavraTreino=5,indicepalavrastreinoLinhas=0; // quando mudar de linha att isso para  0

    public bool treinando = true;

    public List<RedeConvolucional_neuronio> ListaNeuroniosOcultos = new List<RedeConvolucional_neuronio>();

    public List<palavras> palavrasTreinando;

    teste nlp;
    List<int> contadorTreino;

    #region aoIniciar
    private void Start()
    {
        processamento.linhaProcessadorCorpus = new List<palavras>();
        corpus.indiceTextoCorpus = 0;
        iniciarProcesso();
        corpus.textoCorpusAlvo = textoCorpus[corpus.indiceTextoCorpus];
        prepararProcessasCorpus();
        processamento.linhaProcessadorCorpus = processamento.textoProcessadoCorpus[0];
        RodarPalavras();
     

       
    }
    private float tt_;
    private void Update()
    {

        if (Time.time > tt_ + 2)
        {
            tt_ = Time.time;
            treinar();
        }
        }
        private void iniciarProcesso()
    {
        nlp = new teste();
    
       RedeConvolucional_neuronio aux_ = gameObject.AddComponent<RedeConvolucional_neuronio>();
        aux_.Nome = "entrada";
        if (ListaNeuroniosOcultos.Count == 0) {
   
            ListaNeuroniosOcultos.Add(aux_);
            RedeConvolucional_neuronio aux__ = gameObject.AddComponent<RedeConvolucional_neuronio>();
            aux__.Nome = "saida";
            ListaNeuroniosOcultos.Add(aux__);
        }
        else
        {
            ListaNeuroniosOcultos.Insert(0, aux_);

            RedeConvolucional_neuronio aux__ = gameObject.AddComponent<RedeConvolucional_neuronio>();
            aux__.Nome = "saida";
            ListaNeuroniosOcultos.Add(aux__);
        }

        for (int i = 0; i < ListaNeuroniosOcultos.Count; i++)
        {
            ListaNeuroniosOcultos[i].iniciar(tamanhoEMBANDING);
            if (i != ListaNeuroniosOcultos.Count - 1)
            {

                ListaNeuroniosOcultos[i].proximo = ListaNeuroniosOcultos[i + 1];
            }
            if (i != 0)
            {

                ListaNeuroniosOcultos[i].anterior = ListaNeuroniosOcultos[i - 1];
            }
        }
    
    }
    #endregion
    #region para cada novo corpus
    private void prepararProcessasCorpus()
    {
        string nov = corpus.textoCorpusAlvo.name;
      
        string origem = @"Assets\rede neural\" + nov + ".txt";
        palavrasTreinando = (nlp.tokenUnico(origem));
        processamento.textoProcessadoCorpus = nlp.tokenFiltro(origem);

        nlp.preparar(palavrasTreinando,tamanhoEMBANDING);


        contadorTreino = Enumerable.Range(0, palavrasTreinando.Count).ToList();


    }


    #endregion
    #region iniciar nova palavra treino
    private bool escolherNovaPalavra()
    {
        
        if (processamento.linhaProcessadorCorpus.Count-1 < indicePalavraTreino)
        {
            if (processamento.textoProcessadoCorpus.Count < indicepalavrastreinoLinhas)
            {
                indicePalavraTreino = 0;
                processamento.linhaProcessadorCorpus = processamento.textoProcessadoCorpus[indicepalavrastreinoLinhas];
                indicepalavrastreinoLinhas++;
            }
            else
            {
                return true;
            }
        }

        
       
        analisando.alvo = processamento.linhaProcessadorCorpus[indicePalavraTreino];
        analisando.indice = indicePalavraTreino;
        definirPalavrasRedes();
        indicePalavraTreino++;
        return false;
    }

   private void definirPalavrasRedes()
    {
        Debug.Log("def_ ");
        List<palavras> aux_ = new List<palavras>();
        // aux_.AddRange(palavrasTreinando);
        int inc = 0, fim = 0;
        inc = (analisando.indice - tamanhoGrupo) < 0 ? 0 : (analisando.indice - tamanhoGrupo);
        fim = (analisando.indice + tamanhoGrupo);// > palavrasTreinando.Count ? (palavrasTreinando.Count - 1) :            (analisando.indice + tamanhoGrupo);
        for (int i = inc; i < fim; i++)
        {
            try
            {
                if (palavrasTreinando.Contains(processamento.linhaProcessadorCorpus[i]))
                {

                    aux_.Add(palavrasTreinando.Find(x => x == processamento.linhaProcessadorCorpus[i]));
                }
            }
            catch
            {
            }
        }
      //  Debug.Log("inc  " + inc + "     fim  " + fim + "       tt  " + aux_.Count);
        ListaNeuroniosOcultos[ListaNeuroniosOcultos.Count - 1].novaPalavra_saida(processamento.linhaProcessadorCorpus[analisando.indice]);
        aux_.Remove(processamento.linhaProcessadorCorpus[analisando.indice]);

        ListaNeuroniosOcultos[0].novaPalavra_entrada(aux_);


    }
    private void RodarPalavras()
    {

       // ListaNeuroniosOcultos[ListaNeuroniosOcultos.Count - 1].valores_depois[0] = 0;
        if ( escolherNovaPalavra() == true)
        {
            corpus.indiceTextoCorpus++;
            if(corpus.indiceTextoCorpus < textoCorpus.Count)
            {
                corpus.textoCorpusAlvo = textoCorpus[corpus.indiceTextoCorpus];
                prepararProcessasCorpus();
            }
            else
            {
                Debug.Log("reject");
                treinando = false;
            }
        }
       
    }
    #endregion
    #region treinar palavra
    public void treinar()
    {
        if (treinando == true)
        {
            if (testarPalavra())
            {
               
                RodarPalavras();
            }
            else
            {
             
                neuralisar();
                retropropague();
            }
        }
    }
    public void neuralisar()
    {
        ListaNeuroniosOcultos[0].processar();
    }
    public void retropropague()
    {
        ListaNeuroniosOcultos[ListaNeuroniosOcultos.Count - 1].retropropague();
    }
    public bool testarPalavra()
    {
        float media = 0;
        for(int i = 0; i < tamanhoEMBANDING; i++)
        {
            float aux___ = (analisando.alvo.embeding.vetor[i] - ListaNeuroniosOcultos[ListaNeuroniosOcultos.Count - 1].valores_depois[0]);
            media += aux___ <= 0.3f  && aux___ > -0.3f ? 1 :-1;
        }

        //media /= tamanhoEMBANDING;
        Debug.Log(" a media  " + media);
        ListaNeuroniosOcultos[ListaNeuroniosOcultos.Count - 1].valores_depois[0] = 0;
        foreach(RedeConvolucional_neuronio aux in ListaNeuroniosOcultos)
        {
            aux.limpar();
        }
        return media > 0;// && media < 0.01f ;
    }
    #endregion
    private struct _processamento
    {
        public List<List<palavras>> textoProcessadoCorpus;
        public List<palavras> linhaProcessadorCorpus ;
    }
    private struct _analisando
    {
       public palavras alvo;
        public List<palavras> grupoPalavras;
        public int indice;
    }
    public struct _corpus
    {
     
        public TextAsset textoCorpusAlvo;
        public int indiceTextoCorpus;
    }

    /*  // PROCEDIMENTO PARA FAZER UMA NLP QUE FUNCIONA &?&?&??&&¨?$!@# // 
     * 
     * primeiro devemos pegar as PALAVRAS do banco de dados se baseando no texto que recebemos da lista TextoCorpus  / OK
     * 
     * depois disto a gente escolhe uma PALAVRA aleatoria para analisar na rede neural, depois de analisado essa
     * PALAVRA não pode ser mais testada.
     * 
     * quando testar uma PALAVRA devo escolher uma tamanho do teste que vai ser as PALAVRAS em volta da PALAVRA a ser
     * analisada.
     * quando tiver esta lista mande para a classe dos neuronios da rede para processar.
     * 
     * a saida em que se esta analisando vai ser testada e se passar o teste vai ser finalisado o teste.
     * quando finalisado deve se salvar as mudanças do embading , pesosEntrada e pesosSaida(ainda vou criar)
     * quando salvo passar para a proxima PALAVRA a ser testada,
     * 
     * quando se "neuralisa" uma PALAVRA deve se retropropagar a mesma para que tenha um ajuste interno.
     * 
     * 
     * 
     */

}
