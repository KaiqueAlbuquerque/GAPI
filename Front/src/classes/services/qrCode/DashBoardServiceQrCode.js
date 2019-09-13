import QrCodeServiceBase from './QrCodeServiceBase.js';
import Retorno from '../comum/Retorno.js';

export default class DashBoardServiceQrCode extends QrCodeServiceBase{
    constructor(rotas){
        super();
        this._rotas = rotas;
    }

    dashboard(requisicoesMensal, diaRequisicoesDiarias){
        const url = `${this._url}Log/Dashboard?requisicoesMensal=${requisicoesMensal}&diaRequisicoesDiarias=${diaRequisicoesDiarias}&tipo=2`;
        return fetch(url, 
                    {
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