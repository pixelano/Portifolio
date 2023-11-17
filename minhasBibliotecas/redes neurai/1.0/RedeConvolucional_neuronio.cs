using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



// retirar o mono se der ruim
public class RedeConvolucional_neuronio : MonoBehaviour
{
    public string Nome;

    public List<float> valores_antes = new List<float>(), memoria_global = new List<float>()
        , memoria_local = new List<float>(), valores_depois = new List<float>(), bias_pesos = new List<float>();

    public List<List<float>> pesos_neuronios;

    public int tamanhoEmabading, tamahnoPalavrasTreino;
    public float bias = 1, bias_peso;
    public RedeConvolucional_neuronio anterior, proximo;
    public RedeConvolucional inic;
    public RedeConvolucional_neuronio() { }

    public List<palavras> entrada = new List<palavras>();
    public palavras saida ;

    public void novaPalavra_entrada(List<palavras> aux)
    {

        entrada.AddRange(aux);

    }
    public void novaPalavra_saida(palavras aux)
    {
        saida =(aux);

    }
    public void iniciar(int emb)
    {
        tamanhoEmabading = emb;

        valores_antes = new List<float>(emb);
        if (Nome != "saida")
        {
            valores_depois = new List<float>(emb);
        }
        else
        {
            valores_depois = new List<float>(1);
        }
        if (Nome == "entrada" || Nome == "saida")
        {
           
            pesos_neuronios = new List<List<float>>(emb);
        }

        for (int x = 0; x < emb; x++)
        {
            if (Nome== "entrada" || Nome == "saida")
            {
                pesos_neuronios.Add(new List<float>(new float[emb]));
            }
            valores_antes.Add(0);
            
                valores_depois.Add(0);
            
        }
      


    }
    public void processar()
    {
        // Debug.Log(Nome + "   processando");
        

        if (Nome == "entrada")
        {
            foreach (palavras pal in entrada)
            {
                for (int x = 0; x < tamanhoEmabading; x++)
                {
                    for (int w = 0; w < tamanhoEmabading; w++)
                    {
                        valores_antes[w] += pal.embeding.vetor[x] * pal.embeding.pesos[x][w];
                    }
                }
            }

        }
        else
        {
            if (Nome == "saida") {
                for (int w = 0; w < tamanhoEmabading; w++)
                {
                    valores_antes[w] += anterior.valores_depois[w] * saida.embedin_saida.vetor[w];
                }
            }

        }





        if (Nome != "saida")
        {

            valores_depois = (((((valores_antes)))));
            proximo.processar();
        }
        else
        {
            valores_depois = anterior.valores_depois;


            retropropague();
        }



    }

    public void retropropague()
    {
        //anterior.retropropague_contrutor(
        retropropague_desconstrutor();
            //);
    }
    private List<float> retropropague_desconstrutor()
    {/*
       float aux__ = 0;

        float ativacaoDepois_ = media_obtida;
      
        float anpha = ((media_esperada) * deriva(ativacaoDepois_));
        Debug.Log("atiaDp " + ativacaoDepois_ + "  a derivada = " + deriva(ativacaoDepois_) + " resul  " + anpha);
        aux__ =(anpha);

      
        bias = 0.33f * anpha;

        for (int x = 0; x < tamanhoEmabading; x++)
        {

            float valorDepois = ativacaoDepois_;

            float novopeso = bias * (valorDepois == 0 ? 0.01f : valorDepois);
            saida.embeding.vetor[x] += novopeso;
        }

        return aux__;
        */
        List<float> aux_a = new List<float>(tamanhoEmabading);
        for (int y = 0; y < 1; y++)
        {

            float v = saida.embedin_saida.vetor[y];
           
            float ve = saida.embeding.vetor[y];
            float c =  valores_antes[y];
    
            // melhor valor
            float anpha = ve / c;
            //aumeentar ou diminuir
            float polo = v < anpha ? 1 : -1;

            //porcentagem para chegar no melhor valor
            anpha = (v - anpha) / v;

            anpha *= polo;
            
           
            aux_a.Add(anpha);
            
            if (Math.Abs(anpha) == 1)
                continue;
            float novoBias = 0.7f * anpha;
            bias += novoBias;


            float novopeso = novoBias * saida.embedin_saida.vetor[y];
          
          //  saida.embedin_saida.vetor[y] += novopeso;
            Debug.Log( " esperado  " + saida.embeding.vetor[y] + "  com o auste o resultado obtido vai ser de  " +
                (valores_depois[y] * (saida.embedin_saida.vetor[y] + novopeso))
                );
        }

        return aux_a;
    }
    private void retropropague_contrutor(List<float> aux)
    {
        

            float alpha_ = 0;
        for (int y = 0; y < tamanhoEmabading; y++)
        {
            alpha_ += aux[y] * proximo.saida.embedin_saida.vetor[y];
      
                float dif_ = (alpha_ - proximo.saida.embeding.vetor[y] )* ativacao2(valores_antes[y]);


           float aux_ = 0.33f * dif_;
            bias += aux_;
            float taxaCore = 0;

            foreach(palavras pal in entrada)
            {
                for(int x = 0; x < tamanhoEmabading; x++)
                {
                    taxaCore = aux_ * valores_depois[x];
                    pal.embeding.pesos[x][y] += taxaCore;
                }
            }
           
        }


    }
    public void limpar()
    {
       
        for(int i = 0; i < tamanhoEmabading; i++)
        {
            try
            {
                valores_antes[i] = 0;
                valores_depois[i] = 0;
            }
            catch
            {

            }
        }
    }
    public void car(int a)
    {
        bias_peso = (UnityEngine.Random.Range(-1.1f, 1.1f));
        for (int x = 0; x < a; x++)
        {
            bias_pesos.Add(UnityEngine.Random.Range(-1.1f, 1.1f));
        }
    }
    public void Processar(List<float> aux)
    {/*
        // aux = valores de entrada da camada anterior na primeira vez que � chamada inicia com os valores de entrada
        // esta fun��o faz a soma de todos os valores da camada anterior e multi
        valores_antes = new List<float>();

        for (int a = 0; a < pesos_neuronios.Count; a++)
        {
            valores_antes.Add(bias * bias_peso);

            for (int b = 0; b < aux.Count; b++)
            {
                valores_antes[a] +=   aux[b] * pesos_neuronios[a][b];

            }

        }
       
        if(memoria_global.Count > 0)
        {
            valores_depois = (Normalize((Relu(adicionar_memoriaLocal(valores_antes)))));// Relu(ativacao_tanH(valores_antes)); // Softmax(Relu( valores_antes));// (valores_antes);  //Softmax(Relu(valores_antes));
          
        }
        else
        {
            valores_depois = (Normalize((Relu((valores_antes)))));// Relu(ativacao_tanH(valores_antes)); // Softmax(Relu( valores_antes));// (valores_antes);  //Softmax(Relu(valores_antes));

        }


        if (proximo != null)
        {// proxima camada oculta
            proximo.Processar(valores_depois);
        }
        */

    }


    // o gpt falou que eu tenho que alterar para alterar somente o indice que esta sendo analisado 
    // se eu alterar para a verção recomendada a saida gera um array maior do que devia
    // esta é a versao anterior caso eu me esqusa
  
    public List<float> retr_f(int indice)
    {
        
        List<float> aux__ = new List<float>();
        float ativacaoAntes = valores_antes[indice];
        float ativacaoDepois_ = valores_depois[indice];
        float saidaEsperada = valores_depois.Max() +1;
        float anpha = ((saidaEsperada - ativacaoDepois_) * deriva(ativacaoDepois_));
        Debug.Log("atiaDp " + ativacaoDepois_ + "  a derivada = " + deriva(ativacaoDepois_) + " resul  " + anpha);
        aux__.Add(anpha);

        float novoBias = inic.taxa_Aprend * anpha;
        bias += novoBias;

        for (int x = 0; x < tamanhoEmabading; x++)
        {
            float valorAntes = ativacaoAntes;
            float valorDepois = ativacaoDepois_;

            float novopeso = novoBias * (valorDepois== 0 ? 0.01f: valorDepois);
            pesos_neuronios[indice][x] += novopeso;
        }

        return aux__;
    }

  
    public void retroprop_(List<float> aux, int indice_)
    {

        float alpha_ = 0;
        for (int i = 0; i < proximo.tamanhoEmabading; i++)
        {


            alpha_ += aux[0] * proximo.pesos_neuronios[indice_][indice_];
        }

        float dif_ = alpha_ * ativacao2(valores_antes[indice_]);


         bias = inic.taxa_Aprend * dif_;
        this.bias += bias;
        float taxaCore = 0;


        for (int h = 0; h < anterior.valores_depois.Count; h++)
        {

            taxaCore = bias * anterior.valores_depois[h];
            pesos_neuronios[indice_][h] += taxaCore;

        }






    }
    public List<float> alpja_;
    //saida chamando
    public void retropropague(int indice)
    {

        alpja_ = new List<float>();

        alpja_ = retr_f(indice);
        anterior.retroprop_(alpja_, indice);



        // do meio para o inicio
        // anterior.retroprop_(alpja_,indice);


    }
    public void retropropague_(int indice)
    {
       
        anterior.retroprop_(alpja_, indice);
    }
    //primeiro
    public void carregar(List<palavras> aux)
    {
        pesos_neuronios = new List<List<float>>();
        valores_depois = new List<float>();

        for (int a = 0; a < tamanhoEmabading; a++)
        {
            pesos_neuronios.Add(new List<float>());
            //
            //
            for (int b = 0; b < aux.Count; b++)
            {
                //128
                valores_depois.Add(aux[b].embeding.vetor[a]);

                for (int c = 0; c < tamanhoEmabading; c++)
                {
                    pesos_neuronios[a].Add(aux[b].embeding.pesos[c][a]);

                }
            }
        }


        proximo.pesos_neuronios = pesos_neuronios;


    }
    public void carregar()
    {
        for (int a = 0; a < tamanhoEmabading; a++)
        {
            pesos_neuronios.Add(new List<float>());
            for (int b = 0; b < anterior.tamanhoEmabading; b++)
            {
                pesos_neuronios[a].Add(UnityEngine.Random.Range(-1.1f, 1.1f));
            }
        }
    }


    public List<float> Recorrencia(List<float> entrada)
    {
        for(int i = 0; i < memoria_global.Count; i++)
        {
            float aux = memoria_global[i];

            memoria_global[i] *= entrada[i]  ;
            memoria_global[i] += entrada[i] * MathF.Tanh(valores_antes[i] + memoria_local[i]);

            memoria_local[i] = entrada[i] * MathF.Tanh(memoria_global[i]);
        }
      //  memoria_local = Softmax(memoria_local);

       
        return memoria_local;
    }
    public List<float> adicionar_memoriaLocal(List<float> aux)
    {
        for (int i = 0; i < memoria_local.Count; i++)
        {
            
               aux[i] += memoria_local[i];
        }
        
        return aux;
    }

    public void inic_recor()
    {

        for(int i = 0; i < tamanhoEmabading; i++)
        {
            memoria_global.Add(1f);
            memoria_local.Add(1f);
        }
      }
    public static List<float> Softmax(List<float> values)
    {
        List<float> result = new List<float>();
        float sum = 0;

        // Calcula a soma das exponenciais de todos os valores
        foreach (float v in values)
        {
            sum += Mathf.Exp(v);
        }

        // Divide cada exponencial pela soma para normalizar as probabilidades
        foreach (float v in values)
        {
            float probability = Mathf.Exp(v) / sum;
            result.Add(probability);
        }

        return result;
    }

    public static List<float> Relu(List<float> input)
    {
        List<float> output = new List<float>();

        for (int i = 0; i < input.Count; i++)
        {
            output.Add(Math.Max(0, input[i]));
        }

        return output;
    }
    public static List<float> Normalize(List<float> values)
    {
        float max = values.Max();
        float min = values.Min();
        float range = max - min;

        List<float> normalizedValues = new List<float>();
        foreach (float value in values)
        {
            float normalizedValue = (value - min) / range;
            normalizedValue = float.IsNaN(normalizedValue )? 0 : normalizedValue;
            normalizedValues.Add(normalizedValue);
        }

        return normalizedValues;
    }
    public static List<float> sigmoide(List<float> valores)
    {
        List<float> ativacoes = new List<float>();

        foreach (float valor in valores)
        {
            float ativacao = 1.0f / (1.0f + Mathf.Exp(-valor));
            ativacoes.Add(ativacao);
        }

        return ativacoes;
    }
    public static List<float> OneHotEncoding(List<float> valores)
    {
        int indexMax = valores.IndexOf(valores.Max());
        List<float> encoded = new List<float>(valores.Count);
        for (int i = 0; i < valores.Count; i++)
        {
            encoded.Add(i == indexMax ? 1 : 0);
        }
        return encoded;
    }


    public List<float> ativacao(List<float> aux_)
    {
        List<float> f = new List<float>();
        foreach (float a in aux_)
        {
            float aux = MathF.Exp(-a);
            float resultado11 = 1 / (1 + aux);
            f.Add(resultado11);
        }
        return f;
    }

    public float ativacao(float aux_)
    {

        float aux = MathF.Exp(-aux_);
        float resultado11 = 1 / (1 + aux);

        return resultado11;
    }
    public List<float> ativacao_tanH(List<float> aux_)
    {
        List<float> aux__ = new List<float>();
        foreach (float auxX in aux_)
        {

            float aux = MathF.Exp(-auxX);
            float resultado11 = 1 / (1 + aux);
            aux__.Add(resultado11);
        }

        return aux__;
    }
    public float deriva(float aux_)
    {


        float resultado12 = ativacao(aux_) * (1 - ativacao(aux_));

        return resultado12;
    }

    public float ativacao2(float aux_)
    {

        float aux = MathF.Exp(-aux_);
        float resultado11 = 2 / (1 + aux);

        return resultado11 - 1;
    }
    public float deriva2(float aux_)
    {
        float parte1 = 1 + ativacao2(aux_);
        float parte2 = 1 - ativacao2(aux_);

        float resultado12 = parte1 * parte2 * 0.5f;
        return resultado12;
    }
    public float somatoria(List<float> aux)
    {
        float valor = 0; 
        foreach(float a  in aux)
        {
            valor += a;
        }

        return valor;
    }
}

