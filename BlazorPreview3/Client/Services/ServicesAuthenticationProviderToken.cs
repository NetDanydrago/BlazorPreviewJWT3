using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using BlazorPreview3.Shared;

namespace BlazorPreview3.Client.Services
{
    public class ServicesAuthenticationProviderToken : AuthenticationStateProvider
    {
        private readonly IJSRuntime JSRuntime;
        private readonly HttpClient HttpClient;
        private AuthenticationState Anonimo => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public ServicesAuthenticationProviderToken(IJSRuntime jSRuntime, HttpClient httpClient)
        {
            JSRuntime = jSRuntime;
            HttpClient = httpClient;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
