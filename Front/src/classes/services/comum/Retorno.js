export default class Retorno{
    static TrataRetorno(res, rotas){
        if(res.status == 401)
        {
            rotas.push({ path: '/' });
            $('.toast').hide();
            M.toast({html: 'Sessão finalizada. Por gentileza, realize login novamente.', classes: 'rounded'});
        }
        else if(res.status == 403)
        {
            rotas.push({ path: '/' });
            $('.toast').hide();
            M.toast({html: 'Você não tem autorização para acessar este conteúdo. Por gentileza, realize login novamente.', classes: 'rounded'});
        }
        else
        {
            $('.toast').hide();
            M.toast({html: 'Ocorreu um erro, por favor, tente novamente mais tarde.', classes: 'rounded'});
        }
    }    
}