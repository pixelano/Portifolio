using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


public class RedeConvolucional : MonoBehaviour
{
    teste nlp = new teste();
    RedeConvolucional_neuronio entrada = new RedeConvolucional_neuronio();
    RedeConvolucional_neuronio primeiro_neuronio = new RedeConvolucional_neuronio(), saida = new RedeConvolucional_neuronio();

    public int tamanhoDoEmbadding, tamanho_janela, contador_aprend;
   public List<palavras> palavrasTreinando, palavrasJanela;
    public float taxa_Aprend;
    public List<TextAsset> todososTextos;

    public RedeConvolucional()
    {
        
        palavrasTreinando = new List<palavras>();
        palavrasJanela = new List<palavras>();
        palavrasJanela.Add(null);
    }

    public void iniciarRede()
    {
        entrada = new RedeConvolucional_neuronio();
        primeiro_neuronio = new RedeConvolucional_neuronio();
        saida = new RedeConvolucional_neuronio(); 

        entrada.proximo = primeiro_neuronio;
        primeiro_neuronio.proximo = saida;
        primeiro_neuronio.anterior = entrada;
        saida.anterior = primeiro_neuronio;

        primeiro_neuronio.inic = this;
        saida.inic = this;

        saida.tamanhoEmabading = tamanhoDoEmbadding;
        primeiro_neuronio.tamanhoEmabading = tamanhoDoEmbadding;
        entrada.tamanhoEmabading = tamanhoDoEmbadding;
        saida.tamahnoPalavrasTreino = palavrasTreinando.Count;

        primeiro_neuronio.memoria_global = new List<float>();
        primeiro_neuronio.memoria_local = new List<float>();
        primeiro_neuronio.inic_recor();
        saida.carregar();


        primeiro_neuronio.car(tamanhoDoEmbadding);
        saida.car(palavrasTreinando.Count);
        Debug.Log(" recarregou o treco");
    }
    public void neuralisar()
    {

        primeiro_neuronio.Processar(entrada.valores_depois);
    }

    public int indice_janela = -1, indice_frase = 0;
    public List<List<palavras>> corpus;
    public palavras alvo;
    public void slideJanela()
    {



        indice_janela++;
        contador_aprend++;
        Debug.Log(" -----   " + corpus[indice_frase].Count);
        if (indice_frase >= corpus.Count)
        {
            Debug.Log("terminou o treino");
            indice_frase = 0;
            indice_janela = 0;
            taxa_Aprend = taxPrend;
           // novoTxt();

        }
        else
        {
            palavrasJanela.Clear();
            if (indice_janela < corpus[indice_frase].Count)
            {



                for (int y = -tamanho_janela; y < tamanho_janela; y++)
                {

                    if (y == 0 || y + indice_janela < 0 || y + indice_janela > corpus[indice_frase].Count ||
                        corpus[indice_frase].Count >= indice_janela + y)
                    {
                        continue;
                    }
                    try
                    {

                        palavrasJanela.Add(palavrasTreinando.Find(x => x == corpus[indice_frase][indice_janela + y] || x == null) );
                    }
                    catch
                    {
                    }
                }
            }
            else
            {
                indice_janela = 0;
                indice_frase++;

                palavrasJanela.Clear();

                for (int y = -tamanho_janela; y < tamanho_janela; y++)
                {

                    if (y == 0 || y + indice_janela < 0 || y + indice_janela > corpus[indice_frase].Count ||
                      palavrasTreinando.Find(x => x == corpus[indice_frase][indice_janela + y] || x == null) == null)
                    {
                        continue;
                    }
                    try
                    {
                        palavrasJanela.Add(palavrasTreinando.Find(x => x == corpus[indice_frase][indice_janela + y] || x == null));
                    }
                    catch
                    {
                    }
                }
            }

            string aux = "";
            List<palavras> aux__ = new List<palavras>();
            try
            {
                for (int i = 0; i <  palavrasJanela.Count; i++)
                {
                    if(palavrasJanela[i] == null)
                    {
                        Debug.Log("*****   palavra nula   " + i );
                        aux__.Add(palavrasJanela[i]);
                    }
                    else
                    {
                        if (palavrasJanela[i].palavra == null)
                        {
                            Debug.Log("*******#####   string nula ?")
;                           aux__.Add(palavrasJanela[i]);
                        }
                    }
                    aux += palavrasJanela[i].palavra + " "; 
                        aux.Split();
                    
                }
            }
            catch(Exception var)  { Debug.Log(var); }
          //  palavrasJanela.RemoveAll(x => aux__.Contains(x));
            Debug.Log(aux);
            entrada.carregar(palavrasJanela);

        }
        if (indice_frase >= corpus.Count)
        {
            Debug.Log("terminou o treino");
            indice_frase = 0;
            indice_janela = 0;
            taxa_Aprend = taxPrend;
           // novoTxt();
        }
        if(contador_aprend % 100 == 0)
        {
            salvarRede();
        }


        Debug.Log(entrada.tamanhoEmabading + "  " + entrada.pesos_neuronios[0].Count + " " + entrada.pesos_neuronios.Count + "    _" +
            primeiro_neuronio.tamanhoEmabading + "  " + primeiro_neuronio.pesos_neuronios[0].Count + " " + primeiro_neuronio.pesos_neuronios.Count+"  _"+
            saida.tamanhoEmabading + "  " + saida.pesos_neuronios[0].Count + " " + saida.pesos_neuronios.Count
            );
    }
    public void salvarRede()
    {
       /* 
        Debug.Log("salvando");
      foreach(palavras plv in palavrasTreinando )
      
        {
            nlp.obd_().todasAsPalavras.Find(x => x == plv).embeding = plv.embeding;
        }

        nlp.salvar();
    */
    }
    int foco_onehot;
    float dif_erro;
    public bool skip_;
    public float taxPrend = 0.3f;
    public void retropropagar()
    {

        foco_onehot = palavrasTreinando.IndexOf(corpus[indice_frase][indice_janela]);
        float cont = 0;
        try
        {
            cont = SaidaGeral[foco_onehot];
        }
        catch { }
        if (cont < 1    )
        {
           

            dif_erro = MathF.Round((1 - cont), 4);

            Debug.Log(" o erro foi de  " + dif_erro + "    no indice  " + foco_onehot + " indice frase = " + indice_frase + "    a palavra foi " + palavrasTreinando[foco_onehot].palavra);
            saida.retropropague(foco_onehot);
            saida.retropropague_(foco_onehot);
        }
        else
        {


            primeiro_neuronio.Recorrencia(primeiro_neuronio.valores_depois);
            skip_ = false;
            if (contador_aprend % 500 == 0)
            {
                taxa_Aprend = taxa_Aprend * 0.75f;
            }

            Debug.Log("feito");
            //neuralisar();
           // att_pesos(); /// desativei e volto a funcionar
            alvo.embeding.vetor = primeiro_neuronio.valores_depois;
            slideJanela();
        }


    }
    public void att_pesos()
    {  
        
       
            
            int indicePalavra = palavrasTreinando.IndexOf(palavrasJanela[indice_frase]);
            embanding embedding = palavrasTreinando[indicePalavra].embeding;

        List<List<float>> aux__ = new List<List<float>>();
        for (int b = 0; b < primeiro_neuronio.pesos_neuronios.Count; b++)
        {
            aux__.Add(primeiro_neuronio.pesos_neuronios[b].GetRange((indice_frase) * tamanhoDoEmbadding, tamanhoDoEmbadding));
        }
                embedding.pesos = aux__;
           
            embedding.vetor = primeiro_neuronio.valores_depois;

            palavrasTreinando[indicePalavra].embeding = embedding;
        
    }
    public int controladorMtTexto=0;
    
    public void novoTxt()
    {
       
        string nov = todososTextos[controladorMtTexto].name;
        controladorMtTexto = controladorMtTexto + 1 >= todososTextos.Count ? 0 : controladorMtTexto+1;


        string origem = @"Assets\rede neural\" + nov +".txt";
        palavrasTreinando = (nlp.tokenUnico(origem));
        corpus = nlp.tokenFiltro(origem);

        iniciarRede();



        amostraDePalavras.Clear();
        foreach (palavras aux__ in palavrasTreinando)
        {
            //  Debug.Log(aux__.palavra + "  recebeu");
            if (aux__.treinada == false)
            {
                aux__.embeding.randomizar(tamanhoDoEmbadding);
                aux__.treinada = true;
            }

            amostraDePalavras.Add(aux__.palavra);

            //      Debug.Log(aux__.palavra + "  com  " + aux__.embeding.vetor.Count);
        }
        slideJanela(); 
    }
    public void Start()
    {
       
        novoTxt();

    }

    public List<float> SaidaGeral = new List<float>();
    public List<float> amostra = new List<float>();



    public bool auxX;
    float tim;

    public float valorTeste;
    public bool treinar;
    public int indice_definirPalavra;
    public List<palavras> palavras_teste;
    public List<string> amostraDePalavras;
    private void Update()
    {
        try
        {
            SaidaGeral = RedeConvolucional_neuronio.OneHotEncoding((RedeConvolucional_neuronio.Relu(saida.valores_depois)));
            amostra = RedeConvolucional_neuronio.Relu(saida.valores_depois);


        }
        catch { }

        if (treinar == true)
        {
            if (skip_ == true)
            {
                skip_ = false;

            //    novoTxt();
            }
            else
            {


                if (Time.time > tim + 1)
                {

                    tim = Time.time;
                    auxX = false;
                    neuralisar();
                    retropropagar();

                }

            }


        }
        else
        {
            if (skip_ == true)
            {
                skip_ = false;

                //novoTxt();
            }
        }
        if(asdjhas == 2)
        {
            foreach(palavras aux_ in palavrasTreinando)
            {
                if(aux_ == null)
                {
                    Debug.Log(" sim tem nulo");
                }
            }
        }
        asdjhas++;
    }
    int asdjhas;
}
// corpus == treinamento
// one-hot == quantidade de palavras