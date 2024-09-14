using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppLoginOswald.Models
{
    public class Cliente
    {

        private ConexaoDB db = new ConexaoDB();

        [Display(Name = "Código")]
        [Required(ErrorMessage = "O campo é obrigatória")]
        [Key]
        public int ClienteID { get; set; }
        [Required(ErrorMessage = "O campo é obrigatória")]
        [StringLength(30, ErrorMessage = "Máximo 30 caracteres")]
        public string Nome { get; set; }
        [Display(Name = "Endereco")]
        [Required(ErrorMessage = "O campo é obrigatória")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string Endereco { get; set; }
        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "O campo é obrigatória")]
        [StringLength(30, ErrorMessage = "Máximo 30 caracteres")]
        public string Telefone { get; set; }
        [Display(Name = "Cpf")]
        [Required(ErrorMessage = "O campo é obrigatória")]
        [StringLength(14,MinimumLength = 14, ErrorMessage = "Máximo 11 caracteres")]
        [Remote("CpfBusca","Cliente", ErrorMessage = "CPF já cadastrado")]
        public string Cpf { get; set; }

        public void InsertCliente (Cliente cliente)
        {
            string strQuery = string.Format("Insert into cliente(nome, endereco, telefone, cpf)" +
                "Values ('{0}','{1}', '{2}', '{3}');", cliente.Nome, cliente.Endereco, cliente.Telefone, cliente.Cpf.Replace(".", string.Empty).Replace("-",string.Empty));
            db.ExecutaComando(strQuery);
        }

       

        public void Update(Cliente cliente)
        {
            string strQuery = string.Format("update cliente SET nome = '{0}', endereco = '{1}', telefone = '{2}', cpf = '{3}' Where idCliente = {4};", cliente.Nome, cliente.Endereco, cliente.Telefone, cliente.Cpf, cliente.ClienteID);

            db.ExecutaComando(strQuery);
        }

            public List<Cliente> SelectCliente()
        {
            var ListaCliente = new List<Cliente>();
            string StrQuery = "Select * from cliente;";
            MySqlDataReader myReader =  db.RetornaComando(StrQuery);
            while (myReader.Read())
            {

                var objTemp = new Cliente()
                {
                    ClienteID = int.Parse(myReader["idCliente"].ToString()),
                    Nome = myReader["nome"].ToString(),
                    Endereco = myReader["endereco"].ToString(),
                    Telefone = myReader["telefone"].ToString(),
                    Cpf = myReader["cpf"].ToString()
                };
                ListaCliente.Add(objTemp);
            }
            return ListaCliente;
        }
        
        public Cliente SelectID(Cliente cliente)
        {
            string StrQuery = string.Format("Select * from cliente where idCliente = {0};", cliente.ClienteID);

            var ObjCliente = new Cliente();

           MySqlDataReader myReder = db.RetornaComando(StrQuery);
            myReder.Read();
            ObjCliente.ClienteID = int.Parse(myReder["idCliente"].ToString());
            ObjCliente.Nome = myReder["nome"].ToString();
            ObjCliente.Endereco = myReder["endereco"].ToString();
            ObjCliente.Telefone = myReder["telefone"].ToString();
            ObjCliente.Cpf = myReder["cpf"].ToString();
            myReder.Close();
            return ObjCliente;
        }

        public void Delete(Cliente cliente)
        {
            string StrQuery = string.Format("Delete from cliente where idCliente = {0};", cliente.ClienteID);

            

           db.ExecutaComando(StrQuery);
        }

        public string BuscaCpfCliente(string Cpf)
        {
            string StrQuery = string.Format("select Cpf from cliente where Cpf = '{0}';",Cpf.Replace("-", string.Empty).Replace(".",string.Empty).Replace("_", string.Empty));
            string strCpf = db.RetornaData(StrQuery);
            return strCpf;
        }
    }
}