using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;

namespace frontend.Pages;

public class PrivacyModel : PageModel
{
    private readonly IWebHostEnvironment _hostingEnvironment;

    public PrivacyModel(IWebHostEnvironment hostingEnvironment)
    {
        _hostingEnvironment = hostingEnvironment;
    }
    public void OnGet(int page=1)
    {
        Console.WriteLine(page);
    }
}

