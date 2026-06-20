using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorixDLL.DML
{
    public class Empresa
    {
        [DisplayName("Cep:")]
        public string Cep { get; set; }
        [DisplayName("Logradouro:")]
        public string Logradouro { get; set; }
        [DisplayName("Número:")]
        public string Numero { get; set; }
        [DisplayName("Bairro:")]
        public string Bairro { get; set; }
        [DisplayName("Cidade:")]
        public string Cidade { get; set; }
        [DisplayName("Estado:")]
        public string Estado { get; set; }        
        [DisplayName("Ddd:")]
        public string Ddd { get; set; }
        [DisplayName("Telefone Empresa:")]
        public string Telefone { get; set; }

        [EmailAddress(ErrorMessage = "Por favor insira um e-mail válido.")]
        [DisplayName("Email:")]
        [StringLength(255, ErrorMessage = "São permitidos até 255 caracteres.")]
        public string Email { get; set; }
                
        [DisplayName("Cnpj:")]
        public string Cnpj { get; set; }
        [DisplayName("Url da Rede Social:")]
        public string UrlRedeSocial { get; set; }
        [DisplayName("Ddd:")]
        public string DddChat { get; set; }
        [DisplayName("Telefone WhatsApp:")]
        public string TelefoneChat { get; set; }
    }
    public class EmpresaViewModelDML
    {
        public List<Empresa> Empresa { get; set; }

        public EmpresaViewModelDML()
        {
            this.Empresa = new List<Empresa>();
        }

        public EmpresaViewModelDML ConvertToListAtendentes(List<Empresa> listaEmpresa)
        {
            EmpresaViewModelDML empresaViewModel = new EmpresaViewModelDML();
            if (listaEmpresa != null)
            {
                // atendenteViewModel.Atendente = listaAtendente;
                // foreach está sendo usado para CASO deseje incluir validação no carregamento dos registros via conversão
                foreach (var empresa in listaEmpresa)
                {
                    empresaViewModel.Empresa.Add(empresa);
                }
            }

            return empresaViewModel;
        }
    }
}
