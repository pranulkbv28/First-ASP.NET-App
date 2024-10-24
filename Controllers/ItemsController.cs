using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Models;

namespace MyApp.Controllers
{
    public class ItemsController : Controller
    {
        //public IActionResult Overview()
        //{
        //    var item = new Item()
        //    {
        //        Name= "laptop"
        //    };
        //    return View(item);
        //}

        //public IActionResult Edit(int id)
        //{
        //    return Content($"id = {id}");


        //    /*Both of them give the same o/p
        //     * items/edit?id=8
        //     * /items/edit/8 ->  this will only work if the variable is "id" and not anything else as in the Program.cs, it is mentioned like so in the pattern.
        //     */
        //}

        private readonly MyAppContext _myAppContext;

        public ItemsController(MyAppContext context)
        {
            _myAppContext = context;
        }

        public async Task<IActionResult> Index()
        {
            var item = await  _myAppContext.Items.Include(s => s.SerialNumber)
                .Include(c => c.Category)
                .Include(ic => ic.ItemClients)
                .ThenInclude(c => c.Client)
                .ToListAsync();
            return View(item);
        }

        //This is for GET request
        public IActionResult Create()
        {
            ViewData["Categories"] = new SelectList(_myAppContext.Categories, "Id", "Name");
            return View();
        }
        //This is for POST request
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id, Name, Price, CategoryId")] Item item)
        {
            if(ModelState.IsValid)
            {
                _myAppContext.Items.Add(item);
                await _myAppContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(item);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Categories"] = new SelectList(_myAppContext.Categories, "Id", "Name");
            var item = await _myAppContext.Items.FirstOrDefaultAsync(x=> x.Id == id );
            return View(item);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Price, CategoryId")] Item item)
        {
            if (ModelState.IsValid)
            {
                _myAppContext.Update(item);
                await _myAppContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(item);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _myAppContext.Items.FirstOrDefaultAsync(x => x.Id == id);
            return View(item);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _myAppContext.Items.FindAsync(id);
            
            if (item == null)
            {
                return NotFound();
            }

            _myAppContext.Items.Remove(item);

            await _myAppContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}
