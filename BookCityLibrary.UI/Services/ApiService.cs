using System;
using System.Net.Http;
using Microsoft.AspNetCore.Components;

namespace BookCityLibrary.UI.Services;

public class ApiService
{
    public ApiService(HttpClient httpClient, NavigationManager navigationManager)
    {
        HttpClient = httpClient;
        NavigationManager = navigationManager;
        HttpClient.BaseAddress = new Uri(NavigationManager.BaseUri);
    }

    private HttpClient HttpClient { get; }
    private NavigationManager NavigationManager { get; }
}