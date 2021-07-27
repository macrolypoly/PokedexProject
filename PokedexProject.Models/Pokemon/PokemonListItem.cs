using PokedexProject.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject.Models
{
    public class PokemonListItem
    {
        public Guid OwnerId { get; set; }
        public int PokemonId { get; set; }
        public string PokemonName { get; set; }
        public PokeType Type { get; set; }
        public PokeType Type2 { get; set; }
        public ICollection<PokeRoute> RoutesFound { get; set; }

    }
public enum PokeType
{
    [Display(Name = "Normal Type")]
    Normal = 1,
    [Display(Name = "Fire Type")]
    Fire = 2,
    [Display(Name = "Water Type")]
    Water,
    [Display(Name = "Grass Type")]
    Grass,
    [Display(Name = "Electric Type")]
    Electric,
    [Display(Name = "Ice Type")]
    Ice,
    [Display(Name = "Fighting Type")]
    Fighting,
    [Display(Name = "Poison Type")]
    Poison,
    [Display(Name = "Ground Type")]
    Ground,
    [Display(Name = "Flying Type")]
    Flying,
    [Display(Name = "Psychic Type")]
    Psychic,
    [Display(Name = "Bug Type")]
    Bug,
    [Display(Name = "Rock Type")]
    Rock,
    [Display(Name = "Ghost Type")]
    Ghost,
    [Display(Name = "Dark Type")]
    Dark,
    [Display(Name = "Dragon Type")]
    Dragon,
    [Display(Name = "Steel Type")]
    Steel,
    [Display(Name = "Fairy Type")]
    Fairy,
    [Display(Name = "No Type")]
    None

}
}
