import QrCodeServiceBase from './QrCodeServiceBase.js';
import Retorno from '../comum/Retorno.js';

export default class UsuarioService extends QrCodeServiceBase{
    constructor(rotas){
        super();
        this._rotas = rotas;   
    }

    cadastrar(usuario){

        return fetch(`${this._url}Usuario/CadastraUsuario`, {
            headers: new Headers({
                'Authorization': `Bearer ${this._token}`, 
                'Content-type' : 'application/json'
            }),
            method: 'post',
            body: JSON.stringify(usuario)
        })
        .then(res => {
            if(res.status == 200)
            {
                $('.toast').hide();
                M.toast({html: 'Usuário cadastrado com sucesso.', classes: 'rounded'});

                return res.json();
            }            
            else
            {
                Retorno.TrataRetorno(res, this._rotas);
            }
        })
    }

    consultaTodos(idCliente, pagina){

        return fetch(`${this._url}Usuario/ConsultaUsuarios?idCliente=${idCliente}&idApiPertencente=2&pagina=${pagina}`, {
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

    consultaPorIdLogin(idLogin){

        return fetch(`${this._url}Usuario/ConsultaUsuarioIdLogin?idLogin=${idLogin}&idApiPertencente=2`, {
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

    consultaPorId(id){

        return fetch(`${this._url}Usuario/ConsultaUsuario?idUsuario=${id}`, {
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

    alterar(usuario){

        return fetch(`${this._url}Usuario/AlteraUsuario`, {
            headers: new Headers({
                'Authorization': `Bearer ${this._token}`, 
                'Content-type' : 'application/json'
            }),
            method: 'post',
            body: JSON.stringify(usuario)
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
}