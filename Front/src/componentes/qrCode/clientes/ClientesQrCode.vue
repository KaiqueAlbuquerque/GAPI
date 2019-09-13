<template>
    <div class="row">
        <menu-qrCode></menu-qrCode>

        <div class="container">
            <div class="row">
                <meu-card :classe="tamanho" :header="headerCardCadastro">
                    <form>                        
                        <div class="row">
                            <div class="input-field col s12 m9 l8 xl9">
                                <i class="material-icons prefix">business</i>
                                <input id="nome" type="text" autocomplete="off">
                                <label id="lblNome" for="nome">Nome Cliente</label>
                            </div>
                            <div id="btnCadastrarCliente" class="input-field right">
                                <a v-on:click="adicionaCliente" class="waves-effect waves-light btn grey darken-3 btn-small">Cadastrar</a>
                            </div>
                            <div id="btnAtualizarCliente" class="input-field right">
                                <a v-on:click="alteraCliente" class="waves-effect waves-light btn grey darken-3 btn-small">Atualizar</a>
                            </div>
                            <div id="btnCancelarCliente" class="input-field right">
                                <a v-on:click="cancelar" class="waves-effect waves-light btn grey btn-small">Cancelar</a>
                            </div>
                        </div>                 
                    </form>     
                </meu-card>                            
            </div>

            <div class="row">
                <meu-card :classe="tamanho" :header="headerCardCadastrados">
                    <minha-tabela v-on:alterar="editaCliente" v-on:excluir="excluirCliente" :titulos="titulos" :corpos="corpos" :acoes="acoes"></minha-tabela>
                    <div class="row" id="paginacao">
                        <div class="right">
                            <paginate
                                :page-count="paginas"
                                :page-range="2"
                                :margin-pages="2"
                                :click-handler="mudaPagina"
                                :prev-text="'<'"
                                :next-text="'>'"
                                :container-class="'pagination'"
                                :page-class="'waves-effect'">
                            </paginate>
                        </div>
                    </div>
                </meu-card>
            </div>     
        </div>
        <meu-loader id="loaderClientesQr"></meu-loader>
    </div>
</template>

<script>
//Componentes
import MenuQrCode from '../menu/MenuQrCode.vue';
import Card from '../../compartilhado/card/Card.vue';
import Tabela from '../../compartilhado/tabela/Tabela.vue';
import Loader from '../../compartilhado/loader/Loader.vue';

//Models
import Cliente from '../../../classes/model/QrCode/Cliente.js';

//Serviços
import ClienteService from '../../../classes/services/qrCode/ClienteService.js';

export default {
    components: {
        'menu-qrCode': MenuQrCode,
        'meu-card': Card,
        'minha-tabela': Tabela,
        'meu-loader': Loader
    },

    methods:{
        mudaPagina(page){
            this.preencheTabela((page - 1));
        },

        aguarde(){
            let loader = document.getElementById("loaderClientesQr");
            loader.style.display = 'block';
        },

        naoAguarde(){
            let loader = document.getElementById("loaderClientesQr");
            loader.style.display = 'none';
        },

        adicionaCliente(){
            let nomeCliente = document.getElementById("nome");

            if(nomeCliente.value != ""){
                this.aguarde();
                
                let cliente = new Cliente(0, nomeCliente.value, true, 2);

                let clienteService = new ClienteService(this.$router);
                clienteService.cadastra(cliente)
                    .then(cliente => {
                        this.preencheTabela(0);
                        this.naoAguarde();
                    })
                    .catch(erro => this.naoAguarde())

                this.arrumaInputNome();
            }
            else{
                $('.toast').hide();
                M.toast({html: 'Informe o nome do cliente.', classes: 'rounded'});
            }
        },

        alteraCliente(){
            let nome = document.getElementById("nome").value;

            if(nome != ""){
                this.aguarde();
                let clienteService = new ClienteService(this.$router);

                this.clienteAlteracao.NomeCliente = document.getElementById("nome").value;

                clienteService.alterar(this.clienteAlteracao)
                    .then(res => {
                        this.preencheTabela(0);
                        this.arrumaInputNome();
                        this.botoesCadastrar();
                        this.naoAguarde();
                    })
                    .catch(err => this.naoAguarde())
            }
            else{
                $('.toast').hide();
                M.toast({html: 'Informe o nome do cliente.', classes: 'rounded'});
            }
        },

        cancelar(){
            this.botoesCadastrar();
            this.arrumaInputNome();
        },

        editaCliente(obj){
            this.aguarde();

            let clienteService = new ClienteService(this.$router);
            clienteService.consultaPorId(obj.id)
                .then(cliente => {
                    this.clienteAlteracao = cliente;
                    document.getElementById("nome").value = cliente.NomeCliente;
                    document.getElementById("lblNome").classList.add("active");
                    $('html, body').animate({ scrollTop: 0 }, 500);
                    this.botoesAlterar();
                    this.naoAguarde();
                })
                .catch(err => this.naoAguarde())
        },

        excluirCliente(obj){
            this.aguarde();
            
            let cliente = this.arrayClientes.find(cli => {
                return cli.ClienteId == obj.id;
            });

            let clienteService = new ClienteService(this.$router);
            cliente.Ativo = false;
            clienteService.alterar(cliente)
                .then(res => {
                    this.preencheTabela(0);
                    this.botoesCadastrar();
                    this.arrumaInputNome();
                    this.naoAguarde();
                })
                .catch(err => this.naoAguarde())
        },

        arrumaInputNome(){
            let nomeCliente = document.getElementById("nome");
            nomeCliente.value = "";
            document.getElementById("lblNome").classList.remove("active");
        },

        preencheTabela(pagina){
            this.aguarde();

            let clienteService = new ClienteService(this.$router);
            clienteService.consultaTodos(pagina)
                .then(clientes => {
                    this.corpos = [];
                    this.arrayClientes = [];

                    this.paginas = clientes.QuantidadePaginas;
                    this.arrayClientes = clientes.Clientes;

                    clientes.Clientes.forEach(cliente => {
                        this.corpos.push({"id": cliente.ClienteId, "valor": [cliente.NomeCliente]});
                    });

                    if(this.paginas < 2)
                        document.getElementById("paginacao").style.display = "none";
                    else
                        document.getElementById("paginacao").style.display = "block";

                    this.naoAguarde();
                })  
                .catch(err => this.naoAguarde())
        },

        botoesCadastrar(){
            this.clienteAlteracao = "";
                    
            document.getElementById("btnCadastrarCliente").style.display = "block";
            document.getElementById("btnCancelarCliente").style.display = "none";
            document.getElementById("btnAtualizarCliente").style.display = "none";
        },

        botoesAlterar(){
            document.getElementById("btnCadastrarCliente").style.display = "none";
            document.getElementById("btnCancelarCliente").style.display = "block";
            document.getElementById("btnAtualizarCliente").style.display = "block";
        }
    },

    data(){
        return{
            //Controladores
            arrayClientes: [],
            clienteAlteracao: "",
            
            //Paginação
            paginas: 0,

            //Card branco
            tamanho: "col s12",
            headerCardCadastro: {
                titulo: "Cadastro de clientes"
            },
            headerCardCadastrados: {
                titulo: "Clientes cadastrados"
            },

            //Tabela
            titulos: ['Nome'],
            corpos: [],
            acoes: true,
        }
    },

    mounted(){
        this.botoesCadastrar();
        this.preencheTabela(0); 
    } 
}
</script>

<style>
.pagination li.active {
    background-color: #424242;
}
</style>

<style scoped>
#paginacao{
    padding-top: 15px;
    display: none;
}
.row {
    margin-bottom: 0px;
}
#btnCancelarCliente{
    margin-right: 5px;
}
</style>
