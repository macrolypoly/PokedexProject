using PokedexProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokedexProject.Controllers
{
    [Authorize]
    public class PokemonController : Controller
    {
        // GET: Pokemon
        public ActionResult Index()
        {
            var model = new PokemonListItem[0];
            return View(model);
        }
    }
}