using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace ProjetoFornecedor.Models;

public class FornecedorModel
{
    [Key]
    public int Id { get; set; }
    
    [MaxLength(100, ErrorMessage = "O nome só pode ter no máximo 100 caracteres.")]
    public string Nome { get; set; }
    
    public ulong Cnpj { get; set; }
    
    public int Cep { get; set; }
    
    [MaxLength(255, ErrorMessage = "O Endereço pode possuir no máximo 255 caracteres.")]
    public string Endereco { get; set; }
    
    public EspecialidadeEnum Especialidade { get; set; }
}