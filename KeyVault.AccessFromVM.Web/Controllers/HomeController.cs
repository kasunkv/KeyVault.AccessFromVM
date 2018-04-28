using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KeyVault.AccessFromVM.Web.Models;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Azure.KeyVault;

namespace KeyVault.AccessFromVM.Web.Controllers
{
    public class HomeController : Controller
    {
        private static string keyVaultBaseUrl = "https://kvk-keyvault-1.vault.azure.net";
        private static string secretKey = "mysecret";

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            var tokenProvider = new AzureServiceTokenProvider();
            var vaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(tokenProvider.KeyVaultTokenCallback));

            var secret = await vaultClient.GetSecretAsync($"{keyVaultBaseUrl}/secrets/{secretKey}").ConfigureAwait(false);
            ViewBag.Secret = secret.Value;

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
