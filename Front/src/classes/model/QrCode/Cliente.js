export default class Cliente{
    constructor(ClienteId, NomeCliente, Ativo, ApiPertencente = 2){
        this.ClienteId = ClienteId;
        this.NomeCliente = NomeCliente;
        this.Ativo = Ativo;
        this.ApiPertencente = ApiPertencente;
    }
} 