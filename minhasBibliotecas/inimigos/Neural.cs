using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neural : MonoBehaviour
{
    public List<float> _entradas, _saidas;
    public List<List<float>> neuronios, pesos;

    public string nome;

    private int h_neuronios, v_neuronios;

    public int[,] auxtreco;
    public bool semantica, // os neuronios não são resetados mantendo os valores anteriores
        independencia,      // aleatoriamente alguns neuronios são desativados
       binario   // quando um neuronio for possitivo o valor é repassado integralmente

        ;

    public GameObject Exportador;

    // definir entrada ou saida
    private void entrada(int entrar)
    {
        _entradas = new List<float>();

        _entradas.Clear();
        _entradas.AddRange(new float[entrar]);

    }
  
    private void saida(int saidas)
    {
        _saidas.Clear();
        _saidas.AddRange(new float[saidas]);



    }


    private void neuronios_HV(List<int> aux)
    {
        if (neuronios != null)
            neuronios.Clear();

       
        List<float>[] auxf = new List<float>[aux.Count];
        neuronios = new List<List<float>>(auxf);





        for (int i = 0; i < aux.Count; i++)
        {
            float[] auxg = new float[aux[i]];

            if (i == aux.Count - 1)
            {
                float[] auxgf = new float[_saidas.Count];
                neuronios[i] = new List<float>(auxgf);
            }
            else
            {
                neuronios[i] = new List<float>(auxg);

            }
        }




    }

    private void botarPesos(List<int> aux)
    {
        List<float>[] auxf = new List<float>[aux.Count];
        pesos = new List<List<float>>(auxf);

        for (int i = 0; i < neuronios.Count; i++)
        {


            if (i == 0)
            {
                float[] auxgf = new float[_entradas.Count * neuronios[0].Count];
                pesos[0] = new List<float>(auxgf);

            }
            else
            {

                float[] auxg = new float[neuronios[i].Count * neuronios[i - 1].Count];
                pesos[i] = new List<float>(auxg);

            }
        }
    }
    private void botarPesos2(List<int> aux)
    {
        List<float>[] auxf = new List<float>[aux.Count];
        pesos = new List<List<float>>(auxf);

        for (int i = 0; i < neuronios.Count; i++)
        {
            if(i == 0 )
            {
                float[] auxg = new float[neuronios[i].Count];
                pesos[i] = new List<float>(auxg);
            }
            else{
                float[] auxg = new float[neuronios[i].Count * neuronios[i - 1].Count];
                pesos[i] = new List<float>(auxg);
            }


          
        }
    }

    private void neuralizar(List<int> MatrizNeural)
    {
        if (travaNeural == false)
        {


            for (int i = 0; i < neuronios.Count; i++)
            {
                if (travaNeural)
                    break;
                // neuronios y

                if (i == 0)
                {

                    for (int aux = 0; aux < neuronios[i].Count; aux++)
                    {
                        if (travaNeural)
                            break;
                        for (int aux2 = 0; aux2 < _entradas.Count; aux2++)
                        {
                            if (travaNeural)
                                break;
                            neuronios[i][aux] += _entradas[aux2] * pesos[i][aux2 + (aux * _entradas.Count)];

                        }

                    }

                    for (int aux = 0; aux < neuronios[i].Count; aux++)
                    {
                        if (travaNeural)
                            break;
                        if (neuronios[i][aux] > 0)
                        {
                            for (int auxY = 0; auxY < neuronios[i + 1].Count; auxY++)
                            {
                                if (travaNeural)
                                    break;


                                if (i + 1 == neuronios.Count - 1)
                                {
                                    neuronios[i + 1][auxY] = 1;


                                }
                                else
                                {



                                    neuronios[i + 1][auxY] += (neuronios[i][aux]) * pesos[i + 1][aux + (auxY * neuronios[i].Count)];

                                }

                            }

                        }
                        else
                        {
                            neuronios[i][aux] = 0;
                        }


                    }

                }

                if (i > 0)
                {
                    if (i == neuronios.Count - 1)
                    {

                        for (int y = 0; y < neuronios[i].Count; y++)
                        {
                            if (travaNeural)
                                break;

                            if (neuronios[i][y] > 0)
                            {

                                _saidas[y] = binario == true ?
                                    neuronios[i][y] > 1000 ? 1000 :
                                    neuronios[i][y] :
                                    Mathf.Log10(neuronios[i][y]);

                            }
                            else
                            {
                                neuronios[i][y] *= 0.1f;
                                _saidas[y] = binario == true ?
                                   neuronios[i][y] < -1000 ? -1000 :
                                   neuronios[i][y] :
                                   -Mathf.Log10(Mathf.Abs(neuronios[i][y]));
                            }
                        }
                    }
                    else
                    {

                        for (int y = 0; y < neuronios[i].Count; y++)
                        {
                            if (travaNeural)
                                break;

                            if (independencia == true)
                            {
                                if (Random.Range(0, 100) < 20)
                                {
                                    neuronios[i][y] = 0;
                                }
                            }

                            if (neuronios[i][y] > 0)
                            {

                                for (int y2 = 0; y2 < neuronios[i + 1].Count; y2++)
                                {
                                    if (travaNeural)
                                        break;
                                    neuronios[i + 1][y2] += (neuronios[i][y]) * pesos[i + 1][y + (y2 * (neuronios[i].Count))];
                                    neuronios[i + 1][y2] = binario == true ? neuronios[i + 1][y2] > 1000 ? 1000 : neuronios[i + 1][y2] : neuronios[i + 1][y2];


                                }

                            }
                            else
                            {
                                neuronios[i][y] = 0;
                            }




                        }
                    }

                }



            }

            neuronios_HV(MatrizNeural);
        }

    }
    private void neuralizar2(List<int> MatrizNeural)
    {
        if (travaNeural == false && _entradas.Count <= neuronios[0].Count)
        {


            for (int i = 0; i < neuronios.Count; i++)
            {
                if (travaNeural)
                    break;
                // neuronios y

                if (i == 0)
                {
                    for(int e = 0; e < _entradas.Count; e++)
                    {
                        neuronios[0][e] += _entradas[e] * pesos[0][e];

                       
                    }

                    for (int aux = 0; aux < neuronios[i].Count; aux++)
                    {
                        if (travaNeural)
                            break;
                        if (neuronios[i][aux] > 0)
                        {
                            for (int auxY = 0; auxY < neuronios[i + 1].Count; auxY++)
                            {
                                if (travaNeural)
                                    break;


                                if (i + 1 == neuronios.Count - 1)
                                {
                                    neuronios[i + 1][auxY] = 1;


                                }
                                else
                                {



                                    neuronios[i + 1][auxY] += (neuronios[i][aux]) * pesos[i + 1][aux + (auxY * neuronios[i].Count)];

                                }

                            }

                        }
                        else
                        {
                            neuronios[i][aux] = 0;
                        }


                    }


                }

                if (i > 0)
                {
                    if (i == neuronios.Count - 1)
                    {

                        for (int y = 0; y < neuronios[i].Count; y++)
                        {
                            if (travaNeural)
                                break;

                            if (neuronios[i][y] > 0)
                            {

                                _saidas[y] = binario == true ?
                                    neuronios[i][y] > 1000 ? 1000 :
                                    neuronios[i][y] :
                                    Mathf.Log10(neuronios[i][y]);

                            }
                            else
                            {
                                neuronios[i][y] *= 0.1f;
                                _saidas[y] = binario == true ?
                                   neuronios[i][y] < -1000 ? -1000 :
                                   neuronios[i][y] :
                                   -Mathf.Log10(Mathf.Abs(neuronios[i][y]));
                            }
                        }
                    }
                    else
                    {

                        for (int y = 0; y < neuronios[i].Count; y++)
                        {
                            if (travaNeural)
                                break;

                            if (independencia == true)
                            {
                                if (Random.Range(0, 100) < 20)
                                {
                                    neuronios[i][y] = 0;
                                }
                            }

                            if (neuronios[i][y] > 0)
                            {

                                for (int y2 = 0; y2 < neuronios[i + 1].Count; y2++)
                                {
                                    if (travaNeural)
                                        break;
                                    neuronios[i + 1][y2] += (neuronios[i][y]) * pesos[i + 1][y + (y2 * (neuronios[i].Count))];
                                    neuronios[i + 1][y2] = binario == true ? neuronios[i + 1][y2] > 1000 ? 1000 : neuronios[i + 1][y2] : neuronios[i + 1][y2];


                                }

                            }
                            else
                            {
                                neuronios[i][y] = 0;
                            }




                        }
                    }

                }



            }

            neuronios_HV(MatrizNeural);
        }

    }



    // mudei
    private void att_entrada(List<float> aux)
    {
        entrada(aux.Count);
        _entradas = new List<float>();
        _entradas.AddRange(aux);
       /* for (int aux2 = 0; aux2 < _entradas.Count; aux2++)
        {
            _entradas[aux2] = aux[aux2];
        } */
    }
    private List<float> pegar_saida()
    {
        return _saidas;
    }

    private void randomizar_pesos()
    {

        for (int i = 0; i < pesos.Count; i++)
        {
            for (int x = 0; x < pesos[i].Count; x++)
            {

                pesos[i][x] = Random.Range(-1000, 1000);

            }
        }
    
    }
    private void randomizar_pesosV2(int seed, int amplitude)
    {
        for (int i = 0; i < pesos.Count; i++)
        {
            for (int x = 0; x < pesos[i].Count; x++)
            {

                pesos[i][x] =
                    (int)(2000 * (Mathf.PerlinNoise((
                        (i * Mathf.Sqrt(seed) + pesos.Count * Mathf.Sqrt(seed)) / amplitude),
                    (x * Mathf.Sqrt(seed) + pesos[i].Count * Mathf.Sqrt(seed)) / amplitude))) - 1000;


            }
        }
    }




    // mostrar pesos

    //definir pesos
    int contador;
    private void definir_pesos(float[][] aux)
    {

        for (int i = 0; i < aux.Length; i++)
        {
            for (int x = 0; x < aux[i].Length; x++)
            {

                pesos[i][x] = aux[i][x];
            }
        }
    }


    private void IO_att()
    {
        _entradas = new List<float>();
        _saidas = new List<float>();


    }
    private bool RandomizarPesos = false;
    private void iniciarIA(int entradas, int saidas, List<int> MatrizNeural)
    {
        IO_att();
        entrada(entradas);
        saida(saidas);
       
        neuronios_HV(MatrizNeural);
        botarPesos(MatrizNeural);
       // botarPesos2(MatrizNeural);
        if (RandomizarPesos == false)
        {
            randomizar_pesos();
        }
    }
    bool travaNeural;
    private void GerarMutacao()
    {
        for (int i = 0; i < pesos.Count; i++)
        {
            for (int x = 0; x < pesos[i].Count; x++)
            {
                pesos[i][x] += Random.Range(-100, 100);
                pesos[i][x] = pesos[i][x] > 1000 ? 1000 : pesos[i][x] < -1000 ? -1000 : pesos[i][x];
            }
        }
    }

   



}
