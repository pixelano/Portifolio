using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class OrganizadorDeTarefas : MonoBehaviour
{
    [SerializeField]
    public List<tarefas> ListaDeTarefas;

    // carregar dados
    private void Awake()
    {
        string localDeArquivo = Path.Combine(Application.persistentDataPath, "ListaDeTarefas.dat");
        FileStream _saveListaDeTarefas;
        if (!File.Exists(localDeArquivo))
        {
            _saveListaDeTarefas = new FileStream(localDeArquivo, FileMode.Create);
            ListaDeTarefas = new List<tarefas>();
            return;
        }
        BinaryFormatter bf = new BinaryFormatter();
        List<tarefas> _saveLista;
        using (FileStream fs = new FileStream(localDeArquivo, FileMode.Open))
        {
            // Deserializa o objeto do FileStream
            _saveLista = (List<tarefas>)bf.Deserialize(fs);
        }
        ListaDeTarefas = _saveLista;
    }

    private void OnDestroy()
    {
        string localDeArquivo = Path.Combine(Application.persistentDataPath, "ListaDeTarefas.dat");
        BinaryFormatter bf = new BinaryFormatter();
        using (FileStream fs = new FileStream(localDeArquivo, FileMode.Create))
        {
            // Deserializa o objeto do FileStream
            bf.Serialize(fs, ListaDeTarefas);
        }
    }
}
[System.Serializable]
public class tarefas
{
    public string titulo,escopo;
    
}