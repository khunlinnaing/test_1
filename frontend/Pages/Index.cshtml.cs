using frontend;
using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Web;

public class IndexModel : PageModel
{
    private readonly EmployeeController _apiService;
    public IndexModel(EmployeeController apiService)
    {
        _apiService = apiService;
    }

    public PaginatedData<Customer> Customers;
    [BindProperty]
    public Customer Customer { get; set; }
    public HttpRequest page{get;}
    public async Task OnGetAsync()
    {
        int myParam;
        string page=HttpContext.Request.Query["page"];
        if(page != null){
            myParam = int.Parse(page);
            Customers = await _apiService.GetCustomersAsync(myParam);
        }else{
            Customers = await _apiService.GetCustomersAsync(1);
        }
    }
    public async Task<IActionResult> OnPostAsync(IFormCollection form)
    {
        var id=Int16.Parse(form["forecast.id"]);
        Customer = await _apiService.GetCustomerAsync(id);
        await _apiService.DeleteCustomerAsync(id);
        return RedirectToPage("/Index");
    }
}
