using fp_stack.core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using fp_stack.core.Models;

namespace fp_stack.web.ViewComponents
{
    public class NoticiasViewComponent : ViewComponent
    {
        NoticiaService _service;
        public NoticiasViewComponent()
        {
            _service = new NoticiaService();
        }
        public async Task<IViewComponentResult> InvokeAsync(int total, bool noticiasUrgentes=false)
        {
            var view = noticiasUrgentes ? "noticiasUrgentes" : "noticias";
            var itens = _service.GetItens(total);

            return View(view, itens);
        }


    }

}
