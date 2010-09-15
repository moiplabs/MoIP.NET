using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Net;
using System.IO;
using System.Text;

namespace MoIP
{
    public class InstrucaoMoIP
    {
        
        public string Token
        {
            get;
            set;
        }

        public string Key
        {
            get;
            set;
        }

        public string Razao
        {
            get;
            set;
        }

        public decimal Valor
        {
            get;
            set;
        }

        private string GetXML()
        {
            XmlDocument document = new XmlDocument();

            XmlElement RazaoNode = document.CreateElement("Razao");
            RazaoNode.InnerText = Razao;

            XmlElement ValoresNode = document.CreateElement("Valores");
            XmlElement ValorNode = document.CreateElement("Valor");

            XmlAttribute moeda = document.CreateAttribute("moeda");
            moeda.Value = "BRL";

            ValorNode.Attributes.Append(moeda);
            ValorNode.InnerText = String.Format("{0:0.00}", Valor).Replace(',','.');

            ValoresNode.AppendChild(ValorNode);

            XmlNode EnviarInstrucao = document.CreateElement("EnviarInstrucao");
            XmlNode InstrucaoUnica = EnviarInstrucao.AppendChild(document.CreateNode(XmlNodeType.Element, "InstrucaoUnica", ""));

            InstrucaoUnica.AppendChild(RazaoNode);
            InstrucaoUnica.AppendChild(ValoresNode);

            return EnviarInstrucao.OuterXml;
        }

        private MoIPResposta GetMoIPRespostaFromXML(string Response)
        {
            XmlDocument XmlResposta = new XmlDocument();
            XmlResposta.LoadXml(Response);

            MoIPResposta resposta = new MoIPResposta();
            resposta.ID = XmlResposta.DocumentElement.SelectSingleNode("//Resposta//ID").InnerText;
            resposta.Token = XmlResposta.DocumentElement.SelectSingleNode("//Resposta//Token").InnerText;
            resposta.Sucesso = XmlResposta.DocumentElement.SelectSingleNode("//Resposta/Status").InnerText.Equals("Sucesso");

            return resposta;
        }

        public MoIPResposta Enviar()
        {
            if (String.IsNullOrEmpty(Token) || String.IsNullOrEmpty(Key))
                throw new InvalidOperationException("Você deve informar seu token e sua key de forma correta antes de enviar uma instrução.");

            if (String.IsNullOrEmpty(Razao) || Valor == null)
                throw new InvalidOperationException("Você deve informar a razão e o valor do pagamento");


            string XMLInstrucao = GetXML();

            WebClient client = new WebClient();

            string URI = "https://desenvolvedor.moip.com.br/sandbox/ws/alpha/EnviarInstrucao/Unica";
            string auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(Token + ":" + Key));
            client.Headers.Add("Authorization: Basic "+auth);
            client.Headers.Add("User-Agent: Mozilla/4.0");
            byte[] ResponseArray = client.UploadData(URI, "POST", Encoding.UTF8.GetBytes(XMLInstrucao));
            string Response = Encoding.UTF8.GetString(ResponseArray);

            return GetMoIPRespostaFromXML(Response);
        }
    }

    public class MoIPResposta
    {
        public string ID { get; set; }
        public string Token { get; set; }
        public bool Sucesso { get; set; }
    }
}
