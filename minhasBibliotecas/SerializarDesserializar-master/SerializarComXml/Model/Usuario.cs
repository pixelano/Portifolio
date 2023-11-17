using System;
using System.Runtime.Serialization;

namespace SerializarComXml.Model
{
  [Serializable()]
  public class Usuario : ISerializable
  { 
    public string Nome { get; set; }  
    public string Sobrenome { get; set; }
    public string Email { get; set; }
    public Endereco Endereco { get; set; }

    public Usuario() { }

    #region Serialização

    /// <summary>
    ///  A função desserializar (remove dados do objeto do arquivo)
    /// </summary>
    /// <param name="info"></param>
    /// <param name="ctxt"></param>
    public Usuario(SerializationInfo info, StreamingContext ctxt)
    {
      //Pegue os valores de informações e atribua-os às propriedades
      Nome = (string)info.GetValue("Nome", typeof(string));
      Sobrenome = (string)info.GetValue("Sobrenome", typeof(string));
      Email = (string)info.GetValue("Email", typeof(string));
      Endereco = (Endereco)info.GetValue("Endereco", typeof(Endereco));
    }

    /// <summary>
    /// Função de serialização (armazena dados do objeto no arquivo)
    /// SerializationInfo contém os pares de valores-chave
    /// StreamingContext pode conter informações adicionais, mas não estamos usando aqui
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      try
      {
        info.AddValue("Nome", Nome);
        info.AddValue("Sobrenome", Sobrenome);
        info.AddValue("Email", Email);
        info.AddValue("Endereco", Endereco);
      }
      catch (Exception ex)
      {

        throw new Exception(ex.Message);
      }
    }

    #endregion
  }
}
