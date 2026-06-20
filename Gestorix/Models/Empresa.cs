using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Gestorix.Models
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
        [DisplayName("Telefone:")]
        public string Telefone { get; set; }

        [EmailAddress(ErrorMessage = "Por favor insira um e-mail válido.")]
        [DisplayName("Email:")]
        [StringLength(255, ErrorMessage = "São permitidos até 255 caracteres.")]
        public string Email { get; set; }

        [DisplayName("Cnpj:")]
        public string Cnpj { get; set; }
        [DisplayName("Url da Rede Social:")]
        public string UrlRedeSocial { get; set; }
        [DisplayName("Ddd WhatsApp:")]
        public string DddChat { get; set; }
        [DisplayName("Ddd WhatsApp:")]
        public string TelefoneChat { get; set; }
    }
}