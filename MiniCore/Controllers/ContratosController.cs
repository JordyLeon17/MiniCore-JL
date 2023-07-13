using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniCore.Data;
using MiniCore.Models;

namespace MiniCore.Controllers
{
    public class ContratosController : Controller
    {
        private readonly MiniCoreContext _context;

        public ContratosController(MiniCoreContext context)
        {
            _context = context;
        }

        // GET: Contratos
        public async Task<IActionResult> Index()
        {
            var miniCoreContext = _context.Contrato.Include(c => c.Cliente);
            return View(await miniCoreContext.ToListAsync());
        }

        // GET: Contratos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Contrato == null)
            {
                return NotFound();
            }

            var contrato = await _context.Contrato
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(m => m.ContratoId == id);
            if (contrato == null)
            {
                return NotFound();
            }

            return View(contrato);
        }

        // GET: Contratos/Create
        public IActionResult Create()
        {
            ViewData["ClienteID"] = new SelectList(_context.Cliente, "ClienteId", "Nombre");
            return View();
        }

        // POST: Contratos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContratoId,ClienteID,Nombre,Monto,Fecha")] Contrato contrato)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contrato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteID"] = new SelectList(_context.Cliente, "ClienteId", "ClienteId", contrato.ClienteID);
            return View(contrato);
        }

        // GET: Contratos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Contrato == null)
            {
                return NotFound();
            }

            var contrato = await _context.Contrato.FindAsync(id);
            if (contrato == null)
            {
                return NotFound();
            }
            ViewData["ClienteID"] = new SelectList(_context.Cliente, "ClienteId", "ClienteId", contrato.ClienteID);
            return View(contrato);
        }

        // POST: Contratos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContratoId,ClienteID,Nombre,Monto,Fecha")] Contrato contrato)
        {
            if (id != contrato.ContratoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contrato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContratoExists(contrato.ContratoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteID"] = new SelectList(_context.Cliente, "ClienteId", "ClienteId", contrato.ClienteID);
            return View(contrato);
        }

        // GET: Contratos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Contrato == null)
            {
                return NotFound();
            }

            var contrato = await _context.Contrato
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(m => m.ContratoId == id);
            if (contrato == null)
            {
                return NotFound();
            }

            return View(contrato);
        }

        // POST: Contratos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Contrato == null)
            {
                return Problem("Entity set 'MiniCoreContext.Contrato'  is null.");
            }
            var contrato = await _context.Contrato.FindAsync(id);
            if (contrato != null)
            {
                _context.Contrato.Remove(contrato);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContratoExists(int id)
        {
          return _context.Contrato.Any(e => e.ContratoId == id);
        }


        public async Task<IActionResult> Cruce(DateTime fechaini, DateTime fechafin)
        {
            var Response = new List<Cruce>();
            var a = (from c in _context.Cliente
                     join o in _context.Contrato on c.ClienteId equals o.ClienteID
                     where (o.Fecha >= fechaini && o.Fecha <= fechafin)
                     select new Cruce
                     {
                         Id = c.ClienteId,
                         Nombre = c.Nombre,
                         SumaMontos = o.Monto

                     }).ToList();

            var usuariosID = a.Select(a => a.Id).Distinct().ToList();

            foreach(var usuario in usuariosID)
            {
                var Objeto = new Cruce();

                var elhurvo = a.Where(c => c.Id == usuario);
                float? suma= 0;
                foreach (var monto in elhurvo)
                {
                    suma += monto.SumaMontos;
                }

                Objeto.SumaMontos = suma;
                var nombre = elhurvo.Select(a => a.Nombre).FirstOrDefault();
                Objeto.Nombre = nombre;

                Response.Add(Objeto);

            }


            return View(Response);

        }

    }
}
