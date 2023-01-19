using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VillaAPI.models
{
	public class VillaNumber
	{
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VillaNo { get; set; }


        // Here you will create a foreign key relationship with villa class
        // So have to create a navigation property 'public Villa Villa { get; set; }'
        // and refrefenve the name in ForegnKey("Villa")
        [ForeignKey("Villa")]
        public int Id { get; set; }

        public Villa Villa { get; set; }

        public string SpecislDetails { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

    }
}

