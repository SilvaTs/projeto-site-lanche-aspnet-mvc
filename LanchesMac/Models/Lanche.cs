using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models;

[Table("Lanches")]
public class Lanche
{
    [Key]
    public int LancheId { get; set; }

    [Required(ErrorMessage = "O nome do lanche deve ser informada")]
    [StringLength(80, MinimumLength = 10, ErrorMessage = "O {0} dever ter o mínimo {1} e no máximo 80")]
    [Display(Name = "Nome do Lanche")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "A descrição do lanche deve ser informada")]
    [StringLength(200, MinimumLength = 20, ErrorMessage = "O {0} dever ter o mínimo {1} e no máximo 80")]
    [Display(Name = "Descrição do Lanche")]
    public string DescricaoCurta { get; set; }

    [Required(ErrorMessage = "A descrição detalhada deve ser informada")]
    [StringLength(200, MinimumLength = 20, ErrorMessage = "O {0} dever ter o mínimo {1} e no máximo 80")]
    [Display(Name = "Descrição detalhada do Lanche")]
    public string DescricaoDetalhada { get; set; }

    [Required(ErrorMessage = "Informe o preço do lanche")]
    [Column(TypeName = "decimal(10,2)")]
    [Range(1,999.99, ErrorMessage ="O preço deve estar entre e 999.99")]
    [Display(Name = "Preço")]
    public decimal Preco { get; set; }

    [StringLength(200, ErrorMessage = "O {0} dever ter no máximo {1} caracteres")]
    [Display(Name = "Caminho Imagem Normal")]
    public string ImagemUrl { get; set; }

    [StringLength(200, ErrorMessage = "O {0} dever ter no máximo {1} caracteres")]
    [Display(Name = "Caminho Imagem Miniatura")]
    public string ImagemThumbnailUrl { get; set; }

    [Display(Name ="Preferido?")]
    public bool IsLanchePreferido { get; set; }

    [Display(Name = "Estoque")]
    public bool EmEstoque { get; set; }

    public int CategoriaId { get; set; }
    public virtual Categoria Categoria { get; set; }
}
