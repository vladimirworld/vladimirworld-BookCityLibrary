﻿using Microsoft.AspNetCore.Components;

namespace BlazorAppTest.Services;

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