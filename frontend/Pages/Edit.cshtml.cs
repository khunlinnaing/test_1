using frontend;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

public class EditModel : PageModel
{
    private readonly EmployeeController _apiService;

    public EditModel(EmployeeController apiService)
    {
        _apiService = apiService;
    }

    [BindProperty]
    public Customer Customer { get; set; }
    public async Task OnGetAsync(int id)
    {
        Customer = await _apiService.GetCustomerAsync(id);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Console.WriteLine(ModelState.IsValid);
        if (!ModelState.IsValid)
        {
            return Page();
        }
        var result=await _apiService.UpdateCustomerAsync(Customer.id,Customer);
        if (!result.IsSuccessStatusCode) { 
            var errorContent = await result.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject(errorContent);
            TempData["ApiErrorMessage"] =error;
            return Page();
        }
        
        return RedirectToPage("/Index");
    }
}
