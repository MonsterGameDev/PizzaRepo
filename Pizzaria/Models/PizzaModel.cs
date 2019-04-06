using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzaria.Models
{
    public class PizzaModel
    {
        private List<Pizza> _pizzaList;
        public PizzaModel()
        {
            _pizzaList = createPizzaList();
        }

        public List<Pizza> findAll()
        {
            return _pizzaList;
            
        }

        public Pizza find(string id)
        {
            var result = _pizzaList.Find(p => p.Id == id);
            return result;
        }

        private List<Pizza> createPizzaList()
        {
            var result = new List<Pizza>();

            result.Add(new Pizza
            {
                Id = "p01",
                Name = "Scacciafiga",
                Description = "Tomat, fiordilatte mozzarella, gorgonzola, salsiccia, løg, chili",
                Photo = "Scacciafiga.png",
                Price = 99.00
            });
            result.Add(new Pizza
            {
                Id = "p02",
                Name = "Calabrese",
                Description = "Tomat, fiordilatte mozzarella, nduja, løg, origano. Nduja er et typisk produkt af Calabria ... ligner en meget blød og sterk Sobrasada/Chorizo!",
                Photo = "Calabreze.png",
                Price = 82.00
            });
            result.Add(new Pizza
            {
                Id = "p03",
                Name = "Nduja e Gorgonzola",
                Description = "Tomat, mozzarella, nduja, gorgonzola, basilikum. Nduja er et typisk produkt af Calabria ... ligner en meget blød og sterk Sobrasada/Chorizo!",
                Photo = "Nduja_e_Gorgonzola.png",
                Price = 68.00
            });
            result.Add(new Pizza
            {
                Id = "p04",
                Name = "Firarielli e salsiccia",
                Description = "Tomat, fiordilatte mozzarella, Friarielli (Napolitansk broccoli), salsiccia toscana",
                Photo = "Firarielli.png",
                Price = 101.00
            });
            result.Add(new Pizza
            {
                Id = "p05",
                Name = "Veneziana",
                Description = "Fiordilatte mozzarella, løg, mascarpone. (dette er uden tomat!)",
                Photo = "Venezziana.png",
                Price = 91.00
            });
            result.Add(new Pizza
            {
                Id = "p06",
                Name = "Alla Pino",
                Description = "Fiordilatte mozzarella, salsiccia, portobello svampe, porcini svampe \"trifolati\", mascarpone og trøffel creme.",
                Photo = "Alla_Pino.png",
                Price = 72.00
            });
            result.Add(new Pizza
            {
                Id = "p07",
                Name = "Tartufata",
                Description = "Fiordilatte mozzarella, marineret porcini svampe, mascarpone, trøffelcreme, prosciutto cotto. Pizza Bianca...dette er UDEN tomat!!",
                Photo = "Tartufata.png",
                Price = 74.00
            });

            return result;
        }
    }
}
