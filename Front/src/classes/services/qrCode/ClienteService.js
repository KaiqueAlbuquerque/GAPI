import QrCodeServiceBase from './QrCodeServiceBase.js';
import Retorno from '../comum/Retorno.js';

export default class ClienteService extends QrCodeServiceBase{
    constructor(rotas){
        super();
        this._rotas = rotas;   
    }

    cadastra(cliente){
        
        return fetch(`${this._url}Cliente/CadastraCliente`, {
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
        
        return fetch(`${this._url}Cliente/ConsultaClientes?idApiPertencente=2&pagina=${pagina}`, {
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
        
        return fetch(`${this._url}Cliente/ConsultaClientes?idApiPertencente=2`, {
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

        return fetch(`${this._url}Cliente/ConsultaCliente?idCliente=${id}`, {
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
        
        return fetch(`${this._url}Cliente/AlteraCliente`, {
            headers: new Headers({
                'Authorization' : `Bearer ${this._token}`, 
                'Content-type' : 'application/json'
            }),
            method: 'post',
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