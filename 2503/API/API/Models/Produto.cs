namespace API.Models;

public class Produto
{
    public Produto(string nome, string descricao, double preco)
    {
        Id = Guid.NewGuid().ToString();
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        CriadoEm = DateTime.Now;
    }

    //Atributo ou propriedade: nome e descricao
    public string Id { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public double Preco { get; set; }
    public int Quantidade { get; set; }
    public DateTime CriadoEm { get; set; }

    // private string nome;
    // private string descricao;

    // public string getNome()
    // {
    //     return this.nome;
    // }

    // public void setNome(string nome)
    // {
    //     this.nome = nome;
    // }

    // public string getDescricao()
    // {
    //     return this.descricao;
    // }

    // public void setDescricao(string descricao)
    // {
    //     this.descricao = descricao;
    // }
}
