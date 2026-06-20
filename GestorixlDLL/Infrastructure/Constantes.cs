using System.Data;

namespace GestorixDLL.Infrastructure
{
    public static class Constantes
    {
        public const string ATIVO = "A";
        public const string INATIVO = "I";
    }

    public static class ConstantesUsuario
    {
        public const string SUPERVISOR = "S";
        public const string USUARIO = "U";
    }

    public static class ConstantesProduto
    {
        #region Tipo Produto
        public const string SERVICO = "Servico";
        public const string PRODUTO = "Produto";
        #endregion

        public const string PRODUTO_GRAVADO = "Os dados do Produto foram salvos com sucesso.";
        public const string PRODUTO_ATUALIZADO = "Os dados do Produto foram atualizados com sucesso.";
        public const string PRODUTO_INATIVADO = "O Produto foi inativado com sucesso.";
        public const string PRODUTO_ATUALIZADO_EM_ESTOQUE = "O Estoque do Produto foi atualizado com sucesso.";
        public const string PRODUTO_EXISTE = "Já existe um Produto cadastrado com a referência {0} no sistema.";
    }

    public static class ConstantesEmpresa
    {
        #region Empresas
        public const string EMPRESA_ATUALIZADA = "Os dados da Empresa foram atualizados com sucesso.";
        #endregion
    }

    public static class ConstantesPedido
    {
        #region Pedido
        public const string PEDIDO_ATUALIZADO = "Os dados do Pedido foram cadastrados com sucesso.";
        public const string PEDIDO_ITENS_INEXISTENTES = "Produtos ou Serviços não estão cadastrados no sistema:";
        public const string PEDIDO_ITENS_NAO_ENVIADOS = "Produtos ou Serviços não foram cadastrados para a venda.";
        public const string PEDIDO_CLIENTE_NAO_ENVIADO = "Dados pessoais do cliente não foram informados na PRÉ-VENDA. Por favor, preencha a nota e imprima/salve.";
        public const string PEDIDO_VEICULO_NAO_ENVIADO = "Dados do veículo não foram informados na PRÉ-VENDA. Por favor, preencha a nota e imprima/salve.";
        public const string PEDIDO_NAO_ATUALIZADO = "Este Pedido está inativo e não pode ser atualizado.";
        public const string PEDIDO_INATIVADO = "O Pedido foi inativado com sucesso.";
        #endregion
    }

    public static class ConstantesAlerta
    {
        #region Alertas Acesso        
        public const string FALHA_LOGIN = "E-mail ou senha inválidos!";
        public const string EMAIL_ENVIADO_SUCESSO = "E-mail enviado com sucesso!";
        public const string EMAIL_ENVIADO_SUCESSO_DESCRICAO = "Por favor, verifique a sua caixa de entrada e siga as instruções para redefinir a senha.";
        public const string NOVA_SENHA_SUCESSO = "Senha Alterada com Sucesso!";
        public const string NOVA_SENHA_SUCESSO_DESCRICAO = "Realize o login novamente.";
        public const string EMAIL_NAO_ENCONTRADO = "E-mail não encontrado!";
        public const string EMAIL_NAO_ENCONTRADO_DESCRICAO = "Verifique o e-mail digitado e tente novamente.";
        public const string LINK_EXPIRADO = "O link expirou ou o token é inválido.";
        public const string LINK_EXPIRADO_DESCRICAO = "Cada link só pode ser usado uma vez. Solicite uma nova recuperação de senha.";
        public const string LINK_TOKEN_EMAIL_INVALIDOS = "Não existe nenhum e-mail vinculado a este token. Por favor, contate o suporte especializado.";
        #endregion

        #region Alertas Controller
        public const string MENSAGEM_TEMP_DATA_TITULO = "Título do Alerta enviado pelo Servidor";
        public const string MENSAGEM_TEMP_DATA_DESCRICAO = "Descrição do Alerta enviado pelo Servidor";
        #endregion

        #region Alertas Genericos        
        public const string SUCESSO = "Sucesso!";
        public const string TITULO_ATENCAO = "Atenção:";
        public const string REDIRECT = "Por favor, escolha uma das opções abaixo:";
        public const string FALHA_REDIRECT = "Houve um problema no redirecionamento! Tente novamente.";
        public const string NAO_VALIDO = "Preenchimento dos dados Inválidos!";
        public const string DESCRICAO_NAO_VALIDO = "Por favor, tente novamente.";
        public const string ERRO_NAO_TRATADO = "Ocorreu um erro no sistema. Por favor, contate o suporte especializado.";
        public const string NAO_ENCONTRADO = "{0} não encontrado!";
        public const string NAO_ENCONTRADO_DESCRICAO = "Por favor, preencha a referência corretamente ou clique na lupa para listar {0}.";
        #endregion        
    }
}