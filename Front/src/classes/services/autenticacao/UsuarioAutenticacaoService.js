import AutenticacaoServiceBase from './AutenticacaoServiceBase.js';
import Retorno from '../comum/Retorno.js';

export default class UsuarioAutenticacaoService extends AutenticacaoServiceBase{
    constructor(rotas){
        super();
        this._rotas = rotas;   
    }

    cadastrar(usuario){
        
        return fetch(`${this._url}Usuario`,{
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

    alterar(usuario){
        
        return fetch(`${this._url}Usuario`,{
            headers: new Headers({
                'Authorization': `Bearer ${this._token}`, 
                'Content-type' : 'application/json'
            }),
            method: 'put',
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

    consultaPorEmail(email){

        return fetch(`${this._url}Usuario?email=${email}`, {
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

        return fetch(`${this._url}Usuario?idUsuario=${id}`, {
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

    consultaTodos(pagina){

        return fetch(`${this._url}Usuario?pagina=${pagina}&ativo=true`, {
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
}