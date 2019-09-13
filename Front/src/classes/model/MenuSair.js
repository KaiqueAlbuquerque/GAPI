export default class MenuSair{
    constructor(nome, email){
        this.Nome = nome;
        this.Email = email;
        this.Iniciais = '';
    }

    geraIniciais(){
        let nomes = this.Nome.split(" ");

        let iniciais = nomes[0].substring(0,1);
        iniciais += nomes[1].substring(0,1);

        this.Iniciais = iniciais;
    }
}