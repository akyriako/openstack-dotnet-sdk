using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenStack.Iam.Samples.Mvc.Models;
using OpenStack.Iam.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OpenStack.Iam.Samples.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthenticationAndTokenManagementClient _authenticationAndTokenManagementClient;
        private readonly IConfiguration _configuration;
        private readonly AuthenticationAndTokenManagementClientOptions _authenticationAndTokenManagementClientOptions;
            
        public HomeController(ILogger<HomeController> logger,
            IAuthenticationAndTokenManagementClient authenticationAndTokenManagementClient,
            IConfiguration configuration,
            AuthenticationAndTokenManagementClientOptions authenticationAndTokenManagementClientOptions)
        {
            _logger = logger;
            _authenticationAndTokenManagementClient = authenticationAndTokenManagementClient;
            _configuration = configuration;
            _authenticationAndTokenManagementClientOptions = authenticationAndTokenManagementClientOptions;
        }

        public async Task<IActionResult> Index()
        {
            var authToken = await _authenticationAndTokenManagementClient.
                GetTokenPasswordAuthenticationScopedAuthorizationAsync(
                _authenticationAndTokenManagementClientOptions.UserId,
                _authenticationAndTokenManagementClientOptions.TenantId,
                _authenticationAndTokenManagementClientOptions.Password);


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
