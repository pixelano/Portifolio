using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using SerializarComXml.Model;

namespace SerializarComXml
{
  class Program
  {
    static void Main(string[] args)
    {
      string nomeArquivo = DateTime.Now.ToString().Replace(@"/", "").Replace(" ", "").Replace(":", "") + ".xml";
      //SerializarUmObjeto(nomeArquivo);
      //DesSerializarUmObjeto();

      //SerializarListaDeObjetos(nomeArquivo);
      DesSerializarListaDeObjeto();
    }

    #region Serialização
    private static void SerializarUmObjeto(string nomeArquivo)
    {
      Usuario usuario = new Usuario()
                                      {
                                        Nome = "Fabiana",
                                        Sobrenome = "Allana de Paula",
                                        Email = "ffabianaallanadepaula@runup.com.br",
                                        Endereco = new Endereco()
                                        {
                                          Logradouro = "Rua 3",
                                          Cidade = "Goiânia",
                                          Estado = "GO",
                                          Bairro = "Água Branca",
                                          Numero = "160",
                                          Cep = "74723-200"
                                        }
                                      };

      using (StreamWriter stream = new StreamWriter(Path.Combine(@"C:\Users\davin\Documents\Serializar", nomeArquivo)))
      {
        XmlSerializer serializador = new XmlSerializer(typeof(Usuario));
        serializador.Serialize(stream, usuario);
      }
        
    }

    private static void DesSerializarUmObjeto()
    {
      Usuario usuario = null;
      using (StreamReader stream = new StreamReader(@"C:\Users\davin\Documents\Serializar\10032021001420.xml"))
      {
        XmlSerializer serializador = new XmlSerializer(typeof(Usuario));
        usuario = (Usuario)serializador.Deserialize(stream);
      }

    }

    #endregion

    #region Desserialização

    private static void SerializarListaDeObjetos(string nomeArquivo)
    {
      RepositorioDeUsuario repositorio = new RepositorioDeUsuario();

      using (StreamWriter stream = new StreamWriter(Path.Combine(@"C:\Users\davin\Documents\Serializar", nomeArquivo)))
      {
        XmlSerializer serializador = new XmlSerializer(typeof(List<Usuario>));
        serializador.Serialize(stream, repositorio.Usuarios);
      }

    }

    private static void DesSerializarListaDeObjeto()
    {
      List<Usuario> usuarios = null;
      using (StreamReader stream = new StreamReader(@"C:\Users\davin\Documents\Serializar\10032021002059.xml"))
      {
        XmlSerializer serializador = new XmlSerializer(typeof(List<Usuario>));
        usuarios = (List<Usuario>)serializador.Deserialize(stream);
      }
    }

    #endregion
  }
}
