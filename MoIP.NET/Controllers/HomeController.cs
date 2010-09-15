using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using MoIP;

namespace MoIPNET.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            InstrucaoMoIP moip = new InstrucaoMoIP();

            moip.Key = "sua_key";
            moip.Token = "seu_token";
            moip.Razao = "Pagamento de testes";
            moip.Valor = 150.25M;
            MoIPResposta resposta =  moip.Enviar();

            ViewData["Token"] = resposta.Token;
            ViewData["Message"] = "Bem vindo ao exemplo de integração MoIP.NET";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
