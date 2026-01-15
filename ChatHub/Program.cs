using ChatHub.Components;
using Microsoft.AspNetCore.ResponseCompression; 
using ChatHub;

var builder = WebApplication.CreateBuilder(args);

// we need to add signalr to our builder services before building app, otherwise we can use it
builder.Services.AddSignalR();

// this isn't necessary but after reading through docs, it's a good idea to improve response time if there are many users
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

// we added compresssion, still need to use via app call
app.UseResponseCompression();

app.UseAntiforgery();
app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// mapping our end point, or controllers
app.MapHub<MyChatHub>("/chathub");

app.Run();