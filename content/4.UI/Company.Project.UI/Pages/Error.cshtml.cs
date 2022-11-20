using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Company.Project.UI.Pages;

/// <summary>
/// Error Model Class
/// </summary>
[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
public class ErrorModel : PageModel
{
    private readonly ILogger<ErrorModel> _logger;

    /// <summary>
    /// Error Model Class Contructor
    /// </summary>
    /// <param name="logger"></param>
    public ErrorModel(ILogger<ErrorModel> logger)
    {
        _logger = logger;
    }

    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    public void OnGet()
    {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
    }
}
