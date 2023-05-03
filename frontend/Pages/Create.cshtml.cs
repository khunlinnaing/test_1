using frontend;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class CreateModel : PageModel
{
    private readonly EmployeeController _apiService;

    public CreateModel(EmployeeController apiService)
    {
        _apiService = apiService;
    }

    [BindProperty]
    public Customer Customer { get; set; }

    public void OnGet()
    {
        Customer = new Customer();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        // await _apiService.CreateCustomerAsync(Customer);
        var result = await _apiService.CreateCustomerAsync(Customer);
        
        if (!result.IsSuccessStatusCode) { 
            var errorContent = await result.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject(errorContent);
            TempData["ApiErrorMessage"] =error;
            return Page();
        }
        return RedirectToPage("/Index");
    }
}
