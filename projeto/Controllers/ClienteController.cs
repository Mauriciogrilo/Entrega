using Microsoft.AspNetCore.Mvc;
using projeto.Model;
using projeto.Repository;

namespace projeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        // Injetar dependencia do repositorio
        private readonly IClienteRepository _repository;

        public ClienteController(IClienteRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            var clientes = await _repository.GetClientes();
            return clientes.Any() ? Ok(clientes) : NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id){
            var cliente = await _repository.GetClienteById(id);
            return cliente != null ? Ok(cliente) : NotFound("Usuario não encontrado"); 
        }
        

         [HttpPost]
        public async Task<IActionResult> Post(Cliente cliente){
            _repository.AddCliente(cliente);
            return await _repository.SaveChangeAsync() ? Ok("Cliente adicionado") : BadRequest("Algo deu errado");
        }

         [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, Cliente cliente){
            var clienteExiste = await _repository.GetClienteById(id);
            if(clienteExiste == null) return NotFound("Cliente não encontrado");
            clienteExiste.Nome = cliente.Nome ?? clienteExiste.Nome;

            _repository.AtualizarCliente(clienteExiste);

            return await _repository.SaveChangeAsync()
            ? Ok("Cliente Atualizado") : BadRequest("Algo deu errado");

        }

        [HttpDelete("{id}")]
       public async Task<IActionResult> Delete(int id){
        var clienteExiste = await _repository.GetClienteById(id);
        if(clienteExiste == null) 
        return NotFound("Cliente não encontrado");

        _repository.DeletarCliente(clienteExiste);

        return await _repository.SaveChangeAsync()
            ? Ok("Cliente Atualizado") : BadRequest("Algo deu errado");
       }
       
    }
}
 
 