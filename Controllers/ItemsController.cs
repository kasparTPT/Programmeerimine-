using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ListApp.Models;
using System.Threading.Tasks;

namespace ListApp.Controllers
{
    public class ItemsController : Controller
    {
        private readonly AppDbContext _context;

        public ItemsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string search)
        {
            var items = from i in _context.Items select i;

            if (!string.IsNullOrEmpty(search))
            {
                items = items.Where(i => i.Name.Contains(search) || i.Quantity.ToString().Contains(search));
            }
            return View(await items.ToListAsync());
        }

        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> AddItem(string name, int quantity)
        {
            var newItem = new Item { Name = name, Quantity = quantity };
            _context.Items.Add(newItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
