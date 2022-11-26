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

    /// <summary>
    /// Request Identifier.
    /// </summary>/
    public string? RequestId { get; set; }

    /// <summary>
    /// Bool that indicates if show Request Identifier when is available.
    /// </summary>
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    /// <summary>
    /// On Get method that sets the Request Identifier
    /// </summary>
    public void OnGet()
    {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
    }
}
