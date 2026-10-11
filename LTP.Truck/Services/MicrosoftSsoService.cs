using Microsoft.Identity.Client;

namespace LTP.Truck.Services
{
  public sealed class MicrosoftSsoService
  {
    private static readonly string[] Scopes = { "User.Read" };

    public async Task<MicrosoftSsoAccount> SignInAsync(
      CancellationToken cancellationToken = default)
    {
      string clientId = GetRequiredSetting("SSO_MICROSOFT_CLIENT_ID");
      string tenantId = GetRequiredSetting("SSO_MICROSOFT_TENANT_ID");
      string? redirectUri = Environment.GetEnvironmentVariable(
        "SSO_MICROSOFT_REDIRECT_URI");

      var builder = PublicClientApplicationBuilder
        .Create(clientId)
        .WithAuthority(
          $"https://login.microsoftonline.com/{tenantId.Trim()}");

      builder = string.IsNullOrWhiteSpace(redirectUri)
        ? builder.WithDefaultRedirectUri()
        : builder.WithRedirectUri(redirectUri.Trim());

      IPublicClientApplication application = builder.Build();
      AuthenticationResult result = await application
        .AcquireTokenInteractive(Scopes)
        .WithPrompt(Prompt.SelectAccount)
        .WithUseEmbeddedWebView(false)
        .ExecuteAsync(cancellationToken)
        .ConfigureAwait(true);

      string loginId = result.Account?.Username?.Trim() ?? string.Empty;
      if (string.IsNullOrWhiteSpace(loginId))
      {
        throw new InvalidOperationException(
          "Microsoft Entra ID không trả về định danh đăng nhập của người dùng.");
      }

      return new MicrosoftSsoAccount(loginId);
    }

    private static string GetRequiredSetting(string name)
    {
      string? value = Environment.GetEnvironmentVariable(name);
      if (string.IsNullOrWhiteSpace(value))
      {
        throw new InvalidOperationException(
          $"Chưa cấu hình biến môi trường {name} cho Microsoft SSO.");
      }

      return value.Trim();
    }
  }

  public sealed record MicrosoftSsoAccount(string LoginId);
}
