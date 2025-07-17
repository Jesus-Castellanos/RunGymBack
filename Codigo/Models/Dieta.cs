using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RunGym.Models
{
    public class Dieta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Desayuno { get; set; }
        public string Almuerzo { get; set; }
        public string Cena { get; set; }
        public string Snacks { get; set; }

        [ForeignKey("TipoDietaId")]
        public int TipoDietaId { get; set; }

        [JsonIgnore]
        public TipoDieta? TipoDieta { get; set; }
    }
}