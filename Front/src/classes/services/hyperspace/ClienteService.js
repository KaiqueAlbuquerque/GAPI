import HyperspaceServiceBase from './HyperspaceServiceBase.js';
import Retorno from '../comum/Retorno.js';

export default class ClienteService extends HyperspaceServiceBase{
    constructor(rotas){
        super();
        this._rotas = rotas;   
    }

    cadastra(cliente){
        
        return fetch(`${this._url}Empresa`, {
            headers: new Headers({
                'Authorization': `Bearer ${this._token}`, 
                'Content-type' : 'application/json'
            }),
            method: 'post',
            body: JSON.stringify(cliente)
        })
        .then(res => {
            if(res.status == 200)
            {
                $('.toast').hide();
                M.toast({html: 'Cliente cadastrado com sucesso.', classes: 'rounded'});
                return res.json();
            }            
            else
            {
                Retorno.TrataRetorno(res, this._rotas);
            }
        })        
    }

    consultaTodos(pagina){
        
        return fetch(`${this._url}Empresa?pagina=${pagina}&ativo=true`, {
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

    consultaTodosCombo(){
        
        return fetch(`${this._url}Empresa`, {
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

        return fetch(`${this._url}Empresa?idEmpresa=${id}`, {
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

    alterar(cliente){
        
        return fetch(`${this._url}Empresa`, {
            headers: new Headers({
                'Authorization' : `Bearer ${this._token}`, 
                'Content-type' : 'application/json'
            }),
            method: 'put',
            body: JSON.stringify(cliente)
        })
        .then(res => {
            if(res.status == 200)
            {
                $('.toast').hide();

                if(cliente.Ativo == false)
                    M.toast({html: 'Cliente excluido com sucesso.', classes: 'rounded'});

                else
                    M.toast({html: 'Cliente alterado com sucesso.', classes: 'rounded'});

                return res.json();
            }            
            else
            {
                Retorno.TrataRetorno(res, this._rotas);
            }
        })
    }
}