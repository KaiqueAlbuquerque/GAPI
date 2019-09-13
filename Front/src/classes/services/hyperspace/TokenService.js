import HyperspaceServiceBase from './HyperspaceServiceBase.js';
import Retorno from '../comum/Retorno.js';

export default class TokenService extends HyperspaceServiceBase{
    constructor(rotas){
        super();
        this._rotas = rotas;   
    }

    consultaTodos(pagina){

        return fetch(`${this._url}Token?pagina=${pagina}&ativo=true`, {
            headers: new Headers({
                'Authorization': `Bearer ${this._token}`, 
                'Content-type' : 'application/json'
            })
        })
        .then(res => {
            if(res.status == 200)
            {
                return res.json();
            }            
            else
            {
                Retorno.TrataRetorno(res, this._rotas);
            }
        })
    }

    cadastrar(tokens){

        return fetch(`${this._url}Token`, {
            headers: new Headers({
                'Authorization': `Bearer ${this._token}`, 
                'Content-type' : 'application/json'
            }),
            method: 'post',
            body: JSON.stringify(tokens)
        })
        .then(res => {
            if(res.status == 200)
            {
                $('.toast').hide();
                M.toast({html: 'Token(s) cadastrado(s) com sucesso.', classes: 'rounded'});
            }            
            else
            {
                Retorno.TrataRetorno(res, this._rotas);
            }
        })
    }
}