using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CRM.DTOs.CustomerDTOs;

namespace CRM.AppWebMVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HttpClient _httpClientCRMAPI;
        //constructor que recibe una instancia de httpClientFactory para crear el cliente http
        public CustomerController(IHttpClientFactory httpClientFactory)
        {
            _httpClientCRMAPI = httpClientFactory.CreateClient("CRMAPI");
        }
        //metodo para mostrar la lista de clientes
        public async Task<IActionResult> Index(SearchQueryCustomerDTO searchQueryCustomerDTO, int CountRow = 0)
        {
            if (searchQueryCustomerDTO.SendRowCount == 0)
                searchQueryCustomerDTO.SendRowCount = 2;
            if (searchQueryCustomerDTO.Take == 0)
                searchQueryCustomerDTO.Take = 10;
            var result = new SearchResultCustomerDTO();
            //Realizar una solicitud http post para buscar clientes en el servicio web
            var response = await _httpClientCRMAPI.PostAsJsonAsync("/customer/search", searchQueryCustomerDTO);
            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadFromJsonAsync<SearchResultCustomerDTO>();
            result = result != null ? result : new SearchResultCustomerDTO();
            if (result.CountRow == 0 && searchQueryCustomerDTO.SendRowCount == 1)
                result.CountRow = CountRow;
            ViewBag.CountRow = result.CountRow;
            searchQueryCustomerDTO.SendRowCount = 0;
            ViewBag.SearchQuery = searchQueryCustomerDTO;
            return View(result);

        }
        public async Task<IActionResult> Details(int id)
        {
            var result = new GetIdResultCustomerDTO();
            //Realiza una solicitud http para get para obtener los datos del cliente por id
            var response = await _httpClientCRMAPI.GetAsync("/customer/" + id);
            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadFromJsonAsync<GetIdResultCustomerDTO>();
            return View(result ?? new GetIdResultCustomerDTO());
        }
        //Metodo para mostrar el formulario de creacion de un cliente 
        public ActionResult Create()
        {
            return View();
        }
        //Metodo para procesar la creacion de un cliente 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCustomerDTOs createCustomerDTOs)
        {
            try
            {
                var response = await _httpClientCRMAPI.PostAsJsonAsync("/customer", createCustomerDTOs);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Error = "Error al intentar guardar el registro";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            var result = new GetIdResultCustomerDTO();

            var response = await _httpClientCRMAPI.GetAsync("/customer/" + id);

            if (response.IsSuccessStatusCode)

                result = await response.Content.ReadFromJsonAsync<GetIdResultCustomerDTO>();

            return View(new EditCustomerDTO(result ?? new GetIdResultCustomerDTO()));
        }

        // Método para procesar la edición de un cliente

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, EditCustomerDTO editCustomerDTO)
        {
            try {
                // Realizar una solicitud HTTP PUT para editar el cliente
                var response = await _httpClientCRMAPI.PutAsJsonAsync("/customer", editCustomerDTO);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index)); }
                ViewBag.Error = "Error al intentar editar el registro";

                return View();
            }


            catch (Exception ex) {

                ViewBag.Error = ex.Message;
                return View();
            }

        }
    
            
    // Método para mostrar la página de confirmación de eliminación de un cliente
    public async Task<IActionResult> Delete(int id)
    {

        var result = new GetIdResultCustomerDTO();

       var response = await  _httpClientCRMAPI.GetAsync("/customer/" + id);

          if (response.IsSuccessStatusCode)

            result = await response.Content.ReadFromJsonAsync<GetIdResultCustomerDTO>();

              return View (result ?? new GetIdResultCustomerDTO());
    }

// Método para procesar la eliminación de un cliente

[HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult>Delete (int id, GetIdResultCustomerDTO getIdResultCustomerDTO)
    {
            try
            {
                var response = await _httpClientCRMAPI.DeleteAsync("/customer/" + id);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Error = "Error al intentar eliminar el registro";

                return View(getIdResultCustomerDTO);
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
                return View(getIdResultCustomerDTO);
            }

    }


}



}


