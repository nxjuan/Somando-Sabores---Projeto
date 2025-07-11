using Microsoft.EntityFrameworkCore;
using domain.IServices;
using domain.Models;
using domain.Models.Common;
using infra.DbContext;

namespace somandosabores.api.Services;

public class ClienteService(ApplicationDbContext context) : IClienteService
{
    private readonly Validations _validations = new Validations();
    public async Task<ServiceResponse<Cliente>> GetCliente(Guid id)
    {
        var serviceResponse = new ServiceResponse<Cliente>();
        try
        {
            if (id == null)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Id invalido";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            serviceResponse.Data = context.Clientes.FirstOrDefault(x => x.Id == id);
            serviceResponse.Message = "Cliente Encontrado";
            serviceResponse.Success = true;
            return serviceResponse;
        }
        catch (Exception e)
        {
            serviceResponse.Data = null;
            serviceResponse.Message = "Erro ao buscar: " + e.Message;
            serviceResponse.Success = false;
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<Cliente>> GetClienteByEmail(string email)
    {
        var serviceResponse = new ServiceResponse<Cliente>();
        try
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Email inválido";
                serviceResponse.Success = false;
                return serviceResponse;
            } 
            
            serviceResponse.Data = context.Clientes.FirstOrDefault(c => c.Email == email);
            serviceResponse.Message = "Cliente Encontrado";
            serviceResponse.Success = true;
            return serviceResponse;
        }
        catch (Exception e)
        {
            serviceResponse.Data = null;
            serviceResponse.Message = "Erro ao buscar: " + e.Message;
            serviceResponse.Success = false;
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<List<Cliente>>> GetClientes()
    {
        var serviceResponse = new ServiceResponse<List<Cliente>>();
        try
        {
            serviceResponse.Data = context.Clientes.ToList();
            serviceResponse.Message = "Clientes Encontrados";
            serviceResponse.Success = true;
            return serviceResponse;
        }
        catch (Exception e)
        {
            serviceResponse.Data = null;
            serviceResponse.Message = "Erro ao buscar: " + e.Message;
            serviceResponse.Success = false;
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<Cliente>> UpdateCliente(Cliente cliente)
    {
        var serviceResponse = new ServiceResponse<Cliente>();
        try
        {
            bool emailValido = _validations.ValidaEmail(cliente.Email);
            var clienteExiste = await context.Clientes.FindAsync(cliente.Id);
            if (clienteExiste == null)
            {
                serviceResponse.Message = "Cliente não existe";
                serviceResponse.Success = false;
                serviceResponse.Data = null;
                return serviceResponse;
            }
            if (!emailValido)
            {
                serviceResponse.Message = "Email inválido!";
                serviceResponse.Success = false;
                serviceResponse.Data = null;
                return serviceResponse;
            }

            clienteExiste.Nome = cliente.Nome ?? clienteExiste.Nome;
            clienteExiste.Email = cliente.Email ?? clienteExiste.Email;

            context.SaveChangesAsync();
            serviceResponse.Data = clienteExiste;
            serviceResponse.Message = "Cliente Updateado com sucesso";
            serviceResponse.Success = true;
            return serviceResponse;

        }
        catch (Exception e)
        {
            serviceResponse.Message = "Erro ao atualizar: "  + e.Message;
            serviceResponse.Success = false;
            serviceResponse.Data = null;
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<string>> DeleteCliente(Guid id)
    {
        var serviceResponse = new ServiceResponse<string>();
        try
        {
            var clienteExiste = await context.Clientes.FindAsync(id);
            if (clienteExiste == null)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Cliente não encontrado";
                serviceResponse.Success = false;
                return serviceResponse;
            }
            
            context.Clientes.Remove(clienteExiste);
            context.SaveChanges();
            
            serviceResponse.Data = null;
            serviceResponse.Message = "Cliente Deletado com sucesso";
            serviceResponse.Success = true;
            return serviceResponse;
        }
        catch (Exception e)
        {
            serviceResponse.Data = null;
            serviceResponse.Message = "Erro ao deletar: "  + e.Message;
            serviceResponse.Success = false;
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<Cliente>> CreateCliente(Cliente cliente)
    {
        var serviceResponse = new ServiceResponse<Cliente>();
        bool emailValido = _validations.ValidaEmail(cliente.Email);
        try
        {
            if (cliente == null || !emailValido)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Dados inválidos! Verifique novamente";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            await context.Clientes.AddAsync(cliente);
            await context.SaveChangesAsync();

            serviceResponse.Data = cliente;
            serviceResponse.Message = "Cliente Criado com sucesso";
            serviceResponse.Success = true;
            return serviceResponse;
        }
        catch (Exception e)
        {
            serviceResponse.Data = null;
            serviceResponse.Message = "Erro ao salvar: " + e.Message;
            serviceResponse.Success = false;
            return serviceResponse;
        }
    }
}